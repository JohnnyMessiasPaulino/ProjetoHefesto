using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.Models
{
    public class ConfigGeralViewModel 
    //Não herda de PadraoViewModel pois tem contexto diferente
    {
        public string IpHelix { get; set; }

        public double TaxaAtualizacaoDashboard { get; set; }
    }
}
