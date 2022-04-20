using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LogicaNegocios
    {
        public Random r = new Random();
        public List<Persona> Destinatarios = new List<Persona>();
        public List<Repartidor> Repartidores = new List<Repartidor>();
        public List<Envio> Envios = new List<Envio>();
        public Persona BuscarDestinatario(int dni)
        {
            Persona persona = Destinatarios.Find(x => x.Dni == dni);
            return persona;
        }
        public Envio BuscarEnvio(string codigoSeguimiento)
        {
            Envio envio = Envios.Find(x => x.Numero == codigoSeguimiento);
            return envio;
        }
        public short CargarNuevoPedido(int dniDestinatario,DateTime fechaEstimada,string descripcion,double precio)
        {
            Persona destinatario = BuscarDestinatario(dniDestinatario);
            if (destinatario == null)
            {
                return 400;
            }
            else
            {
                Envio nuevoEnvio = new Envio(r.Next().ToString(), destinatario, fechaEstimada, descripcion,precio);
                return 201;
            }
        }
        public bool ActualizarEnvio(string codigoSeguimiento, estadosEnvios estado)
        {
            Envio envioActualizar = BuscarEnvio(codigoSeguimiento);
            if (envioActualizar == null)
            {
                throw new Exception("Envio no encontrado");
            }
            else
            {
                if (estado.CompareTo(envioActualizar.Estado) <= 0)
                {
                    throw new Exception("Estado aneterior al actual");
                }
                else
                {
                    envioActualizar.Estado = estado;
                    if (estado == estadosEnvios.Entregado)
                    {
                        envioActualizar.Repartidor.TotalGanado += (envioActualizar.Precio * envioActualizar.Repartidor.Comision) / 100;
                    }

                    return true;
                }
            }
            
        }
        public static double CalcularDistancia(this Posicion posicionRepartidor,Posicion posicionDestinatario)
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
