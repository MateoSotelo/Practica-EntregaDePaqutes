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
        public Repartidor AsignarReparidor(Envio envio)
        {
            double DistanciaMinima = double.MaxValue;
            Repartidor repartidorAsignado = null;

            foreach (Repartidor repartidor in Repartidores)
            {
                if (DistanciaMinima > Extension.CalcularDistancia(repartidor.Posicion,envio.Destinatario.Posicion))
                {
                    DistanciaMinima = Extension.CalcularDistancia(repartidor.Posicion, envio.Destinatario.Posicion);
                    repartidorAsignado = repartidor;
                }
            }

            if (repartidorAsignado == null)
            {
                throw new Exception("Repartidor no encontrado");
            }
            else
            {
                envio.Repartidor = repartidorAsignado;
                envio.Estado = estadosEnvios.AsignadoRepartidor;
                return repartidorAsignado;
            }
        }
        public List<Repartidor> CrarListaRepartidores(DateTime desde,DateTime hasta)
        {
            List<Repartidor> listadoReparidores = new List<Repartidor>();
            var listaEnviosFiltrada = Envios.FindAll(x => x.Estado == estadosEnvios.Entregado && x.FechaEntrega <= hasta && x.FechaEntrega >= desde);

            foreach (Envio envio in listaEnviosFiltrada)
            {
                if (!listadoReparidores.Contains(envio.Repartidor))
                {
                    listadoReparidores.Add(envio.Repartidor);
                }
            }

            return listadoReparidores;
        }
    }
}
