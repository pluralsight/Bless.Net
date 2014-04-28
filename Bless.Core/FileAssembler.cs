using System.Collections.Generic;
using System.IO;

namespace Bless.Core
{
    public class FileAssembler
    {
        public IDictionary<string, string> BuildFiles(IEnumerable<string> files, string baseFileName)
        {
            var baseFileExtension = Path.GetExtension(baseFileName);
            var extensionLessFileName = Path.GetFileNameWithoutExtension(baseFileName);
            var fileNamesAndContents = new Dictionary<string, string>();
            int modifier = 0;
            foreach (var file in files)
            {
                modifier++;
                fileNamesAndContents.Add(BuildFileName(extensionLessFileName, modifier, baseFileExtension), file);
            }
            fileNamesAndContents.Add(baseFileName, fileNamesAndContents[BuildFileName(extensionLessFileName, modifier, baseFileExtension)]);
            fileNamesAndContents.Remove(BuildFileName(extensionLessFileName, modifier, baseFileExtension));
            return fileNamesAndContents;
        }

        private static string BuildFileName(string extensionLessFileName, int modifier, string baseFileExtension)
        {
            return extensionLessFileName + modifier + baseFileExtension;
        }

        public string AddImportsToBaseFile(IEnumerable<string> assembledFiles, string outputCss)
        {
            string imports = string.Empty;
            foreach (var file in assembledFiles)
            {
                imports += string.Format("@import(/{0});\n", file);
            }
            return imports + outputCss;
        }
    }
}
