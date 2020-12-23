using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExempleDapper.Contracts
{
    public class Ubicacio
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adreça { get; set; }
        public int Capacitat { get; set; }
    }
}
