using ProjetoHefesto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.DAO
{
    public class FazendaDAO : PadraoDAO<FazendaViewModel>
    {

        protected override SqlParameter[] CriaParametros(FazendaViewModel model)
        {
            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("fazendaID", model.Id);

            var tabela = HelperDAO.ExecutaProcSelect("spCount_numeroSensores", p);
            model.NumeroSensores = Convert.ToInt32(tabela.Rows[0][0]);

            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("Id", model.Id);
            parametros[1] = new SqlParameter("fazendaNome", model.FazendaNome);
            parametros[2] = new SqlParameter("proprietarioID", model.ProprietarioID);
            parametros[3] = new SqlParameter("tamanho", model.Tamanho);
            parametros[4] = new SqlParameter("endereco", model.Endereco);
            return parametros;
        }

        protected override FazendaViewModel MontaModel(DataRow registro)
        {
            FazendaViewModel fazenda = new FazendaViewModel();
            fazenda.Id = Convert.ToInt32(registro["Id"]);
            fazenda.FazendaNome = registro["fazendaNome"].ToString();
            fazenda.ProprietarioID = Convert.ToInt32(registro["proprietarioID"]);
            fazenda.Tamanho = Convert.ToInt32(registro["tamanho"]);
            fazenda.NumeroSensores = Convert.ToInt32(registro["numeroSensores"]);
            fazenda.Endereco = registro["endereco"].ToString();
            return fazenda;
        }

        protected override void SetTabela()
        {
            Tabela = "Fazenda";
        }

        public List<FazendaViewModel> ConsultaAvancada(string fazendaNome,int tamanhoInicial,int tamanhoFinal, int numeroSensores)
        {
            var p = new SqlParameter[]
           {
                new SqlParameter("fazendaNome", fazendaNome),
                new SqlParameter("tamanhoInicial", tamanhoInicial),
                new SqlParameter("tamanhoFinal", tamanhoFinal),
                new SqlParameter("numeroSensores", numeroSensores)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancada_Fazenda", p);
            List<FazendaViewModel> lista = new List<FazendaViewModel>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }
    }
}
