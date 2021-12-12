using Microsoft.AspNetCore.Mvc;
using ProjetoHefesto.DAO;
using ProjetoHefesto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoHefesto.DATA;

namespace ProjetoHefesto.Controllers
{

    public class PadraoController<T> : Controller where T : PadraoViewModel
    {
        protected PadraoDAO<T> DAO { get; set; }
        protected bool GeraProximoId { get; set; }
        protected string NomeViewIndex { get; set; } = "index";
        protected string NomeViewForm { get; set; } = "form";
        protected bool ExigeAutenticacao { get; set; } = false;
        public virtual IActionResult Index()
        {
            try
            {
                var lista = DAO.Listagem();
                return View(NomeViewIndex, lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public virtual IActionResult Create(int id)
        {

            try
            {

                //Verificar se o tipo criado é um sensor

                T model = Activator.CreateInstance(typeof(T)) as T;
                if (model is SensorViewModel)
                {
                    ObterDadosDispositivo d = new ObterDadosDispositivo();
                    int numeroDispositivosHelix = d.ObtemNumeroDeDispositivos();

                    // Verificar se é necessario criar um dispositivo no helix para o sensor
                    if (numeroDispositivosHelix < DadosEstaticos.sensoresEstaticos.Count)
                    {
                        CreateDispositivo dis = new CreateDispositivo();
                        dis.CriarDispositivo(numeroDispositivosHelix + 1);
                    }

                    ViewBag.Operacao = "I";
                    (model as SensorViewModel).FazendaID = id;
                    DadosEstaticos.IdParaLinkar = -1;
                    DadosEstaticos.IdParaLinkar = id;
                    PreencheDadosParaView("I", model);
                    return View(NomeViewForm, model);
                }
                else if (model is FazendaViewModel)
                {
                    DadosEstaticos.IdParaLinkar = -1;
                    DadosEstaticos.IdParaLinkar = id;
                    (model as FazendaViewModel).ProprietarioID = id;
                    ViewBag.Operacao = "I";
                    PreencheDadosParaView("I", model);
                    return View(NomeViewForm, model);
                }
                else
                {
                    ViewBag.Operacao = "I";
                    PreencheDadosParaView("I", model);
                    return View(NomeViewForm, model);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }


        protected virtual void PreencheDadosParaView(string Operacao, T model)
        {
            if (GeraProximoId && Operacao == "I")

                model.Id = DAO.ProximoId();
        }
        public virtual IActionResult Save(T model, string Operacao)
        {
            try
            {
                ValidaDados(model, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    return View(NomeViewForm, model);
                }
                else
                {
                    if (Operacao == "I")
                        DAO.Insert(model);
                    else
                        DAO.Update(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        protected virtual void ValidaDados(T model, string operacao)
        {
            ModelState.Clear();
            if (operacao == "I" && DAO.Consulta(model.Id) != null)
                ModelState.AddModelError("Id", "Código já está em uso!");
            if (operacao == "A" && DAO.Consulta(model.Id) == null)
                ModelState.AddModelError("Id", "Este registro não existe!");
            if (model.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
        }
        public IActionResult Edit(int id, int id2)
        {
            try
            {
                DadosEstaticos.IdParaLinkar = -1;
                DadosEstaticos.IdParaLinkar = id;
                DadosEstaticos.IdParaLinkar2 = -1;
                DadosEstaticos.IdParaLinkar2 = id2;
                ViewBag.Operacao = "A";
                var model = DAO.Consulta(id);
                if (model == null)
                    return RedirectToAction(NomeViewIndex);
                else
                {
                    PreencheDadosParaView("A", model);
                    return View(NomeViewForm, model);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                DAO.Delete(id);

                
                T model = Activator.CreateInstance(typeof(T)) as T;
                if (model is ProprietarioViewModel)
                {
                    return RedirectToAction("Index","Proprietario");
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (ExigeAutenticacao && !HelperControllers.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }
    }
}

