using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP03.Models;

namespace TP03.DAO
{
    public class ContainerDAO : DAO
    {
        public void adiciona(Container container)
        {
            open();

            String sql = $"insert into container (tipo, tamanho, numBL) values ('{container.Tipo}', {container.Tamanho}, {container.NumBL});";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public IList<Container> lista()
        {
            open();

            IList<Container> containers = new List<Container>();

            String sql = "select * from container;";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            while (dr.Read())
            {
                containers.Add(new Container(dr.GetInt32(0), dr.GetString(1), dr.GetFloat(2), dr.GetInt32(3)));
            }

            close();

            return containers;
        }

        public void atualiza(Container container)
        {
            open();

            String sql = $"update container set tipo = '{container.Tipo}', tamanho = '{container.Tamanho}', numBL = '{container.NumBL}' where num = {container.Num};";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public void exclui(int id)
        {
            open();

            String sql = $"delete from container where num = {id};";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public Container buscaID(int id)
        {
            open();

            String sql = $"select * from container where num = {id};";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            dr.Read();

            Container container = new Container(dr.GetInt32(0), dr.GetString(1), dr.GetFloat(2), dr.GetInt32(3));

            close();
            
            return container;            
        }
    }
}