using Microsoft.AspNetCore.Mvc;
using ProjetoHefesto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjetoHefesto.DAO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetoHefesto.Controllers
{
    public class PaginaAposLoginController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Acesso(ContaViewModel conta)
        {
            try
            {
                UpdateEstaticos();
                conta.Usuario = DadosEstaticos.UsuarioAtualLogado;
                return View("PaginaAposLogin", conta);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        private void UpdateEstaticos()
        {
            DadosEstaticos.LimpaEstaticos();

            SensorController sc = new SensorController();
            DadosEstaticos.sensoresEstaticos = sc.Listagem();

            FazendaController fc = new FazendaController();
            DadosEstaticos.fazendasEstaticas = fc.Listagem();

            ProprietarioController pc = new ProprietarioController();
            DadosEstaticos.proprietariosEstaticos = pc.Listagem();
        }



    }
}
