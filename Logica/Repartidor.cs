using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Repartidor :Persona
    {
        public byte Comision { get; set; }
        public double TotalGanado { get; set; }
        public short CantidadEnvios { get; set; }
    }
}
