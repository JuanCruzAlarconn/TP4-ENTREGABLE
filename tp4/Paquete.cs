using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Paquete
    {
        public decimal peso { get; set; }
        public int codigo { get; set; }
        public string descripción { get; set; }
        public string tipo { get; set; }//Si es un sobre o un bulto

        public decimal valor_declarado { get; set; }


        public static Paquete crear()
        {
            var paquete = new Paquete();

            paquete.codigo = asignar_codigo();
            paquete.peso = asignar_peso();
            paquete.descripción = asignar_descripcion();
            paquete.tipo = asignar_tipo();
            paquete.valor_declarado = asignar_valor_declarado();

            return paquete;

        }

        private static decimal asignar_valor_declarado()
        {
            //ingreso del user, importante para calcular el precio del seguro
            throw new NotImplementedException();
        }

        private static string asignar_tipo()
        {
            //ingreso del user
            throw new NotImplementedException();
        }

        private static decimal asignar_peso()
        {
            //ingreso del user
            throw new NotImplementedException();
        }

        private static string asignar_descripcion()
        {
            //ingreso del user
            throw new NotImplementedException();
        }

        private static int asignar_codigo()
        {
            throw new NotImplementedException();
        }
    }
}
