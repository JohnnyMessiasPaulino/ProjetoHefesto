using Microsoft.AspNetCore.Mvc;
using ProjetoHefesto.DAOMONGODB;
using ProjetoHefesto.DATA;
using ProjetoHefesto.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjetoHefesto.DAO;

namespace ProjetoHefesto.Controllers
{
    public class MonitoramentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Monitoramento()
        {
            try
            {

                return View("Monitoramento");

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        /// <summary>
        /// Somente para teste para obter dados via Get, apagar depois
        /// </summary>
        /// <returns></returns>
        public IActionResult TestData()
        {
            try
            {
                ObterDadosDispositivo dis = new ObterDadosDispositivo();
                List<DispositivoViewModel> dispositivos = new List<DispositivoViewModel>();
                ConfigGeralViewModel cg = new ConfigGeralViewModel();

                cg.IpHelix = "35.199.78.175";

                IRestResponse response = dis.ObterDados();
                dispositivos = dis.MontaDispositivos(response);

                return View("TestarRecebimentoDados", dispositivos);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        /// <summary>
        /// Somente para teste para obter dados do MongoDB, apagar depois
        /// </summary>
        /// <returns></returns>
        public IActionResult TestMongoData()
        {
            try
            {
                ConexaoMongoDB mongo = new ConexaoMongoDB();
                List<DispositivoMongoViewModel> listaDispositivosObtidosMongo = new List<DispositivoMongoViewModel>();
                listaDispositivosObtidosMongo = mongo.MontaDadosRecebidosEmObjeto();

                return View("TestarRecebimentoDadosMongo", listaDispositivosObtidosMongo);

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Dashboard()
        {
            try
            {

                return View("Dashboard");

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult AtualizaDadosMonitoramentoAjax()
        {
            try
            {
                ObterDadosDispositivo dadosAtuais = new ObterDadosDispositivo();
                IRestResponse dados = dadosAtuais.ObterDados();
                DispositivoViewModel dadosDispositivo = new DispositivoViewModel();
                List<DispositivoViewModel> dadosDispositivos = new List<DispositivoViewModel>();

                dadosDispositivos = dadosAtuais.MontaDispositivos(dados);

                //string listaJson = JsonConvert.SerializeObject(dadosDispositivos);

                return PartialView("PartialView", dadosDispositivos);

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }

        public PartialViewResult AtualizarDashboard()
        {
            return PartialView("PartialViewDashboard");
        }

        public IActionResult AtualizarDadosMongoHistorico()
        {
            try
            {
                ConexaoMongoDB mongo = new ConexaoMongoDB();
                List<DispositivoMongoViewModel> listaDispositivosObtidosMongo = new List<DispositivoMongoViewModel>();
                listaDispositivosObtidosMongo = mongo.MontaDadosRecebidosEmObjeto();

                return PartialView("PartialViewDadosMongo", listaDispositivosObtidosMongo);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }

        public IActionResult MonitoramentoFazenda()
        {
            try
            {
                MonitoramentoDAO dao = new MonitoramentoDAO();
                List<MonitoramentoViewModel> lista = dao.Listagem();

                return View("MonitoramentoFazenda", lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }

        public IActionResult AtualizaDadosMonitoramentoFazenda()
        {
            try
            {
                MonitoramentoDAO dao = new MonitoramentoDAO();
                List<MonitoramentoViewModel> lista = dao.Listagem();
                UpdateSQLAutomatico();

                return PartialView("PartialViewMonitoramentoFazenda", lista);

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }

        /// <summary>
        /// Método para atualizar o SQL juntamente com o AJAX
        /// </summary>
        public void UpdateSQLAutomatico()
        {
            
            SensorController sc = new SensorController();

            List<SensorViewModel> sensores = new List<SensorViewModel>();

            sensores = sc.Listagem();

            int qtdSensores = sensores.Count;

            for(int i = 1; i <= qtdSensores; i++)
            {
                sc.UpdateExterno(i);
            }

        }

        public IActionResult ExibeConsultaMonitoramento()
        {
            try
            {
                return View("MonitoramentoFazenda");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }
        }

        public IActionResult ObtemDadosConsultaMonitoramento(string nomeFazenda,
                                                                        int Id)
        {
            try
            {
                MonitoramentoDAO dao = new MonitoramentoDAO();
                if (string.IsNullOrEmpty(nomeFazenda))
                    nomeFazenda = "";
                var lista = dao.ConsultaAvancadaMonitoramento(nomeFazenda, Id);
                return PartialView("PartialViewMonitoramentoFazenda", lista);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }


    }
}
