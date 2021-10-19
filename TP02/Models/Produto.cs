using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP02.Models
{
    public class Produto
    {
        private int id, qtd;
        private String nome, categ, desc;
        private float preco;

        public Produto(int id, int qtd, String nome, String categ, String desc, float preco)
        {
            this.id = id;
            this.qtd = qtd;
            this.nome = nome;
            this.categ = categ;
            this.desc = desc;
            this.preco = preco;
        }

        public int Id { get => id; set => id = value; }
        public int Qtd { get => qtd; set => qtd = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Categ { get => categ; set => categ = value; }
        public string Desc { get => desc; set => desc = value; }
        public float Preco { get => preco; set => preco = value; }
                
    }
}