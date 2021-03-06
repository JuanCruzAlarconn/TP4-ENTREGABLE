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
    {//implica los estados por los que pasara el paquete en su transito por donde sea asignado 
        public int codigo_entidad { get; set; }

        public string entidad { get; set; }//Puede ser una sucursal, centro provincial, centro regional, transporte de la empresa propia de cada dimesión de traslado

        public string estado { get; set; }//Espacio donde la  entidad que lo recibe coloca en que estado se hallan los bultos

        public DateTime fecha { get; set; }
      


        public static Estado crear()
        {
            var estado = new Estado();

            estado.codigo_entidad = 0000001;
            estado.entidad = "Aplicación de servicio";
            estado.estado = "Iniciado";
            estado.fecha = DateTime.Now;

            return estado;


        }

        public Estado (int c, string entidad, string estado, string fecha)
        {
            this.codigo_entidad = c;
            this.entidad = entidad;
            this.estado = estado;
            this.fecha = Convert.ToDateTime(fecha);

        }

        public Estado()
        {

        }

    }
}
