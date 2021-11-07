using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class AppCrearOrdenes
    {
        private Validador validador;
        private ConjuntoDeOrdenes ordenes;

        public AppCrearOrdenes()
        {
            validador = new Validador();
            ordenes = new ConjuntoDeOrdenes();
        }

        public void ejecutar()
        {
            Console.WriteLine("Completa los datos de tu envío" + "\n");
            string tipo = validador.pedirTipo("¿Qué tipo de paquete quieres enviar? (Nacional/Internacional)\n");
            
        }
    }
}
