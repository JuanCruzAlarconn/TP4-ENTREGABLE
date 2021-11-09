using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace tp4
{
    class Centro_Provincial
    {
        public DateTime fecha { get; set; }
        
        public Orden_de_servicio orden_asignada { get; set; } //A medida que le van llegando los pedidos los almancenan en una lista 
      



        public static List<Centro_Provincial> abrir_archivo()
        {

            
            string centrosprovincialesJson = File.ReadAllText("Centros Provinciales.Json");
            var lista = JsonConvert.DeserializeObject<List<Centro_Provincial>>(centrosprovincialesJson);

            return lista;
        }

        public static void asignar_orden_servicio(Orden_de_servicio orden, DateTime fecha)
        {
            var lista = Centro_Provincial.abrir_archivo();

            var elemento = new Centro_Provincial();

            elemento.orden_asignada = orden;
            elemento.fecha = fecha;

            lista.Add(elemento);

            Centro_Provincial.actualizar_archivo(lista);
        }




        public static void actualizar_archivo(List<Centro_Provincial> lista)
        {
            string lista_modificada = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Centros Provinciales.Json", lista_modificada);

            //Actualiza la base de datos de los centros provinciales con su asignación de orden de servicio
        }
    }
}
