using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP02.Models
{
    public class Usuario
    {
        private int id;
        private String nome, senha;

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}