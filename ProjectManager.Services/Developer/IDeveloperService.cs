using ProjectManager.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.Services.Developer
{
    public interface IDeveloperService
    {
        Task<DeveloperEntity> GetById(int developerId);
        Task<List<DeveloperEntity>> GetAll();
        Task Add(string firstName, string lastName, string eMail, string phone);
        Task<DeveloperEntity> DeleteEntry(int developerId);
        Task<DeveloperEntity> EditEntry(int developerId, string firstName, string lastName, string eMail, string phone);
        string CheckIfEmailExists(string eMail);
    }
}
