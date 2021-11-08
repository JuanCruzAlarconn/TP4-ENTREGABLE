using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class ConjuntoDeOrdenes
    {
        private List<Orden_de_servicio2> ordenes;

        public ConjuntoDeOrdenes()
        {
            ordenes = new List<Orden_de_servicio2>();
        }

        public string listado()
        {
            string retorno = "";

            foreach (Orden_de_servicio2 orden in ordenes)
            {
                retorno = retorno + orden.ToString() + "\n";
            }

            return (retorno);
        }

        public bool agregar(Orden_de_servicio2 orden)
        {
            if (!ordenes.Contains(orden))
            {
                ordenes.Add(orden);
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public Orden_de_servicio2 obtener(int codigo, string tipo)
        {
            Orden_de_servicio2 retorno = null;
            Orden_de_servicio2 aBuscar = new Orden_de_servicio2(codigo, 0, /*0, */null, null, null, /*0, "", "", */0, "", default);

            int posicion = this.ordenes.IndexOf(aBuscar);

            if (posicion != -1)
            {
                retorno = ordenes[posicion];
            }

            return (retorno);
        }

        /*public int asignar_codigo_servicio()
        {
            Random r = new Random();

            int servicio = r.Next(0, 9999999);

            return servicio;
        }*/

        public int asignar_codigo_servicio()
        {
            //Generado a partir de consultar el último código de servicio que fue generado y guardado
            var lista = Orden_de_servicio.abrir_archivo();

            var elemento = lista.Last();

            var codigo = elemento.codigo_servicio;


            return codigo + 1;
        }

        /*public static void carga_prueba()
        {
            var orden_de_servicio = new Orden_de_servicio();
            orden_de_servicio.codigo_servicio = 00000001;
            orden_de_servicio.codigo_cliente = 38456910;
            orden_de_servicio.estado = "Inicializado";
            orden_de_servicio.fecha_ingreso = DateTime.Now;
            

            var orden_de_servicio2 = new Orden_de_servicio();
            orden_de_servicio2.codigo_servicio = 00000002;
            orden_de_servicio2.codigo_cliente = 12345678;
            orden_de_servicio.estado = "Inicializado";
            orden_de_servicio.fecha_ingreso = DateTime.Now;
            

            List<Orden_de_servicio> lista = new List<Orden_de_servicio>();
            lista.Add(orden_de_servicio);
            lista.Add(orden_de_servicio2);
 
            string clienteJson = JsonConvert.SerializeObject(lista);

            File.WriteAllText("ordenes.json", ordenesJson);
        }

        private static int asignar_codigo_servicio()
        {
            //Generado a partir de consultar el último código de servicio que fue generado y guardado
            var lista = Orden_de_servicio.abrir_archivo();

            var elemento = lista.Last();

            var codigo = elemento.codigo_servicio;


            return codigo + 1;
        }
        public static List<Orden_de_servicio> abrir_archivo()
        {
            var lista = new List<Orden_de_servicio>();
            string ordendeservicioJson = File.ReadAllText("Ordenes de servicio.Json");

            lista = JsonConvert.DeserializeObject<List<Orden_de_servicio>>(ordendeservicioJson);

            return lista;
        }
        public static void actualizar_archivo(List<Orden_de_servicio> lista)
        {
            string lista_modificada = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Ordenes de servicio.Json", lista_modificada);

            //Actualiza la base de datos de las ordenes de servicio

        }*/
    }
}
