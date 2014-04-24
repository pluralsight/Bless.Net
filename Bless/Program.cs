using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            foreach (string file in files)
            {
                // TODO Create a new file and write the contents of file to it.
            }
        }
    }
}
