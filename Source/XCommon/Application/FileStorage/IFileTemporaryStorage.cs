
namespace XCommon.Application.FileStorage
{
    public interface IFileTemporaryStorage : IFileStorage
    {
        bool ClearAll();
    }
}
