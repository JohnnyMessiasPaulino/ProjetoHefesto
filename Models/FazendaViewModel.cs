using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.Models
{
    public class FazendaViewModel : PadraoViewModel
    {
        /*
        public int Id { get; set; }
        */

        public string FazendaNome { get; set; }

        public int ProprietarioID { get; set; }

        public int Tamanho { get; set; }

        public int NumeroSensores { get; set; }

        public string Endereco { get; set; }
    }
}
