using API.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DAO
{
    public class UsuarioDAO : DAO
    {
        public void Insert(Usuario usuario)
        {
            open();

            sql = $"insert into usuario (nome, senha, sts) values('{usuario.Nome}', md5('{usuario.Senha}'), {usuario.Status});";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public List<Usuario> SelectAll()
        {
            open();

            List<Usuario> usuarios = new List<Usuario>();

            sql = "select id, nome, sts from usuario;";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            while (dr.Read())
            {
                usuarios.Add(new Usuario
                {
                    Id = dr.GetInt32(0),
                    Nome = dr.GetString(1),
                    Status = dr.GetBoolean(2)
                });
            }

            close();

            return usuarios;
        }

        public Usuario SelectID(int id)
        {
            open();

            sql = $"select id, nome, sts from usuario where id = {id};";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            Usuario usuario;

            if (dr.Read())
            {
                usuario = new Usuario
                {
                    Id = dr.GetInt32(0),
                    Nome = dr.GetString(1),
                    Status = dr.GetBoolean(2)
                };
            }
            else 
                usuario = null;

            close();

            return usuario;
        }

        public Usuario Login(String nome, String senha)
        {
            open();

            sql = $"select id, nome, sts from usuario where nome = '{nome}' and senha = '{senha}';";

            comando = new MySqlCommand(sql, conexao);
            dr = comando.ExecuteReader();

            dr.Read();

            Usuario usuario = new Usuario
            {
                Id = dr.GetInt32(0),
                Nome = dr.GetString(0),
                Status = dr.GetBoolean(0)
            };

            close();

            return usuario;
        }

        public void Update(Usuario usuario)
        {
            open();

            sql = $"update usuario set nome = '{usuario.Nome}', sts = {usuario.Status} where id = {usuario.Id};";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }

        public void Delete(int id)
        {
            open();

            sql = $"delete from usuario where id = {id}";

            comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            close();
        }
    }
}