using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace tp4
{
    class Estado
    {//implica los estados por los que pasara el paquete en su transito 
        public int codigo_entidad { get; set; }

        public string entidad { get; set; }

        public string estado { get; set; }

        public static Estado crear()
        {
            var estado = new Estado();

            estado.codigo_entidad = asignar_codigo();
            estado.entidad = asignar_entidad();
            estado.estado = asignar_estado_inicial();

            return estado;


        }

        private static string asignar_estado_inicial()
        {
            throw new NotImplementedException();
        }

        private static string asignar_entidad()
        {
            throw new NotImplementedException();
        }

        private static int asignar_codigo()
        {
            throw new NotImplementedException();
        }
    }
}
