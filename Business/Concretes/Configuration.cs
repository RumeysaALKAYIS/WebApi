using Business.Constants;
using DataAccess.Concretes.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class Configuration
    {
        private readonly ConfigDbContext _context;
        private readonly string _applicationName;

        public Configuration(ConfigDbContext context, string applicationName)
        {
            _context = context;
            _applicationName = applicationName;
        }

        public T GetValue<T> (string key)
        {
            var config=_context.Configurations
                .AsNoTracking()
                .FirstOrDefault(c=>c.Name== key && c.ApplicationName == _applicationName && c.IsActive);

            if(config == null)
            {
                throw new KeyNotFoundException(Messages.ConfigNotFound);
            }
            string rawValue = config.Value;

            try
            {
                return (T)Convert.ChangeType(config.Value, typeof(T));
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(Messages.ConfigValueInvalid);
            }
        }
    }
}
