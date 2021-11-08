using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp4
{
    class Orden_de_servicio2
    {
        private int codigo;
        private int cliente;
        //private int postal;
        private Punto_logistico origen;
        private Punto_logistico destino;
        private Modalidad modalidad;
        //private int sucursal;
        //private string nombre;
        //private string direccion;
        private decimal costo;
        private string estado;
        private DateTime fecha;

        public Orden_de_servicio2(int codigo, int cliente, /*int postal, */Punto_logistico origen, Punto_logistico destino, Modalidad modalidad, /*int sucursal, string nombre, string direccion, */decimal costo, string estado, DateTime fecha)
        {
            this.codigo = codigo;
            this.cliente = cliente;
            //this.postal = postal;
            this.origen = origen;
            this.destino = destino;
            this.modalidad = modalidad;
            //this.sucursal = sucursal;
            //this.nombre = nombre;
            //this.direccion = direccion;
            this.costo = costo;
            this.estado = estado;
            this.fecha = fecha;
        }

        public override bool Equals(object obj)
        {
            var orden = obj as Orden_de_servicio2;
            return orden != null &&
                   codigo == orden.codigo;
        }
        
        public override int GetHashCode() { return 0; }

        
    }
}
