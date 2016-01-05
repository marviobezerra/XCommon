namespace XCommon.Application.FileStorage
{
    public interface IFileStorage
    {
        bool Save(string container, string fileName, byte[] content);
        bool Save(string fileName, byte[] content);
        bool Exists(string fileName);
        bool Exists(string container, string fileName);
        bool DeleteIfExists(string fileName);
        bool DeleteIfExists(string container, string fileName);
        bool Delete(string fileName);
        bool Delete(string container, string fileName);
        byte[] Load(string fileName);
        byte[] Load(string container, string fileName);
        byte[] LoadIfExists(string fileName);
    }
}
