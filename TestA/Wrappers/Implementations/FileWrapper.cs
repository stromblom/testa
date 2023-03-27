namespace TestA.Wrappers.Implementations
{
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
        
        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }
        
        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
    }
}