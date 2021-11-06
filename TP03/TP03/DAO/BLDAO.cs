using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP03.Models;

namespace TP03.DAO
{
    public class BLDAO : DAO
    {
        public void adiciona(BL bl)
        {
            open();

            String sql = $"insert into bl (consignee, navio) values ({bl.Consignee}, '{bl.Navio}');";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public IList<BL> lista()
        {
            open();

            IList<BL> bls = new List<BL>();

            String sql = "select * from bl;";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            while (dr.Read())
            {
                bls.Add(new BL(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2)));
            }

            close();

            return bls;
        }

        public void atualiza(BL bl)
        {
            open();

            String sql = $"update bl set consignee = '{bl.Consignee}', navio = '{bl.Navio}' where num = {bl.Num};";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public void exclui(int id)
        {
            open();

            String sql = $"delete from bl where num = {id};";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public BL buscaID(int id)
        {
            open();

            String sql = $"select * from bl where num = {id};";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            dr.Read();

            BL bl = new BL(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2));

            close();

            return bl;
        }
    }
}