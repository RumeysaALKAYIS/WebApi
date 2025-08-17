using Business.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;

public class ConfigurationService : IConfigurationService
{
    private readonly ConfigDbContext _context;
    private readonly string _applicationName;

    public ConfigurationService(ConfigDbContext context, string applicationName)
    {
        _context = context;
        _applicationName = applicationName;
    }

    public async Task<List<Configuration>> GetAllAsync()
    {
        return await _context.Configurations
            .Where(c => c.ApplicationName == _applicationName)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Configuration?> GetByIdAsync(int id)
    {
        return await _context.Configurations
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id && c.ApplicationName == _applicationName);
    }

    public async Task AddAsync(Configuration config)
    {
        config.ApplicationName = _applicationName;
        await _context.Configurations.AddAsync(config);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Configuration config)
    {
        _context.Configurations.Update(config);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var config = await _context.Configurations.FindAsync(id);
        if (config != null)
        {
            _context.Configurations.Remove(config);
            await _context.SaveChangesAsync();
        }
    }

    public T GetValue<T>(string key)
    {
        var config = _context.Configurations
            .AsNoTracking()
            .FirstOrDefault(c => c.Name == key && c.ApplicationName == _applicationName);

        if (config == null)
        {
            throw new KeyNotFoundException("Config not found");
        }

        try
        {
            return (T)Convert.ChangeType(config.Value, typeof(T));
        }
        catch (InvalidCastException)
        {
            throw new InvalidCastException("Config value type invalid");
        }
    }

}
