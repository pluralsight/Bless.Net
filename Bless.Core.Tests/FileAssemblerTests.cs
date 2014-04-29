using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using NUnit.Framework;
using System.Linq;

namespace Bless.Core.Tests
{
    [TestFixture]
    class FileAssemblerTests
    {
        private FileAssembler assembler;
        [SetUp]
        public void SetUp()
        {
            assembler = new FileAssembler();
        }

        [TestCase("file1", "file2", "file3")]
        [TestCase("file1", "file2", "file3", "file4", "file5")]
        [TestCase("file1")]
        public void GeneratesTheRightNumberOfFiles(params string[] files)
        {
            var result = assembler.BuildFiles(files, "output.txt");
            Assert.That(result.Count(), Is.EqualTo(files.Length));
        }

        [Test]
        public void AddsToBaseFileNameWhileKeepingExtension()
        {
            var files = new List<string> {"file1", "file2"};
            var result = assembler.BuildFiles(files, "output.txt");
            Assert.That(result.Keys.First().StartsWith("output"));
            Assert.That(result.Keys.First().EndsWith(".txt"));
        }

        [Test]
        public void CreatesFileWithTheBaseName()
        {
            var files = new List<string> { "file1", "file2" };
            var result = assembler.BuildFiles(files, "output.txt");
            Assert.That(result.Keys, Contains.Item("output.txt"));
        }

        [Test]
        public void FileWithTheBaseNameIsTheLastFile()
        {
            var files = new List<string> {"file1", "file2"};
            var result = assembler.BuildFiles(files, "output.css");
            Assert.That(result["output.css"], Is.EqualTo("file2"));
        }

        [Test]
        public void AddsImportsToTheBaseFile()
        {
            var assembledFiles = new List<string>
            {
                "output1.css",
                "output2.css"
            };
            var modifiedFile = assembler.AddImportsToBaseFile(assembledFiles, "css goes here");
            Assert.That(modifiedFile, Contains.Substring("@import url('output1.css')"));
            Assert.That(modifiedFile, Contains.Substring("@import url('output2.css')"));
            Assert.That(modifiedFile, Contains.Substring("css goes here"));
        }


        [Test]
        public void AddsImportsInTheCorrectOrder()
        {
            var assembledFiles = new List<string>
            {
                "output1.css",
                "output2.css"
            };
            var modifiedFile = assembler.AddImportsToBaseFile(assembledFiles, "css goes here");

            var output1Index = modifiedFile.IndexOf("@import url('output1.css')");
            var output2Index = modifiedFile.IndexOf("@import url('output2.css')");
            var originalFileContentsIndex = modifiedFile.IndexOf("css goes here");

            Assert.That(output1Index, Is.EqualTo(0));
            Assert.That(output2Index, Is.GreaterThan(output1Index));
            Assert.That(originalFileContentsIndex, Is.GreaterThan(output2Index));
        }

    }
}
