using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Data.Services;

namespace Hahn.ApplicationProcess.July2021.Data.Configuration
{
    public interface IUnitofWork
    {
        IUserProfileRepository UserProfile { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
