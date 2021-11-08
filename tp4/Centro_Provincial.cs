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
        public string nombre { get; set; }
        public int codigo { get; set; }
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

        public void asignar_orden_servicio(int codigo_orden)
        {
            var lista = Centro_Provincial.abrir_archivo();

            var centro = Centro_Provincial.hallar(this.codigo);

            centro.codigos_ordenes_asignadas.Add(codigo_orden);

            foreach (var elemento in lista)
            {
                if (elemento.codigo == this.codigo)
                {
                    lista.Remove(elemento);
                }
            }

            lista.Add(centro);

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
