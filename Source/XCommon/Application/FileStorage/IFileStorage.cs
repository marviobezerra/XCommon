namespace XCommon.Application.FileStorage
{
    public interface IFileStorage
    {
        bool Save(string container, string fileName, byte[] content, bool overRide = true);

        bool Save(string fileName, byte[] content, bool overRide = true);

        bool Exists(string fileName);

        bool Exists(string container, string fileName);

        bool Delete(string fileName);

        bool Delete(string container, string fileName);

        byte[] Load(string fileName);

        byte[] Load(string container, string fileName);
    }
}
