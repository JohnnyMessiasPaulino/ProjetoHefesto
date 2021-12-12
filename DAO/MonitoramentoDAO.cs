using ProjetoHefesto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.DAO
{
    public class MonitoramentoDAO
    {

        public List<MonitoramentoViewModel> Listagem()
        {
            List<MonitoramentoViewModel> lista = new List<MonitoramentoViewModel>();

            DataTable tabela = HelperDAO.ExecutaProcSelect("spMonitoramento", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaMonitoramento(registro));
            return lista;
        }

        public static MonitoramentoViewModel MontaMonitoramento(DataRow registro)
        {
            MonitoramentoViewModel m = new MonitoramentoViewModel();
            m.FazendaNome = registro["fazendaNome"].ToString();
            m.Temperatura = Convert.ToDouble(registro["temperatura"]);
            m.Umidade = Convert.ToDouble(registro["umidade"]);
            m.DataAtualizacao = registro["dataAtualizacao"].ToString();
            m.ID = Convert.ToInt32(registro["id"]);
            return m;
        }

        public List<MonitoramentoViewModel> ConsultaAvancadaMonitoramento(string nomeFazenda, int Id)
        {
            var p = new SqlParameter[]
           {
                new SqlParameter("nomeFazenda", nomeFazenda),
                new SqlParameter("Id", Id)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancada_Monitoramento", p);
            List<MonitoramentoViewModel> lista = new List<MonitoramentoViewModel>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaMonitoramento(registro));

            return lista;
        }
    }
}
