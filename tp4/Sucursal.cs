using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Sucursal
    {
        public DateTime fecha { get; set; }
        
        
        public Orden_de_servicio orden_asignada { get; set; } //A medida que le van llegando los pedidos los almancenan en una lista 



      
        public static List<Sucursal> abrir_archivo()
        {

          
            string sucursalJson = File.ReadAllText("Sucursales.Json");

            List<Sucursal> lista_surcursal = JsonConvert.DeserializeObject<List<Sucursal>>(sucursalJson);

            return lista_surcursal;

        }

     

        public static void asignar_orden_servicio(Orden_de_servicio orden, DateTime fecha)
        {
            var lista = Sucursal.abrir_archivo();

            var elemento = new Sucursal();

            elemento.orden_asignada = orden;
            elemento.fecha = fecha;

            lista.Add(elemento);

            Sucursal.actualizar_archivo(lista);

        }

        public static void actualizar_archivo(List<Sucursal> lista)
        {
            string lista_modificada = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Sucursales.Json", lista_modificada);

            //Actualiza la base de datos de las sucursales con su asignación de orden de servicio
        }
    }
}
