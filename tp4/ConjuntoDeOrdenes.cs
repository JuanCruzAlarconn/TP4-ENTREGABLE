using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class ConjuntoDeOrdenes
    {
        private List<Orden_de_servicio2> ordenes;

        public ConjuntoDeOrdenes()
        {
            ordenes = new List<Orden_de_servicio2>();
        }
        public bool agregar(Orden_de_servicio2 orden)
        {
            if (!ordenes.Contains(orden))
            {
                ordenes.Add(orden);
                return (true);
            }
            else
            {
                return (false);
            }
        }
    }
}
