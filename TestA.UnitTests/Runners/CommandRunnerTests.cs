using System;
using FakeItEasy;
using NUnit.Framework;
using TestA.Enums;
using TestA.Runners;
using TestA.Runners.Implementations;
using TestA.Wrappers;

namespace TestA.UnitTests.Runners
{
    public class CommandRunnerTests
    {

        private IFileWrapper _fileWrapper;
        private ICommandRunner _commandRunner;

        [SetUp]
        public void SetUp()
        {
            _fileWrapper = A.Fake<IFileWrapper>();
            _commandRunner = new CommandRunner(_fileWrapper);
        }

        [Test]
        public void CountFileNameOccurrences_WhenArgumentsEmpty_ReturnsArgumentMissingAndZeroOccurrences()
        {
            // Arrange
            const ValidationResult expectedValidationResult = ValidationResult.ArgumentMissing;
            const int expectedOccurrences = 0;

            // Act
            var result = _commandRunner.CountFileNameOccurrences(Array.Empty<string>());

            // Assert
            Assert.That(result.Occurrences, Is.EqualTo(expectedOccurrences));
            Assert.That(result.ValidationResult, Is.EqualTo(expectedValidationResult));
        }

        [Test]
        public void CountFileNameOccurrences_WhenArgumentsMoreThanOne_ReturnsArgumentMissingAndZeroOccurrences()
        {
            // Arrange
            const ValidationResult expectedValidationResult = ValidationResult.ArgumentMissing;
            const int expectedOccurrences = 0;

            // Act
            var result = _commandRunner.CountFileNameOccurrences(new[] { "Arg One", "Arg Two" });

            // Assert
            Assert.That(result.Occurrences, Is.EqualTo(expectedOccurrences));
            Assert.That(result.ValidationResult, Is.EqualTo(expectedValidationResult));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void CountFileNameOccurrences_WhenArgumentNullEmptyOrWhiteSpace_ReturnsArgumentMissingAndZeroOccurrences(
            string argument)
        {
            // Arrange
            const ValidationResult expectedValidationResult = ValidationResult.ArgumentMissing;
            const int expectedOccurrences = 0;

            // Act
            var result = _commandRunner.CountFileNameOccurrences(new[] { argument });

            // Assert
            Assert.That(result.Occurrences, Is.EqualTo(expectedOccurrences));
            Assert.That(result.ValidationResult, Is.EqualTo(expectedValidationResult));
        }

        [Test]
        public void CountFileNameOccurrences_WhenFileNotFound_ReturnsNotFoundAndZeroOccurrences()
        {
            // Arrange
            const ValidationResult expectedValidationResult = ValidationResult.NotFound;
            const int expectedOccurrences = 0;
            const string argument = "file/Path.txt";

            A.CallTo(() => _fileWrapper.Exists(argument)).Returns(false);
            
            // Act
            var result = _commandRunner.CountFileNameOccurrences(new[] { argument });

            // Assert
            Assert.That(result.Occurrences, Is.EqualTo(expectedOccurrences));
            Assert.That(result.ValidationResult, Is.EqualTo(expectedValidationResult));
            
            A.CallTo(() => _fileWrapper.Exists(argument)).MustHaveHappenedOnceExactly();
        }
        
        [Test]
        public void CountFileNameOccurrences_ReturnsSuccessAndCorrectAmountOfOccurrences()
        {
            // Arrange
            const ValidationResult expectedValidationResult = ValidationResult.Success;
            const int expectedOccurrences = 4;
            const string argument = "file/Path.txt";

            A.CallTo(() => _fileWrapper.Exists(argument)).Returns(true);
            A.CallTo(() => _fileWrapper.GetFileNameWithoutExtension(argument)).Returns("Path");
            A.CallTo(() => _fileWrapper.ReadAllLines(argument))
                .Returns(new[]
                {
                    "Path",
                    "Kebab is gut!", 
                    "Find me this Path", 
                    "Here is another one, Path", 
                    "Maybe here? Path",
                    "But not here: path"
                });
            
            // Act
            var result = _commandRunner.CountFileNameOccurrences(new[] { argument });

            // Assert
            Assert.That(result.Occurrences, Is.EqualTo(expectedOccurrences));
            Assert.That(result.ValidationResult, Is.EqualTo(expectedValidationResult));
            
            A.CallTo(() => _fileWrapper.Exists(argument)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileWrapper.GetFileNameWithoutExtension(argument)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileWrapper.ReadAllLines(argument)).MustHaveHappenedOnceExactly();
        }
        
        [Test]
        public void CountFileNameOccurrences_ReturnsSuccessAndZeroOccurrences()
        {
            // Arrange
            const ValidationResult expectedValidationResult = ValidationResult.Success;
            const int expectedOccurrences = 0;
            const string argument = "file/Path.txt";

            A.CallTo(() => _fileWrapper.Exists(argument)).Returns(true);
            A.CallTo(() => _fileWrapper.GetFileNameWithoutExtension(argument)).Returns("Path");
            A.CallTo(() => _fileWrapper.ReadAllLines(argument)).Returns(Array.Empty<string>());
            
            // Act
            var result = _commandRunner.CountFileNameOccurrences(new[] { argument });

            // Assert
            Assert.That(result.Occurrences, Is.EqualTo(expectedOccurrences));
            Assert.That(result.ValidationResult, Is.EqualTo(expectedValidationResult));
            
            A.CallTo(() => _fileWrapper.Exists(argument)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileWrapper.GetFileNameWithoutExtension(argument)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileWrapper.ReadAllLines(argument)).MustHaveHappenedOnceExactly();
        }
    }
}