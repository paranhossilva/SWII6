using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TP01
{
    class Conn
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private static String banco = "server=localhost;uid=root;password=root;database=TP01";
        private Books books = new Books();
        private Authors authors = new Authors();
        private MySqlDataReader dr;
        private List<int> id = new List<int>();

        private void open()
        { 
            conexao = new MySqlConnection(banco);
            conexao.Open();
        }

        private void close() { conexao.Close(); }

        public Authors autor()
        {
            open();

            comando = new MySqlCommand("SELECT nome, email, sexo FROM autor;", conexao);
            dr = comando.ExecuteReader();

            while (dr.Read())
            {
                authors.Lista.Add(new Author(dr.GetString(0), dr.GetString(1), dr.GetChar(2)));
            }

            close();

            return authors;
        }

        public Books livros()
        {
            open();

            comando = new MySqlCommand("SELECT * FROM livro", conexao);
            dr = comando.ExecuteReader();

            while (dr.Read())
            {
                id.Add(dr.GetInt32(0));

                books.Lista.Add(new Book(dr.GetString(1), dr.GetDouble(2), dr.GetInt32(3)));
            }

            for (int i = 0; i < id.Count; i++)
            {
                dr.Close();

                comando = new MySqlCommand("select autor.nome, autor.email, autor.sexo from autor " +
                                           "inner join autorlivro on autor.id = idAutor " +
                                           "inner join livro on livro.id = idLivro " +
                                           "where idlivro = " + id[i] + "; ", conexao);

                dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    books.Lista[i].Authors.Add(new Author(dr.GetString(0), dr.GetString(1), dr.GetChar(2)));
                }
            }

            close();
            return books;
        }
    }
}
