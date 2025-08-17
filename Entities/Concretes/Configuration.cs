using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Configuration : BaseEntity
    {
        public string Name { get; set; }
        public ConfigValueType Type { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string ApplicationName { get; set; }
    }
    public enum ConfigValueType
    {
        String = 1,
        Int = 2,
        Bool = 3,
        Double = 4,
        DateTime = 5
    }
}
