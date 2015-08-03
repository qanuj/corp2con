using System.Threading.Tasks;

namespace Talent21.Service.Abstraction
{
    public interface IDemoDataService
    {
        Task<string> BuildAsync();
        void BuildMaster();
    }
}