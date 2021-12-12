using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjetoHefesto.DAO;
using ProjetoHefesto.DAOMONGODB;
using ProjetoHefesto.DATA;
using ProjetoHefesto.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProjetoHefesto.Controllers
{
    public class ContaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            try
            {
                ContaViewModel conta = new ContaViewModel();
                return View("Login", conta);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult FazLogin(string usuario, string senha)
        {
            ContaDAO dao = new ContaDAO();
            ContaViewModel conta = new ContaViewModel();
            DadosEstaticos.UsuarioAtualLogado = "";
            DadosEstaticos.UsuarioAtualLogado = usuario;
            conta.Usuario = usuario;
            conta.Senha = senha;
            if (dao.Consulta(usuario, senha) != null)
            {
                HttpContext.Session.SetString("Logado", "true");
                //return View("Login", conta);
                return RedirectToAction("Acesso", "PaginaAposLogin");
            }
            else
            {
                ViewBag.Erro = "Usuário ou senha inválidos!";
                return View("Login", conta);
            }
        }
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult CriarNovaConta()
        {
            try
            {
                ContaViewModel conta = new ContaViewModel();

                return View("CadastroConta", conta);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult SalvarNovaConta(ContaViewModel conta)
        {
            try
            {
                ValidaDados(conta);
                if (ModelState.IsValid == false)
                {
                    return View("CadastroConta", conta);
                }
                else
                {
                    ContaDAO dao = new ContaDAO();
                    dao.Inserir(conta);
                    DadosEstaticos.UsuarioAtualLogado = conta.Usuario;

                    // return RedirectToAction("Login");
                    return RedirectToAction("Create", "Proprietario");
                }

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }


        private void ValidaDados(ContaViewModel conta)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            ContaDAO dao = new ContaDAO();

            if (dao.Consulta(conta.Usuario, conta.Senha) != null)
                ModelState.AddModelError("Usuario", "Login já está em uso.");

            if (conta.Usuario.Length <= 0)
                ModelState.AddModelError("Usuario", "Digite um login");

        }

    }
}
