using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExempleDapper.Contracts
{
    public class Situacio
    {
        public int Id { get; set; }
        public Cotxe Cotxe { get; set; }
        public Ubicacio Ubicacio { get; set; }
        public DateTime Data { get; set; }
        public bool Llogat { get; set; }
    }
}
