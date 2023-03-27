using TestA.Enums;
using TestA.Runners;
using TestA.Wrappers.Implementations;

var commandRunner = new CommandRunner(new FileWrapper());

var result = commandRunner.CountFileNameOccurrences(args);

switch (result.ValidationResult)
{
    case ValidationResult.ArgumentMissing:
        Console.WriteLine("Program takes one argument, and only one argument :)");
        return;
    case ValidationResult.NotFound:
        Console.WriteLine("File does not exist");
        return;
    case ValidationResult.Success:
        Console.WriteLine($"Term found '{result.Occurrences}' time(s)");
        return;
}