namespace TestA.Wrappers
{
    public interface IFileWrapper
    {
        bool Exists(string path);
        string[] ReadAllLines(string path);
        string GetFileNameWithoutExtension(string path);
    }
}