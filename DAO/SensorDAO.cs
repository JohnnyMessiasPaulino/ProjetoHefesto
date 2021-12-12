using ProjetoHefesto.DATA;
using ProjetoHefesto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.DAO
{
    public class SensorDAO : PadraoDAO<SensorViewModel>
    {
        private ObterDadosDispositivo getDados = new ObterDadosDispositivo();
      
        protected override SqlParameter[] CriaParametros(SensorViewModel model)
        {
            model.Temperatura = GetTemperatura(model);
            model.Umidade = GetUmidade(model);
            model.DataAtualizacao = DateTime.Now.ToString();

            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("Id", model.Id);
            parametros[1] = new SqlParameter("fazendaID", model.FazendaID);
            parametros[2] = new SqlParameter("temperatura", model.Temperatura);
            parametros[3] = new SqlParameter("umidade", model.Umidade);
            parametros[4] = new SqlParameter("localizacao", model.Localizacao);
            parametros[5] = new SqlParameter("dataAtualizacao", model.DataAtualizacao);
            parametros[6] = new SqlParameter("condicao", model.Condicao);
            return parametros;
        }

        protected override SensorViewModel MontaModel(DataRow registro)
        {
            SensorViewModel sensor = new SensorViewModel();
            sensor.Id = Convert.ToInt32(registro["Id"]);
            sensor.FazendaID = Convert.ToInt32(registro["fazendaID"]);
            sensor.Temperatura = Convert.ToDouble(registro["temperatura"]);
            sensor.Umidade = Convert.ToDouble(registro["umidade"]);
            sensor.Localizacao = registro["localizacao"].ToString();
            sensor.DataAtualizacao = registro["dataAtualizacao"].ToString();
            sensor.Condicao = Convert.ToChar(registro["condicao"]);
            return sensor;
        }

        protected override void SetTabela()
        {
            Tabela = "Sensor";
        }

        private double GetTemperatura(SensorViewModel sensor)
        {
            List <DispositivoViewModel> dispositivos = new List<DispositivoViewModel>();
            dispositivos = getDados.MontaDispositivos(getDados.ObterDados());

            double temperatura = -1;

            foreach (DispositivoViewModel d in dispositivos)
            {
                if (sensor.Id == Convert.ToInt32(d.Id.Substring(d.Id.Length - 3, 3)))
                {
                    temperatura = Convert.ToDouble(d.Temperature.Value);
                }
            }

            return temperatura;
        }

        private double GetUmidade(SensorViewModel sensor)
        {
            List<DispositivoViewModel> dispositivos = new List<DispositivoViewModel>();
            dispositivos = getDados.MontaDispositivos(getDados.ObterDados());

            double umidade = -1;

            foreach (DispositivoViewModel d in dispositivos)
            {
                if (sensor.Id == Convert.ToInt32(d.Id.Substring(d.Id.Length - 3, 3)))
                {
                    umidade = Convert.ToDouble(d.Humidity.Value);
                }
            }

            return umidade;
        }

        public List<SensorViewModel> ConsultaAvancadaSensor(int fazendaID, string localizacao)
        {
            var p = new SqlParameter[]
           {
                new SqlParameter("fazendaID", fazendaID),
                new SqlParameter("localizacao", localizacao)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancada_Sensor", p);
            List<SensorViewModel> lista = new List<SensorViewModel>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }

    }
}
