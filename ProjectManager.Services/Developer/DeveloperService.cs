using Microsoft.EntityFrameworkCore;
using ProjectManager.Database;
using ProjectManager.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.Services.Developer
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IRepository<DeveloperEntity> _repository;

        public DeveloperService(IRepository<DeveloperEntity> repository)
        {
            _repository = repository;
        }

        public async Task Add(string firstName, string lastName, string eMail, string phone)
        {
            DeveloperEntity newRecord = new DeveloperEntity()
            {
                FirstName = firstName,
                LastName = lastName,
                EMail = eMail,
                Phone = phone,
            };

            _repository.Add(newRecord);
            _repository.SaveChanges();
        }

        public async Task<List<DeveloperEntity>> GetAll()
        {
            List<DeveloperEntity> developerList;
            developerList = await _repository.Entities.ToListAsync();
            return developerList;
        }

        public async Task<DeveloperEntity> GetById(int developerId)
        {
            var developer = await _repository.Entities.FirstOrDefaultAsync(e => e.Id == developerId);

            return developer;
        }

        public async Task<DeveloperEntity> DeleteEntry(int developerId)
        {
            DeveloperEntity entity = await _repository.Entities.FirstOrDefaultAsync(e => e.Id == developerId);
            _repository.Remove(entity);
            _repository.SaveChanges();
            return entity; /*TODO fix*/
        }

        public async Task<DeveloperEntity> EditEntry(int developerId, string firstName, string lastName, string eMail, string phone)
        {
            DeveloperEntity entity = new DeveloperEntity
            {
                Id = developerId,
                FirstName = firstName,
                LastName = lastName,
                EMail = eMail,
                Phone = phone,
            };

            _repository.Update(entity);
            _repository.SaveChanges();
            return entity;
        }
    }
}
