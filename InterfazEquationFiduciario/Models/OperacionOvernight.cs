using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfazEquationFiduciario.Models
{
    public class OperacionOvernight
    {
        public String Cuenta { get; set; }
        public long Operacion { get; set; }
        public byte StatusOp { get; set; }
    }
}
