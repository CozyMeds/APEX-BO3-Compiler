namespace APEX.Compiler.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.lblConsole = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnCompile = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlHeaderLine = new System.Windows.Forms.Panel();
            this.pnlFooterLine = new System.Windows.Forms.Panel();
            this.lblStatusIndicator = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnOpenOutput = new System.Windows.Forms.Button();
            this.btnBrowseCompiler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "APEX COMPILER";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.lblSubtitle.Location = new System.Drawing.Point(32, 70);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(420, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "BLACK OPS III / PS4";
            // 
            // lblSource
            // 
            this.lblSource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.lblSource.Location = new System.Drawing.Point(30, 132);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(300, 20);
            this.lblSource.TabIndex = 2;
            this.lblSource.Text = "SOURCE DIRECTORY";
            // 
            // txtSource
            // 
            this.txtSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.txtSource.Location = new System.Drawing.Point(30, 160);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(690, 25);
            this.txtSource.TabIndex = 3;
            // 
            // btnBrowseSource
            // 
            this.btnBrowseSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(109)))), ((int)(((byte)(151)))));
            this.btnBrowseSource.FlatAppearance.BorderSize = 0;
            this.btnBrowseSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseSource.ForeColor = System.Drawing.Color.White;
            this.btnBrowseSource.Location = new System.Drawing.Point(735, 157);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(135, 36);
            this.btnBrowseSource.TabIndex = 5;
            this.btnBrowseSource.Text = "Browse";
            this.btnBrowseSource.UseVisualStyleBackColor = false;
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
            // 
            // lblOutput
            // 
            this.lblOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.lblOutput.Location = new System.Drawing.Point(30, 220);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(300, 20);
            this.lblOutput.TabIndex = 6;
            this.lblOutput.Text = "OUTPUT FILE";
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.txtOutput.Location = new System.Drawing.Point(30, 248);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(690, 25);
            this.txtOutput.TabIndex = 7;
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(109)))), ((int)(((byte)(151)))));
            this.btnBrowseOutput.FlatAppearance.BorderSize = 0;
            this.btnBrowseOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseOutput.ForeColor = System.Drawing.Color.White;
            this.btnBrowseOutput.Location = new System.Drawing.Point(735, 245);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(135, 36);
            this.btnBrowseOutput.TabIndex = 8;
            this.btnBrowseOutput.Text = "Browse";
            this.btnBrowseOutput.UseVisualStyleBackColor = false;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // lblConsole
            // 
            this.lblConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.lblConsole.Location = new System.Drawing.Point(30, 315);
            this.lblConsole.Name = "lblConsole";
            this.lblConsole.Size = new System.Drawing.Size(300, 20);
            this.lblConsole.TabIndex = 9;
            this.lblConsole.Text = "BUILD CONSOLE";
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(19)))), ((int)(((byte)(30)))));
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbLog.ForeColor = System.Drawing.Color.Lime;
            this.rtbLog.Location = new System.Drawing.Point(30, 345);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(840, 175);
            this.rtbLog.TabIndex = 10;
            this.rtbLog.Text = "Ready to compile...";
            // 
            // btnCompile
            // 
            this.btnCompile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(126)))), ((int)(((byte)(167)))));
            this.btnCompile.FlatAppearance.BorderSize = 0;
            this.btnCompile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompile.ForeColor = System.Drawing.Color.White;
            this.btnCompile.Location = new System.Drawing.Point(30, 540);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(840, 50);
            this.btnCompile.TabIndex = 11;
            this.btnCompile.Text = "COMPILE GSCC";
            this.btnCompile.UseVisualStyleBackColor = false;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.lblStatus.Location = new System.Drawing.Point(32, 619);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(450, 24);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHeaderLine
            // 
            this.pnlHeaderLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            this.pnlHeaderLine.Location = new System.Drawing.Point(30, 108);
            this.pnlHeaderLine.Name = "pnlHeaderLine";
            this.pnlHeaderLine.Size = new System.Drawing.Size(840, 2);
            this.pnlHeaderLine.TabIndex = 13;
            // 
            // pnlFooterLine
            // 
            this.pnlFooterLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(65)))), ((int)(((byte)(82)))));
            this.pnlFooterLine.Location = new System.Drawing.Point(30, 603);
            this.pnlFooterLine.Name = "pnlFooterLine";
            this.pnlFooterLine.Size = new System.Drawing.Size(840, 1);
            this.pnlFooterLine.TabIndex = 14;
            // 
            // lblStatusIndicator
            // 
            this.lblStatusIndicator.AutoSize = true;
            this.lblStatusIndicator.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusIndicator.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblStatusIndicator.ForeColor = System.Drawing.Color.Lime;
            this.lblStatusIndicator.Location = new System.Drawing.Point(12, 617);
            this.lblStatusIndicator.Name = "lblStatusIndicator";
            this.lblStatusIndicator.Size = new System.Drawing.Size(23, 25);
            this.lblStatusIndicator.TabIndex = 15;
            this.lblStatusIndicator.Text = "●";
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(420, 619);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(450, 24);
            this.lblVersion.TabIndex = 16;
            this.lblVersion.Text = "APEX COMPILER · v0.2";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOpenOutput
            // 
            this.btnOpenOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnOpenOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            this.btnOpenOutput.Location = new System.Drawing.Point(356, 619);
            this.btnOpenOutput.Name = "btnOpenOutput";
            this.btnOpenOutput.Size = new System.Drawing.Size(200, 30);
            this.btnOpenOutput.TabIndex = 17;
            this.btnOpenOutput.Text = "\tOPEN OUTPUT FOLDER";
            this.btnOpenOutput.UseVisualStyleBackColor = false;
            this.btnOpenOutput.Click += new System.EventHandler(this.btnOpenOutput_Click);
            // 
            // btnBrowseCompiler
            // 
            this.btnBrowseCompiler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnBrowseCompiler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCompiler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            this.btnBrowseCompiler.Location = new System.Drawing.Point(356, 305);
            this.btnBrowseCompiler.Name = "btnBrowseCompiler";
            this.btnBrowseCompiler.Size = new System.Drawing.Size(200, 30);
            this.btnBrowseCompiler.TabIndex = 18;
            this.btnBrowseCompiler.Text = "SELECT COMPILER";
            this.btnBrowseCompiler.UseVisualStyleBackColor = false;
            this.btnBrowseCompiler.Click += new System.EventHandler(this.btnBrowseCompiler_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(31)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.btnBrowseCompiler);
            this.Controls.Add(this.btnOpenOutput);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblStatusIndicator);
            this.Controls.Add(this.pnlFooterLine);
            this.Controls.Add(this.pnlHeaderLine);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCompile);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.lblConsole);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.btnBrowseSource);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Apex Compiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Label lblConsole;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnCompile;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlHeaderLine;
        private System.Windows.Forms.Panel pnlFooterLine;
        private System.Windows.Forms.Label lblStatusIndicator;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnOpenOutput;
        private System.Windows.Forms.Button btnBrowseCompiler;
    }
}

