using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public enum estadosEnvios { Pendiente,AsignadoRepartidor,EnCamino,Entregado}
    public class Envio
    {
        public string Numero { get; set; }
        public Persona Destinatario { get; set; }
        public Repartidor Repartidor { get; set; }
        public estadosEnvios Estado { get; set; }
        public DateTime FechaEstimada { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public double Precio { get; set; }
        public Envio(string numero, Persona destinatario,DateTime fechaEstimada,string descripcion,double precio)
        {
            this.Precio = precio;
            this.Numero = numero;
            this.Descripcion = Descripcion;
            this.Destinatario = destinatario;
            this.Estado = estadosEnvios.Pendiente;
        }
    }
}
