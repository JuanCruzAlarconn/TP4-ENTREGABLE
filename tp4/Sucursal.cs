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
        public string ubicacion { get; set; }
        public string localidad_dominante { get; set; }//cual es su zona de influencia en donde maneja la operación de forma independiente
        public int codigo_sucursal { get; set; }
        public List<int> codigos_ordenes_asignadas { get; set; } //A medida que le van llegando los pedidos los almancenan en una lista 



        public static List<Sucursal> abrir_archivo()
        {
            string sucursalJson = File.ReadAllText("Sucursales.Json");

            List<Sucursal> lista_surcursal = JsonConvert.DeserializeObject<List<Sucursal>>(sucursalJson);

            return lista_surcursal;

        }

        public static Sucursal hallar(int codigo)
        {
            var lista = abrir_archivo();
            var sucursal = new Sucursal();//envío una copia de la información

            foreach (var s in lista)
            {
                if (s.codigo_sucursal == codigo)
                {
                    sucursal = s;
                    break;
                }
            }

            return sucursal;
        }

        public static void asignar_orden_servicio(int codigo_orden)
        {
            //Le paso el codigo de la orden de servicio a la entidad para que en el momento en que tome contacto físico con la misma pueda modificar el estado partiendo de la base de datos unificada
            throw new NotImplementedException();
        }

        public void actuliazar_archivo()
        {
            //paso final luego de haber incorporado el nuevo elemento dentro de la lista
            throw new NotImplementedException();
        }
    }
}
