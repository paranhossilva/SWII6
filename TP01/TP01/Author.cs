using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01
{
    class Author
    {
        private String name, email;
        private char gender;

        public Author(string name, string email, char gender)
        {
            this.name = name;
            this.email = email;
            this.gender = gender;
        }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public char Gender { get => gender; set => gender = value; }
    }
}
