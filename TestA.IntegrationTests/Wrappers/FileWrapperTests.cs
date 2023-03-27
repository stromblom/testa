using NUnit.Framework;
using TestA.Wrappers.Implementations;

namespace TestA.IntegrationTests.Wrappers
{
    public class FileWrapperTests
    {
        private FileWrapper _fileWrapper;

        [SetUp]
        public void SetUp()
        {
            _fileWrapper = new FileWrapper();
        }

        [TestCase("FileDoesNotExist.txt", false)]
        [TestCase("FileDoesExist.txt", true)]
        public void Exists_ReturnsCorrectResult(string filePath, bool expectedResult)
        {
            // Act
            var result = _fileWrapper.Exists(filePath);
            
            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ReadAllLines_ReadsAllLines()
        {
            // Arrange
            const string filePath = "FileDoesExist.txt";
            var expectedResult = new[] { "This file exists and has data :)", "Loads of data!" };
            
            // Act
            var result = _fileWrapper.ReadAllLines(filePath);
            
            // Assert
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo(expectedResult[0]));
            Assert.That(result[1], Is.EqualTo(expectedResult[1]));
        }

        [TestCase("FileDoesExist.txt", "FileDoesExist")]
        [TestCase("this/is/a/path/FileDoesExist.txt", "FileDoesExist")]
        [TestCase("Kebab.txt", "Kebab")]
        [TestCase("Snuggles", "Snuggles")]
        public void GetFileNameWithoutExtension_ReturnsExpected(string filePath, string expectedResult)
        {
            // Act
            var result = _fileWrapper.GetFileNameWithoutExtension(filePath);
            
            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}