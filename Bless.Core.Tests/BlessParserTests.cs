using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Bless.Core.Tests
{
    [TestFixture]
    public class BlessParserTests
    {
        private List<string> files;

        [SetUp]
        public void SetUp()
        {
            var blessParser = new BlessParser();
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bless.Core.Tests.example.css");
            files = blessParser.Parse(stream);
        }

        [Test]
        public void SplitsFiles()
        {
            Assert.That(files.Count, Is.EqualTo(4));
        }

        [Test]
        public void CheckFirstLinesOfFiles()
        {
            Assert.That(files[0].StartsWith("#test{background-color:red;}"));
            Assert.That(files[1].StartsWith("#test{background-color:green;}"));
            Assert.That(files[2].StartsWith("#test{background-color:blue;}"));
            Assert.That(files[3].EndsWith("#test{background-color:white;}"));
        }
    }
}
