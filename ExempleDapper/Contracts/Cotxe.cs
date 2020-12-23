using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExempleDapper.Contracts
{
    public class Cotxe
    {
        public int Id { get; set; }
        public Model Model { get; set; }
        public string Matricula { get; set; }
        public DateTime Any { get; set; }
    }
}
