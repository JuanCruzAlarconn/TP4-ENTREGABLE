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
        public string nombre { get; set; }
        public int codigo { get; set; }

        public List<int> codigos_ordenes_asignadas { get; set; } //A medida que le van llegando los pedidos los almancenan en una lista 

        public List<int> codigos_centros_provinciales_incluidos { get; set; }



        public static List<Centro_Regional> abrir_archivo()
        {
            //Abre el archivo y lo pasa a formato lista para poder operar

            string centrosregionalesJson = File.ReadAllText("Centros Regionales.json");
            var lista = JsonConvert.DeserializeObject<List<Centro_Regional>>(centrosregionalesJson);

            return lista;
        }
        public static Centro_Regional hallar(int codigo)
        {
            var centro_regional = new Centro_Regional();

            var lista_centros_regionales = abrir_archivo();

            foreach (var centro in lista_centros_regionales)
            {
                if (centro.codigo == codigo)
                {
                    centro_regional = centro;

                    break;
                }
            }

            return centro_regional;

        }

        public void asignar_orden_servicio(int codigo_servicio)
        {
            var lista = Centro_Regional.abrir_archivo();

            var centro = Centro_Regional.hallar(this.codigo);

            centro.codigos_ordenes_asignadas.Add(codigo_servicio);

            foreach(var elemento in lista)
            {
                if(elemento.codigo==this.codigo)
                {
                    lista.Remove(elemento);
                }
            }

            lista.Add(centro);

            Centro_Regional.actualizar_archivo(lista);
        }

        public static void actualizar_archivo(List<Centro_Regional> lista)
        {
            string lista_modificada = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Centros Regionales.Json", lista_modificada);

            //Actualiza la base de datos de los centros regionales con su asignación de orden de servicio
        }
    }
}
