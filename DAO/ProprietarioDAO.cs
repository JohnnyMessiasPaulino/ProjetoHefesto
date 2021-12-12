using ProjetoHefesto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.DAO
{
    public class ProprietarioDAO : PadraoDAO<ProprietarioViewModel>
    {
      
        protected override SqlParameter[] CriaParametros(ProprietarioViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("Id", model.Id);
            parametros[4] = new SqlParameter("usuario", model.Usuario);
            parametros[1] = new SqlParameter("nome", model.Nome);
            parametros[2] = new SqlParameter("telefone", model.Telefone);
            parametros[3] = new SqlParameter("email", model.Email);
            
            return parametros;
        }

        protected override ProprietarioViewModel MontaModel(DataRow registro)
        {
            ProprietarioViewModel proprietario = new ProprietarioViewModel();
            proprietario.Id = Convert.ToInt32(registro["Id"]);
            proprietario.Usuario = registro["usuario"].ToString();
            proprietario.Nome = registro["nome"].ToString();
            proprietario.Telefone = registro["telefone"].ToString();
            proprietario.Email = registro["email"].ToString();
            
            return proprietario;
        }

        protected override void SetTabela()
        {
            Tabela = "Proprietario";
        }
    }
}
