using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.Models
{
    public class MonitoramentoViewModel
    {
        public string FazendaNome { get; set; }

        public double Temperatura { get; set; }

        public double Umidade { get; set; }

        public string DataAtualizacao { get; set; }

        public int ID { get; set; }
    }
}
