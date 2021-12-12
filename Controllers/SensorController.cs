using Microsoft.AspNetCore.Mvc;
using ProjetoHefesto.DAO;
using ProjetoHefesto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjetoHefesto.DATA;
using RestSharp;

namespace ProjetoHefesto.Controllers
{
    public class SensorController : PadraoController<SensorViewModel>
    {
        public SensorController()
        {
            DAO = new SensorDAO();
            GeraProximoId = true;
            NomeViewForm = "FormSensor";
            NomeViewIndex = "ListagemDeSensor";
        }

        protected override void ValidaDados(SensorViewModel model, string operacao)
        {

            base.ValidaDados(model, operacao);
            if (model.FazendaID <= 0)
                ModelState.AddModelError("FazendaID", "Id inválido!");

        }

        public IActionResult Update(int id)
        {
            SensorViewModel sensor = new SensorViewModel();
            sensor = DAO.Consulta(id);
            DAO.Update(sensor);

            List<SensorViewModel> sensores = new List<SensorViewModel>();

            sensores = DAO.Listagem();

            return View("ListagemDeSensor", sensores);
        }

        public List<SensorViewModel> Listagem()
        {

            List<SensorViewModel> sensores = new List<SensorViewModel>();

            sensores = DAO.Listagem();

            return sensores;
        }

        public void UpdateExterno(int id)
        {
            SensorViewModel sensor = new SensorViewModel();
            sensor = DAO.Consulta(id);
            DAO.Update(sensor);
        }

        public IActionResult ExibeConsultaSensor()
        {
            try
            {
                return View("ConsultaSensor");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }
        }

        public IActionResult ObtemDadosConsultaSensor(int fazendaID,
                                                        string localizacao)
        {
            try
            {
                SensorDAO dao = new SensorDAO();
                if (string.IsNullOrEmpty(localizacao))
                    localizacao = "";
                var lista = dao.ConsultaAvancadaSensor(fazendaID, localizacao);
                return PartialView("pvGridSensor", lista);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }
    }
}
