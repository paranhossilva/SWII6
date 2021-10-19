using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP02.Models
{
    public class Categoria
    {
        private int id;
        private String nome, desc;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Desc { get => desc; set => desc = value; }

        public Categoria(int id, String nome, String desc)
        {
            this.id = id;
            this.nome = nome;
            this.desc = desc;
        }

        public Categoria(String nome, String desc)
        {
            this.nome = nome;
            this.desc = desc;
        }
    }
}