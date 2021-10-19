using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP02.Models;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace TP02.DAO
{

    public class ProdutosDAO
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

        public void Adiciona(Produto produto)
        {
        }

        public IList<Produto> Lista()
        {
            open();

            IList<Produto> produtos = new List<Produto>();

            String sql = "select produto.id, produto.nome, produto.preco, produto.descricao, produto.qtd, categoria.nome " +
                         "from produto inner join categoria on categoria.id = produto.idCateg;";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            while (dr.Read())
            {
                produtos.Add(new Produto(dr.GetInt32(0), dr.GetInt32(4), dr.GetString(1), dr.GetString(5), dr.GetString(3), dr.GetFloat(2)));
            }

            close();

            return produtos;
        }
    }
}