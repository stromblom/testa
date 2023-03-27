using TestA.Enums;
using TestA.Wrappers;

namespace TestA.Runners.Implementations
{
    public class CommandRunner : ICommandRunner
    {
        private readonly IFileWrapper _fileWrapper;

        public CommandRunner(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }
        
        public (int Occurrences, ValidationResult ValidationResult) CountFileNameOccurrences(string[] arguments)
        {
            if (arguments.Length != 1 || string.IsNullOrWhiteSpace(arguments[0]))
            {
                return (0, ValidationResult.ArgumentMissing);
            }

            var filePath = arguments[0];

            if (!_fileWrapper.Exists(filePath))
            {
                return (0, ValidationResult.NotFound);
            }

            var fileNameWithoutExtension = _fileWrapper.GetFileNameWithoutExtension(filePath);

            var linesInFile = _fileWrapper.ReadAllLines(filePath);

            var counter = CountOccurrences(fileNameWithoutExtension, linesInFile);

            return (counter, ValidationResult.Success);
        }

        private int CountOccurrences(string term, string[] linesInFile)
        {
            var counter = 0;
            foreach (var lineInFile in linesInFile)
            {
                var wordsInLine = lineInFile
                    .Replace(",", string.Empty)
                    .Replace(".", string.Empty)
                    .Replace(":", string.Empty)
                    .Replace(";", string.Empty)
                    .Replace("!", string.Empty)
                    .Replace("?", string.Empty)
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
                foreach (var wordInLine in wordsInLine)
                {
                    if (wordInLine != term)
                    {
                        continue;
                    }

                    counter++;
                }
            }

            return counter;
        }
    }
}