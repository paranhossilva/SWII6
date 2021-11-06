using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP03.Models
{
    public class BL
    {
        public int Num { get; set; }
        public int Consignee { get; set; }
        public String Navio { get; set; }

        public BL(int num, int consignee, string navio)
        {
            Num = num;
            Consignee = consignee;
            Navio = navio;
        }
    }
}