using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.DAO
{
    public static class ConexaoBD
    {
        /// <summary>
        /// Método Estático que retorna um conexao aberta com o BD
        /// </summary>
        /// <returns>Conexão aberta</returns>
        public static SqlConnection GetConexao()
        {
            //string strCon = "Data Source=LOCALHOST; Database=Hefestos; integrated security=true";
            //string strCon = "Data Source=LOCALHOST\\SQLEXPRESS; Database=Hefestos; integrated security=true";
            string strCon = "Data Source=SQL5097.site4now.net;Initial Catalog=db_a7c6f8_hefesto;User Id=db_a7c6f8_hefesto_admin;Password=HefestoEc5*";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
