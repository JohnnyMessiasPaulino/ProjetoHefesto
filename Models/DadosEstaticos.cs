using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoHefesto.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.Models
{
    public static class DadosEstaticos
    {
        public static string UsuarioAtualLogado;
        public static int IdParaLinkar;
        public static int IdParaLinkar2;

        public static List<SensorViewModel> sensoresEstaticos = new List<SensorViewModel>();
        public static List<FazendaViewModel> fazendasEstaticas = new List<FazendaViewModel>();
        public static List<ProprietarioViewModel> proprietariosEstaticos = new List<ProprietarioViewModel>();

        public static void LimpaEstaticos()
        {
            try
            {
                if (sensoresEstaticos != null)
                    DadosEstaticos.sensoresEstaticos.Clear();

                if (fazendasEstaticas != null)
                    DadosEstaticos.fazendasEstaticas.Clear();

                if (proprietariosEstaticos != null)
                    DadosEstaticos.proprietariosEstaticos.Clear();
            }
            catch
            {
                Console.WriteLine("Estaticos nulos");
            }


        }
    }
}
