using Entities.Concretes;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IConfigurationService
    {
        Task<List<Configuration>> GetAllAsync();
        Task<Configuration?> GetByIdAsync(int id);
        Task AddAsync(Configuration config);
        Task UpdateAsync(Configuration config);
        Task DeleteAsync(int id);
        T GetValue<T>(string key);
    }
}
