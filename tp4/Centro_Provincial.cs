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
        public string ubicacion { get; set; }
        public int codigo { get; set; }
        public string descripcion { get; set; }
        public List<int> codigos_ordenes_asignadas { get; set; } //A medida que le van llegando los pedidos los almancenan en una lista 

        public List<int> codigos_sucursales_incluidas { get; set; }



        public static List<Centro_Provincial> abrir_archivo()
        {
            //Abre el archivo y lo pasa a formato lista para poder operar

            string centrosprovincialesJson = File.ReadAllText("Centros Provinciales.Json");
            var lista = JsonConvert.DeserializeObject<List<Centro_Provincial>>(centrosprovincialesJson);

            return lista;
        }
        public static Centro_Provincial hallar(int codigo)
        {
            var centro_provincial = new Centro_Provincial();

            var lista_centros_provinciales = abrir_archivo();

            foreach (var centro in lista_centros_provinciales)
            {
                if (centro.codigo == codigo)
                {
                    centro_provincial = centro;

                    break;
                }
            }

            return centro_provincial;

        }

        public void asignar_orden_servicio()
        {
            //Le paso el codigo de la orden de servicio a la entidad para que en el momento en que tome contacto físico con la misma pueda modificar el estado partiendo de la base de datos unificada

            throw new NotImplementedException();
        }

        public static void actuliazar_archivo(List<Centro_Provincial> lista)
        {
            string lista_modificada = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Centros Provinciales.Json", lista_modificada);

            //Actualiza la base de datos de los centros provinciales con su asignación de orden de servicio
        }
    }
}
