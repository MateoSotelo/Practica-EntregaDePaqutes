using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Persona
    {
        public int Dni { get; set; }
        public string NombreApellido { get; set; }
        public short CP { get; set; }
        public Posicion Posicion { get; set; }
        public int Telefono { get; set; }
    }
}
