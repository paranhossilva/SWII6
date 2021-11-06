using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP03.Models
{
    public class Container
    {
        public int Num { get; set; }
        public String Tipo { get; set; }
        public float Tamanho { get; set; }
        public int NumBL { get; set; }

        public Container(int num, String tipo, float tamanho, int numBL)
        {
            Num = num;
            Tipo = tipo;
            Tamanho = tamanho;
            NumBL = numBL;
        }
    }
}