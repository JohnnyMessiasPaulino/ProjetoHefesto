using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.Models
{
    public class DispositivoViewModel
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public Humidity Humidity { get; set; }

        public Temperature Temperature { get; set; }

    }
}
