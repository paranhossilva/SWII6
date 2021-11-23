using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public String Titulo { get; set; }
        public String Autor { get; set; }
        public String Categoria { get; set; }

        public Livro(int id, String titulo, String autor, String categoria)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            Categoria = categoria;
        }
    }
}