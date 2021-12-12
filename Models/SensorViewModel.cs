using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.Models
{
    public class SensorViewModel : PadraoViewModel
    {
        /*
        public int Id { get; set; }
        */

        public int FazendaID { get; set; }

        public double Temperatura { get; set; }

        public double Umidade { get; set; }

        public string Localizacao { get; set; }

        public string DataAtualizacao { get; set; }

        public Char Condicao { get; set; }
    }
}
