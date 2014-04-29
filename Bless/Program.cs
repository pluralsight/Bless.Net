using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Bless.Core;
using System.IO;

namespace Bless
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<string> files;
            using (var fileStream = File.Open(args[0], FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var parser = new BlessParser();
                files = parser.Parse(fileStream);
            }

            var dir = Path.GetDirectoryName(args[1]);
            var baseFileName = Path.GetFileName(args[1]);

            var fileAssembler = new FileAssembler();
            var builtFiles = fileAssembler.BuildFiles(files, baseFileName);
            builtFiles[builtFiles.Last().Key] = fileAssembler.AddImportsToBaseFile(builtFiles.Take(builtFiles.Count - 1).Select(x => x.Key), builtFiles.Last().Value);

            foreach (var file in builtFiles)
            {
                File.WriteAllText(Path.Combine(dir, file.Key), file.Value);
            }
        }
    }
}
