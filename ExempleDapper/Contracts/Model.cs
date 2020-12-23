using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExempleDapper.Contracts
{
    public class Combustible
    {
        public int Id { get; set; }
        public string Codi { get; set; }
        public string Descripcio { get; set; }
    }

    public class Model
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Nom { get; set; }
        public int Any { get; set; }
        public Combustible Fuel { get; set; }
        public double Cilindrada { get; set; }
    }
}
