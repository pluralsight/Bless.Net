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
            var index = files.Count() - 1;
            foreach (var file in files)
            {
                var fileName = baseFileName + (index == 0 ? "" : index.ToString(CultureInfo.InvariantCulture));
                using (var fileStream = File.OpenWrite(Path.Combine(dir ?? "", fileName)))
                {
                    var bytes = Encoding.UTF8.GetBytes(file);
                    fileStream.Write(bytes, 0, bytes.GetLength(0));
                }
            }
        }
    }
}
