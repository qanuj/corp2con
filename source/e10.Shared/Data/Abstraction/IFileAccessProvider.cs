using System.Threading.Tasks;

namespace e10.Shared.Data.Abstraction
{
    public interface IFileAccessProvider
    {
        Task<FileAccessInfo> ByUrlAsync(string userId, string filepath);
    }
}