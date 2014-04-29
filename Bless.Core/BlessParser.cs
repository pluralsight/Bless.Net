using System.Collections.Generic;
using System.IO;
using System.Text;
using ExCSS;

namespace Bless.Core
{
    public class BlessParser
    {
        public const int MaxSelectorCount = 4095;

        public IEnumerable<string> Parse(Stream stream)
        {
            var files = new List<string>();
            var parser = new Parser();
            var stylesheet = parser.Parse(stream);

            var file = new StringBuilder();
            var index = 0;
            foreach (StyleRule rule in stylesheet.Rulesets)
            {
                if (index >= MaxSelectorCount)
                {
                    index = 0;
                    files.Add(file.ToString());
                    file = new StringBuilder();
                }

                file.AppendLine(rule.ToString());
                index++;
            }
            files.Add(file.ToString().TrimEnd());

            return files;
        }
    }
}
