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
    public static class Extension
    {
        public static double CalcularDistancia(this Posicion posicionRepartidor, Posicion posicionDestinatario)
        {
            double distance = 0;
            double EarthRadius = 63710000;
            double Lat = (posicionDestinatario.Latitud - posicionRepartidor.Latitud) * (Math.PI / 180);
            double Lon = (posicionDestinatario.Longitud - posicionRepartidor.Longitud) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(posicionRepartidor.Latitud * (Math.PI / 180)) * Math.Cos(posicionDestinatario.Latitud * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = EarthRadius * c;
            return distance;
        }
    }
}
