using Microsoft.AspNetCore.Mvc;
using ProjetoHefesto.DAO;
using ProjetoHefesto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProjetoHefesto.Controllers
{
    public class FazendaController : PadraoController<FazendaViewModel>
    {
        
        public FazendaController()
        {
            DAO = new FazendaDAO();
            GeraProximoId = true;
            NomeViewForm = "FormFazenda";
            NomeViewIndex = "ListagemDeFazenda";
            
        }
        protected override void ValidaDados(FazendaViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            if (string.IsNullOrEmpty(model.Endereco))
                ModelState.AddModelError("Endereco", "Campo obrigatório!");

            if (model.ProprietarioID <= 0)
                ModelState.AddModelError("ProprietarioID", "Proprietario inválido!");

            if (model.FazendaNome.Length <= 0)
                ModelState.AddModelError("FazendaNome", "Insira um nome para a Fazenda");
           
            if (model.Tamanho <= 0)
                ModelState.AddModelError("Tamanho", "Campo iválido");
        }


        //Havia o método PreencheDadosParaView que podemos fazer override, caso faça-se uma caixa combo


        public IActionResult ExibeConsultaAvancada()
        {
            try
            {
                return View("ConsultaFazenda");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }
        }

        public IActionResult ObtemDadosConsultaAvancada(string fazendaNome,
                                                        int tamanhoInicial,
                                                          int tamanhoFinal,
                                                        int numeroSensores)
        {
            try
            {
                FazendaDAO dao = new FazendaDAO();
                if (string.IsNullOrEmpty(fazendaNome))
                    fazendaNome = "";
                var lista = dao.ConsultaAvancada(fazendaNome,tamanhoInicial, tamanhoFinal,numeroSensores);
                return PartialView("pvGridFazenda", lista);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }

        public List<FazendaViewModel> Listagem()
        {

            List<FazendaViewModel> fazendas = new List<FazendaViewModel>();

            fazendas = DAO.Listagem();

            return fazendas;
        }
    }
}
