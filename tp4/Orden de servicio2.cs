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
        private string tipo;
        private int postal;
        private int sucursal;
        private string tipo2;
        private string nombre;
        private string direccion;
        private decimal costo;

        public Orden_de_servicio2(int codigo, string tipo, int postal, int sucursal, string tipo2, string nombre, string direccion, decimal costo)
        {
            this.codigo = codigo;
            this.tipo = tipo;
            this.postal = postal;
            this.sucursal = sucursal;
            this.tipo2 = tipo2;
            this.nombre = nombre;
            this.direccion = direccion;
            this.costo = costo;
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
