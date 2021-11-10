using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace tp4
{
    class Centro_Regional
    {
        public DateTime fecha_ingreso { get; set; }
       

        public Orden_de_servicio orden_asignada { get; set; } //A medida que le van llegando los pedidos los almancenan en una lista 

      



        public static List<Centro_Regional> abrir_archivo()
        {

            Centro_Regional.actualizar_archivo(new List<Centro_Regional>());
            string centrosregionalesJson = File.ReadAllText("Centros Regionales.json");
            var lista = JsonConvert.DeserializeObject<List<Centro_Regional>>(centrosregionalesJson);

            return lista;
        }
       

       

        public static void actualizar_archivo(List<Centro_Regional> lista)
        {
            string lista_modificada = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Centros Regionales.Json", lista_modificada);

            //Actualiza la base de datos de los centros regionales con su asignación de orden de servicio
        }

        public static void asignar_orden_servicio(Orden_de_servicio orden, DateTime fecha)
        {
            var lista = Centro_Regional.abrir_archivo();

            var elemento = new Centro_Regional();
            elemento.fecha_ingreso = fecha;
            elemento.orden_asignada = orden;

            lista.Add(elemento);

            actualizar_archivo(lista);
        }

    }
}
