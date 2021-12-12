using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.Models
{
    public class DispositivoDatabaseSettings : IDispositivoDatabaseSettings
    {
        public string DispositivoCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }

    public interface IDispositivoDatabaseSettings
    {
        public string DispositivoCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
