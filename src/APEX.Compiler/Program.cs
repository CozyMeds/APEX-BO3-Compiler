using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TreyarchCompiler;

namespace ApexCompiler
{
    internal static class Program
    {
        // A standalone compiler ONLY. Does not connect to or modify a running process or PS4.
        private static int Main(string[] args)
        {
            if (args.Length != 2 || args[0] == "--help")
            {
                Console.WriteLine("APEX compiler-only | PS4 BO3 1.33 GSC");
                Console.WriteLine("Usage: APEX.Compiler.exe <source-directory-or-file> <output.gscc>");
                return args.Length == 1 && args[0] == "--help" ? 0 : 2;
            }

            try
            {
                var input = Path.GetFullPath(args[0]);
                var output = Path.GetFullPath(args[1]);
                if (!output.EndsWith(".gscc", StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Output file must have a .gscc extension.");

                var files = File.Exists(input) ? new[] { input } :
                    Directory.Exists(input) ? Directory.GetFiles(input, "*.gsc", SearchOption.AllDirectories)
                        .OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToArray() :
                    throw new FileNotFoundException("Source directory/file not found: " + input);

                files = files.Where(x => x.EndsWith(".gsc", StringComparison.OrdinalIgnoreCase)).ToArray();
                if (files.Length == 0) throw new InvalidOperationException("No .gsc source files found.");

                var source = new StringBuilder();
                foreach (var file in files)
                {
                    Console.WriteLine("Source: " + file);
                    source.AppendLine(File.ReadAllText(file));
                    source.AppendLine();
                }

                var preprocessor = new ConditionalBlocks();
                preprocessor.LoadConditionalTokens(new List<string> { "BO3" });
                var processed = preprocessor.ParseSource(source.ToString());
                var result = Compiler.Compile(false, processed);
                if (result == null || !string.IsNullOrEmpty(result.Error))
                {
                    Console.Error.WriteLine("Compilation failed: " + (result == null ? "No result" : result.Error));
                    return 1;
                }
                if (result.CompiledScript == null || result.CompiledScript.Length < 0x50)
                    throw new InvalidDataException("Compiler produced no valid output.");

                // The PS4 BO3 script magic, byte for byte, little-endian.
                byte[] magic = { 0x80, 0x47, 0x53, 0x43, 0x0D, 0x0A, 0x00, 0x1C };
                if (!result.CompiledScript.Take(8).SequenceEqual(magic))
                    throw new InvalidDataException("Output does not have expected PS4 BO3 script magic.");

                var directory = Path.GetDirectoryName(output);
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                File.WriteAllBytes(output, result.CompiledScript);
                Console.WriteLine("Wrote " + output + " (" + result.CompiledScript.Length + " bytes)");
                foreach (var warning in result.Warning ?? new List<string>()) Console.WriteLine("Warning: " + warning);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: " + ex.Message);
                return 1;
            }
        }
    }
}
