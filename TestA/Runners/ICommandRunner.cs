using TestA.Enums;

namespace TestA.Runners
{
    public interface ICommandRunner
    {
        (int Occurrences, ValidationResult ValidationResult) CountFileNameOccurrences(string[] arguments);
    }
}