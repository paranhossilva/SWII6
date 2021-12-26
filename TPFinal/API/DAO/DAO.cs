using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace API.DAO
{
    public abstract class DAO
    {
        protected MySqlConnection conexao;
        protected MySqlCommand comando;
        protected MySqlDataReader dr;
        protected String sql;

        protected void open()
        {
            conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            conexao.Open();
        }

        protected void close()
        {
            conexao.Close();
        }
    }
}