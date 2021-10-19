using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TP02.Models;

namespace TP02.DAO
{
    public class CategoriasDAO
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader dr;

        private void open()
        {
            conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            conexao.Open();
        }

        private void close() { conexao.Close(); }

        public void adiciona(Categoria categ)
        {
            open();

            String sql = $"insert into categoria (nome, descricao) values ('{categ.Nome}', '{categ.Desc}');";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public IList<Categoria> lista()
        {
            open();

            IList<Categoria> categs = new List<Categoria>();

            String sql = "select * from categoria;";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            while (dr.Read())
            {
                categs.Add(new Categoria(dr.GetInt32(0), dr.GetString(1), dr.GetString(2)));
            }

            close();

            return categs;
        }

        public Categoria buscaID(int id)
        {
            open();

            String sql = $"select * from categoria where id = {id};";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            dr.Read();

            Categoria categ = new Categoria(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));

            close();

            return categ;
        }

        public void atualiza(Categoria categ)
        {
            open();

            String sql = $"update categoria set nome = '{categ.Nome}', descricao = '{categ.Desc}' where id = {categ.Id};";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public void exclui(int id)
        {
            open();

            String sql = $"delete from categoria where id = {id};";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }
    }
}