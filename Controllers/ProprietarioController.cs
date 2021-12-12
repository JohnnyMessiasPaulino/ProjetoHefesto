using Microsoft.AspNetCore.Mvc;
using ProjetoHefesto.DAO;
using ProjetoHefesto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetoHefesto.Controllers
{
    public class ProprietarioController : PadraoController<ProprietarioViewModel>
    {
        public ProprietarioController ()
        {
            DAO = new ProprietarioDAO();
            GeraProximoId = true;
            NomeViewForm = "FormProprietario";
            NomeViewIndex = "ListagemDeProprietarios";

        }

        protected override void PreencheDadosParaView(string Operacao, ProprietarioViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);

        }


        protected override void ValidaDados(ProprietarioViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            if(string.IsNullOrEmpty(model.Usuario))
                ModelState.AddModelError("usuario", "Campo obrigatório!");

            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("nome", "Nome deve ser preenchido!");

            if (string.IsNullOrEmpty(model.Telefone.ToString()))
                ModelState.AddModelError("telefone", "Telefone deve ser preenchido!");

            if (string.IsNullOrEmpty(model.Email))
                ModelState.AddModelError("email", "Email deve ser preenchido!");
        }

        public List<ProprietarioViewModel> Listagem()
        {

            List<ProprietarioViewModel> proprietarios = new List<ProprietarioViewModel>();

            proprietarios = DAO.Listagem();

            return proprietarios;
        }

    }


}

