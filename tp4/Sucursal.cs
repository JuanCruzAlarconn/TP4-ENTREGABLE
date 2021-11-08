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
        public string nombre { get; set; }
        public string localidad_dominante { get; set; }//cual es su zona de influencia en donde maneja la operación de forma independiente
        public int codigo_sucursal { get; set; }
        public List<int> codigos_ordenes_asignadas { get; set; } //A medida que le van llegando los pedidos los almancenan en una lista 



        public static List<Sucursal> abrir_archivo()
        {
            string sucursalJson = File.ReadAllText("Sucursales.Json");

            List<Sucursal> lista_surcursal = JsonConvert.DeserializeObject<List<Sucursal>>(sucursalJson);

            return lista_surcursal;

        }

        public static Sucursal hallar(string localidad)
        {
            var lista = abrir_archivo();
            var sucursal = new Sucursal();//envío una copia de la información

            foreach (var s in lista)
            {
                if (s.localidad_dominante==localidad)
                {
                    sucursal = s;
                    break;
                }
            }

            return sucursal;
        }

        public void asignar_orden_servicio(int codigo_orden, string localidad)
        {
            var lista = Sucursal.abrir_archivo();

            var elemento = Sucursal.hallar(localidad);

            lista.Remove(elemento);

            lista.Add(elemento);

            

        }

        public static void actualizar_archivo(List<Sucursal> lista)
        {
            string lista_modificada = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Sucursales.Jso", lista_modificada);

            //Actualiza la base de datos de las sucursales con su asignación de orden de servicio
        }
    }
}
