using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ProjetoHefesto.Models;

namespace ProjetoHefesto.DAO
{
    public class ContaDAO
    {
        private SqlParameter[] CriaParametros(ContaViewModel conta)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("usuario", conta.Usuario);
            parametros[1] = new SqlParameter("senha", conta.Senha);
            return parametros;
        }

        public void Inserir(ContaViewModel conta)
        {
            HelperDAO.ExecutaProc("spInsert_Conta", CriaParametros(conta));
        }

        public void Excluir(string usuario)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("usuario", usuario)
            };

            HelperDAO.ExecutaProc("sp_ExcluiConta", p);

        }

        public ContaViewModel Consulta(string usuario, string senha)
        {
            var p = new SqlParameter[]
             {
             new SqlParameter("usuario", usuario),
             new SqlParameter("senha", senha)
             };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsulta_Conta", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaConta(tabela.Rows[0]);

        }

        public static ContaViewModel MontaConta(DataRow registro)
        {
            ContaViewModel conta = new ContaViewModel();
            conta.Usuario = registro["usuario"].ToString();
            conta.Senha = registro["senha"].ToString();
            return conta;
        }
        /*

        public int ProximoId()
        {
            var p = new SqlParameter[]
             {
             new SqlParameter("tabela", "conta")
             };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spProximoId", p);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);

        }

        */
    }
}
