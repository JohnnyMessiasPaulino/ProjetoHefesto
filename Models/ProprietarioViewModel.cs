using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.Models
{
    public class ProprietarioViewModel : PadraoViewModel
    {
        /*
        public int Id { get; set; }
        */

        public string Usuario { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

    }
}
