using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bless.Core;

namespace Bless
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<string> files;
            using (var streamReader = new StreamReader(args[0]))
            {
                var parser = new BlessParser();
                files = parser.Parse(streamReader.ReadToEnd());
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
