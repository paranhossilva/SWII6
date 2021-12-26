using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Senha { get; set; }
        public bool Status { get; set; }
    }
}
