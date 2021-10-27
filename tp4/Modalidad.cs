using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Modalidad
    {
        public string modo_retiro { get; set; }
        public string modo_entrega { get; set; }
        public string tipo_envio { get; set; } //si es normal o urgente, muy importante a la hora de las designaciones y de poder fijar el precio por el servicio

        public static Modalidad crear()
        {
            var modalidad = new Modalidad();
            modalidad.modo_retiro = asignar_retiro();
            modalidad.modo_entrega = asignar_entrega();
            modalidad.tipo_envio = asignar_tipo();

            return modalidad;
        }

        private static string asignar_entrega()
        {
            throw new NotImplementedException();
        }

        private static string asignar_tipo()
        {
            throw new NotImplementedException();
        }

        private static string asignar_retiro()
        {
            throw new NotImplementedException();
        }
    }
}
