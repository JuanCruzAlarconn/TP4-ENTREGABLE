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

        public Orden_de_servicio2(int codigo, string tipo)
        {
            this.codigo = codigo;
            this.tipo = tipo;
        }

        public override bool Equals(object obj)
        {
            var orden = obj as Orden_de_servicio2;
            return orden != null &&
                   codigo == orden.codigo;
        }

    }
}
