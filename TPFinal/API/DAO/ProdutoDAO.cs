using API.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DAO
{
    public class ProdutoDAO : DAO
    {
        public void Insert(Produto produto)
        {
            open();

            sql = $"insert into produto (nome, preco, sts, idCad) values ('{produto.Nome}', {produto.Preco}, {produto.Status}, {produto.IdCad});";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public List<Produto> SelectAll()
        {
            open();

            List<Produto> produtos = new List<Produto>();

            sql = $"select * from produto;";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            while (dr.Read())
            {
                produtos.Add(new Produto
                {
                    Id = dr.GetInt32(0),
                    Nome = dr.GetString(1),
                    Preco = dr.GetFloat(2),
                    Status = dr.GetBoolean(3),
                    IdCad = dr.GetInt32(4),
                    IdUp = dr.IsDBNull(5) ? 0 : dr.GetInt32(5)
                });
            }

            close();

            return produtos;
        }

        public Produto SelectID(int id)
        {
            open();

            sql = $"select * from produto where id = {id}";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            dr.Read();

            Produto produto = new Produto
            {
                Id = dr.GetInt32(0),
                Nome = dr.GetString(1),
                Preco = dr.GetFloat(2),
                Status = dr.GetBoolean(3),
                IdCad = dr.GetInt32(4),
                IdUp = dr.IsDBNull(5) ? 0 : dr.GetInt32(5)
            };

            close();

            return produto;
        }

        public void Update(Produto produto)
        {
            open();

            sql = $"update produto set nome = '{produto.Nome}', preco = {produto.Preco}, sts = {produto.Status}, idUp = {produto.IdUp} where id = {produto.Id};";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public void Delete(int id)
        {
            open();

            sql = $"delete from produto where id = {id};";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }
    }
}