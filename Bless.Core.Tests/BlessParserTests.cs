using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Bless.Core.Tests
{
    [TestFixture]
    public class BlessParserTests
    {
        private List<string> _files;

        [SetUp]
        public void SetUp()
        {
            var blessParser = new BlessParser();
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bless.Core.Tests.example.css");
            _files = blessParser.Parse(stream).ToList();
        }

        [Test]
        public void SplitsFiles()
        {
            Assert.That(_files.Count, Is.EqualTo(4));
        }

        [Test]
        public void CheckFirstLinesOfFiles()
        {
            Assert.That(_files[0].StartsWith("#test{background-color:red;}"));
            Assert.That(_files[1].StartsWith("#test{background-color:green;}"));
            Assert.That(_files[2].StartsWith("#test{background-color:blue;}"));
            Assert.That(_files[3].EndsWith("#test{background-color:white;}"));
        }
    }
}
