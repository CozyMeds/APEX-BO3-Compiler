using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace APEX.Compiler.GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            UpdateStatus("Ready", Color.FromArgb(88, 214, 154));

            // Load previously saved paths.
            txtSource.Text = Properties.Settings.Default.SourcePath;
            txtOutput.Text = Properties.Settings.Default.OutputPath;
            RefreshCompilerDisplay();

            // The fields shorten visually when the window narrows. Hover to see the full path.
            pathToolTip.AutoPopDelay = 18000;
            pathToolTip.InitialDelay = 350;
            pathToolTip.ReshowDelay = 100;
            pathToolTip.ShowAlways = true;
            txtCompiler.TextChanged += (sender, args) =>
                pathToolTip.SetToolTip(txtCompiler, txtCompiler.Text);
            txtSource.TextChanged += (sender, args) =>
                pathToolTip.SetToolTip(txtSource, txtSource.Text);
            txtOutput.TextChanged += (sender, args) =>
                pathToolTip.SetToolTip(txtOutput, txtOutput.Text);
            UpdatePathTooltips();

            this.FormClosing += MainForm_FormClosing;
        }

        // Use the saved compiler path, or find the CLI beside the GUI in bundle releases.
        private string ResolveCompilerPath()
        {
            string configuredPath = Properties.Settings.Default.CompilerPath;
            if (!string.IsNullOrWhiteSpace(configuredPath) && File.Exists(configuredPath))
                return configuredPath;

            string siblingPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "APEX.Compiler.exe");
            if (File.Exists(siblingPath))
                return siblingPath;

            return configuredPath ?? string.Empty;
        }

        private void RefreshCompilerDisplay()
        {
            string compilerPath = ResolveCompilerPath();
            bool found = !string.IsNullOrWhiteSpace(compilerPath) && File.Exists(compilerPath);

            txtCompiler.Text = compilerPath;
            lblCompilerBadge.Text = found ? "COMPILER FOUND" : "SELECT COMPILER";
            lblCompilerBadge.ForeColor = found
                ? Color.FromArgb(88, 214, 154)
                : Color.FromArgb(226, 179, 98);
            lblCompilerBadge.BackColor = found
                ? Color.FromArgb(24, 60, 54)
                : Color.FromArgb(59, 53, 38);
        }

        private void UpdatePathTooltips()
        {
            pathToolTip.SetToolTip(txtCompiler, txtCompiler.Text);
            pathToolTip.SetToolTip(txtSource, txtSource.Text);
            pathToolTip.SetToolTip(txtOutput, txtOutput.Text);
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select your APEX GSC source directory";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtSource.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Save compiled APEX GSCC";
                dialog.Filter = "GSCC Files (*.gscc)|*.gscc";
                dialog.DefaultExt = "gscc";
                dialog.FileName = "APEX.gscc";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutput.Text = dialog.FileName;
                }
            }
        }

        private async void btnCompile_Click(object sender, EventArgs e)
        {
            // Prefer a configured compiler, with portable sibling-EXE detection.
            string compilerPath = ResolveCompilerPath();

            string sourcePath = txtSource.Text.Trim();
            string outputPath = txtOutput.Text.Trim();

            // Check the paths before compiling.
            if (!File.Exists(compilerPath))
            {
                MessageBox.Show("Compiler executable not found.");
                return;
            }

            if (!Directory.Exists(sourcePath) && !File.Exists(sourcePath))
            {
                UpdateStatus("Invalid source", Color.FromArgb(240, 91, 91));
                MessageBox.Show("Please select a valid source directory.");
                return;
            }

            if (string.IsNullOrWhiteSpace(outputPath) ||
                !outputPath.EndsWith(".gscc", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Please select a .gscc output file.");
                return;
            }

            string outputDirectory = Path.GetDirectoryName(outputPath);

            string tempOutputPath = Path.Combine(
                outputDirectory,
                "APEX_" + Guid.NewGuid().ToString("N") + ".gscc"
            );

            // Prevent multiple compilations simultaneously.
            btnCompile.Enabled = false;
            UpdateStatus("Compiling...", Color.FromArgb(0, 191, 255));
            rtbLog.Clear();
            Stopwatch buildTimer = Stopwatch.StartNew();

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = compilerPath,
                    Arguments = "\"" + sourcePath + "\" \"" + tempOutputPath + "\"",
                    WorkingDirectory = Path.GetDirectoryName(compilerPath),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;

                    process.OutputDataReceived += (s, args) =>
                    {
                        if (args.Data != null)
                            AppendLog(args.Data, Color.FromArgb(86, 216, 255));
                    };

                    process.ErrorDataReceived += (s, args) =>
                    {
                        if (args.Data != null)
                            AppendLog("ERROR: " + args.Data, Color.FromArgb(240, 91, 91));
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    await Task.Run(() => process.WaitForExit());

                    if (process.ExitCode == 0 && File.Exists(tempOutputPath) && new FileInfo(tempOutputPath).Length > 4)
                    {
                        // Replace an existing build, retaining the old
                        // file if replacement fails.
                        if (File.Exists(outputPath))
                        {
                            File.Replace(tempOutputPath, outputPath, null);
                        }
                        else
                        {
                            File.Move(tempOutputPath, outputPath);
                        }

                        buildTimer.Stop();
                        long outputBytes = new FileInfo(outputPath).Length;
                        string elapsed = buildTimer.Elapsed.TotalSeconds.ToString("0.00") + "s";

                        UpdateStatus("Build successful · " + elapsed, Color.FromArgb(88, 214, 154));
                        AppendLog("Final output: " + outputPath, Color.FromArgb(86, 216, 255));
                        AppendLog("Output size: " + outputBytes.ToString("N0") + " bytes");
                        AppendLog("Total build time: " + elapsed);
                        AppendLog(
                            "GSCC compiled and verified successfully.",
                            Color.FromArgb(0, 255, 0)
                        );
                    }
                    else
                    {
                        buildTimer.Stop();
                        string elapsed = buildTimer.Elapsed.TotalSeconds.ToString("0.00") + "s";
                        UpdateStatus("Build failed · " + elapsed, Color.FromArgb(240, 91, 91));

                        AppendLog(
                            "Compilation failed. Previous GSCC preserved.",
                            Color.FromArgb(240, 91, 91)
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                buildTimer.Stop();
                UpdateStatus("Build error · " + buildTimer.Elapsed.TotalSeconds.ToString("0.00") + "s",
                    Color.FromArgb(240, 91, 91));
                AppendLog("ERROR: " + ex.Message, Color.FromArgb(240, 91, 91));
            }

            finally
            {
                buildTimer.Stop();
                if (File.Exists(tempOutputPath))
                {
                    try
                    {
                        File.Delete(tempOutputPath);
                    }
                    catch (IOException)
                    {
                        AppendLog("WARNING: Could not remove temporary build file.");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        AppendLog("WARNING: Access denied removing temporary build file.");
                    }
                }

                btnCompile.Enabled = true;
            }
        }

        // Writes compiler messages to the build console.
        private void AppendLog(string message, Color? color = null)
        {
            if (rtbLog.IsDisposed)
                return;

            if (rtbLog.InvokeRequired)
            {
                rtbLog.BeginInvoke(new Action(() =>
                    AppendLog(message, color)
                ));
                return;
            }

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;

            rtbLog.SelectionColor = color ?? Color.FromArgb(206, 220, 232);

            rtbLog.AppendText(message + Environment.NewLine);
            rtbLog.ScrollToCaret();
        }

        private void UpdateStatus(string message, Color color)
        {
            lblStatus.Text = message;
            lblStatusIndicator.ForeColor = color;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.SourcePath = txtSource.Text;
            Properties.Settings.Default.OutputPath = txtOutput.Text;

            Properties.Settings.Default.Save();
        }

        private void btnOpenOutput_Click(object sender, EventArgs e)
        {
            string outputPath = txtOutput.Text.Trim();

            if (!File.Exists(outputPath))
            {
                MessageBox.Show("No compiled GSCC file found.");
                return;
            }

            Process.Start("explorer.exe", "/select,\"" + outputPath + "\"");
        }

        private void btnBrowseCompiler_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select APEX Compiler";
                dialog.Filter = "Executable Files (*.exe)|*.exe";
                dialog.FileName = "APEX.Compiler.exe";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.CompilerPath = dialog.FileName;
                    Properties.Settings.Default.Save();

                    RefreshCompilerDisplay();
                    AppendLog("\nCompiler selected: " + dialog.FileName);
                }
            }
        }
    }
}