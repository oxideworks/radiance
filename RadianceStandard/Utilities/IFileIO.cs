namespace RadianceStandard.Utilities
{
    public interface IFileIO
    {
        void WriteFile(string path, string data);
        string ReadFile(string path);
    }
}
