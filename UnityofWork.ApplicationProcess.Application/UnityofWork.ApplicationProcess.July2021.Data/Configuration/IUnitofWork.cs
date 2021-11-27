using System.Threading.Tasks;
using UnityofWork.ApplicationProcess.July2021.Data.Services;

namespace UnityofWork.ApplicationProcess.July2021.Data.Configuration
{
    public interface IUnitofWork
    {
        IUserProfileRepository UserProfile { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
