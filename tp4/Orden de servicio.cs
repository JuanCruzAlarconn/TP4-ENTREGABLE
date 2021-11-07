using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Orden_de_servicio
    {
        public int codigo_servicio { get; set; }//Generado aleatoriamente para poder brindar una identifición sobre el pedido que llegara a otras clases, para que las mismas puedan contar con el acceso y puedan modificar la base de datos en consecuencia
        public List<Estado> estado { get; set; } //Genera una lista de todos los estados por los que va transitando el paquete hasta el momento de la consulta, GENERA TRAZABILIDAD
        public DateTime fecha_ingreso { get; set; }//Se asigna la fecha del momento en que se creo el pedido
        public DateTime? fecha_egreso { get; set; } // Cuando nace el objeto permanece en null hasta que lo damos por finalizado, cuando acciono el metodo finalizar lo carga a modo de registro para poder archivarlo dentro de la base de datos
        public  Paquete paquete { get; set; }//caracteristicas del paquete
        public List<Transporte_designado> transportes_asignados { get; set; } //agrega dentro de una lista todos los transporte necesarios para que le envío pueda ser entregado
        public int codigo_cliente { get; set; }//Quien es el iniciador del proceso
        public int codigo_seguro { get; set; }//Asignación de un seguro de acuerdo con el valor declarado por el cliente
        public decimal precio { get; set; }//Calculo del precio del servicio
        public Punto_logistico origen { get; set; }//puede ser una sucursal o ser retirado en domicilio
        public Punto_logistico destino { get; set; }//puede ser una sucursa, otro pais, o que el paquete deba de ser retirado dentro de la sucursal
        public Modalidad modalidad { get; set; }//Cuales son las formas de entrega del paquete dentro del servicio

        public static Orden_de_servicio Crear(int codigo_cliente)
        {
            //Se da inicio cuando se hace el proceso de generar un nuevo servicio de entrega de algo
            Orden_de_servicio orden_de_servicio = new Orden_de_servicio();
            orden_de_servicio.codigo_cliente = codigo_cliente;//codigo de cliente corporativo
            orden_de_servicio.codigo_servicio = asignar_codigo_servicio();//codigo de la orden
            orden_de_servicio.estado = asignar_estado_inicial();//cuando se crea la orden cuenta con el estado de inicializado, hay que volcar los datos iniciales del envío
            orden_de_servicio.fecha_ingreso = asignar_fecha_ingreso();//la fecha en que ingreso el pedido
            orden_de_servicio.fecha_egreso = asignar_fecha_egreso();//cuando el paquete llego finalmente a destino
            orden_de_servicio.codigo_seguro = asignar_seguro();//El paquete debe de tener un seguro según el enunciado
            orden_de_servicio.origen = Punto_logistico.crear("origen");//lugar de donde parte, puede ser una sucursal o que se halla retirado a domicilio
            orden_de_servicio.destino = Punto_logistico.crear("destino");//lugar en que se deposita al final puede ser una sucursal o lo envío a domicilio
            orden_de_servicio.paquete = asignar_paquetes();//Información sobre el paquete          
            orden_de_servicio.modalidad = asignar_modalidad();//aspectos acerca de como debe de llegar y entregar el paquete además de que si es o no urgente           
            orden_de_servicio.transportes_asignados = asignar_transportes(orden_de_servicio.origen, orden_de_servicio.destino, orden_de_servicio.codigo_servicio, orden_de_servicio.paquete);//Todos los transportes propios de la empresa implicados en la operación, si no son necesarios se los deja en null
            orden_de_servicio.precio = asignar_precio();

            return orden_de_servicio;
        }

        private static Paquete asignar_paquetes()
        {
            throw new NotImplementedException();
        }

        private static List<Transporte_designado> asignar_transportes(Punto_logistico origen, Punto_logistico destino, int codigo_servicio, Paquete paquete)
        {
            //evalua los transportes necesarios de cada operación, y asigna al servicio según las necesidades de la operación
            throw new NotImplementedException();
        }

        private static decimal asignar_precio()
        {
            //En función de las propiedades del paquete, la distancia y si es urgente defino el precio que es información que debo de entregar al cliente junto con el codigo de estado para poder consultarlo
            throw new NotImplementedException();
        }

        private static Modalidad asignar_modalidad()
        {
            //Implica ver las condiciones de retiro, entrega y la urgencia
            throw new NotImplementedException();
        }

        private static List<Estado> asignar_estado_inicial()
        {
            List<Estado> lista_estados = new List<Estado>();
            var estado_inicial = Estado.crear();

            lista_estados.Add(estado_inicial);

            return lista_estados;
        }



        public static string consultar_estado()
        {
            var codigo = menu_ingreso();

            var elemento = hallar(codigo);//saco una copia de la orden de servicio desde la base de datos para poder consultar

            var estado_de_orden = elemento.estado;//El estado es una lista de los diferentes estados por lo que pasa el conjunto de bultos hasta llegar al final del recorrido

            var ultimo_estado = estado_de_orden.Last();

            string estado="";

            
            if(ultimo_estado.estado=="Iniciado")
            {
                estado = "La orden de servicio correspondiente se halla inicializada y cargada dentro de la base de datos del sistema";
            }

            if (ultimo_estado.estado == "Entregado")//Es un estado que debe de quemarlo una sucursal
            {
                estado = "El paquete fue entragado a su destinatario";
            }
            if (ultimo_estado.entidad == "Sucursal")
            {
                /*ESTADOS POSIBLES
                 * En sucursal de origen
                 * En sucursal de destino
                 */
                estado = "El paquete se halla dentro de las inmediaciones de la sucursal en donde se recepciono el producto proveniente del cliente";
                var penultimo_estado = estado_de_orden[estado_de_orden.Count - 2];


                if (penultimo_estado.estado == "En distribución desde centro provincial hacia sucursal" && elemento.modalidad.modo_entrega == "Entregado en sucursal")
                {
                    estado = "El paquete se halla en la sucursal disponible para el retiro por parte del destinatario";
                }

                if (penultimo_estado.estado == "En distribución desde centro provincial hacia sucursal" && elemento.modalidad.modo_entrega == "Entrega en domicilio")
                {
                    //fue entregado hasta una sucursal de destino
                    estado = $"El paquete se halla dentro de las inmediaciones de la sucursal que se corresponde con la localidad de {elemento.destino.localidad}";
                }

                if (ultimo_estado.estado == "En distribución hacia la sucursal de origen")//Estado que lo maneja internamente la sucursal
                {
                    estado = "El paquete se halla en distribución desde el cliente hasta la sucursal de origen";
                }

                if (ultimo_estado.estado == "En distribución hacia el destinatario")//Estado que lo maneja internamente la sucursal
                {
                    
                    estado = "El paquete se halla en distribución hacia su destino final para ser entregado al destinatario en su domicilio";
                }


            }

            if (ultimo_estado.entidad == "Transporte")
            {
                /*ESTADOS POSIBLES QUE EL TRANSPORTISTA PUEDE AÑADIR A LA LISTA DE ESTADOS DE LA ORDEN DE SERVICIO CORRESPONDIENTE
                 * En distribución desde la sucursal hasta el centro provincial
                 * En distribución desde el centro provincial hasta el centro regional
                 * En distribución entre centros regionales
                 * En distribución desde el centro regional hasta el centro provincial
                 * En distribución desde el centro provincial hasta la sucursal
                 * NOTA: SE CORRESPONDE CON QUE EN EL MOMENTO EN QUE SE HACE LA ENTREGA EN EL CENTRO CORRESPONDIENTE EL CENTRO MISMO HACE LA CARGA QUE SE HALLA DENTRO DE LAS INTALACIONES DEL MISMO
                 */

                estado = "En distribución";

                //Los transporte modifican el estado incluyendo en que consiste su viaje
                //Como los transporte enunciados son de la empresa se llega al acuerdo de que en lugar de generar 2 estado iguales como lo pueden ser entregado en algun punto de distribución, se deja que el centro de distribución defina que el tiene el paquete también indica que el paquete fue entregado dado que todas las entidades pertenecen al mismo sistema

            }

            if (ultimo_estado.estado == "En centro provincial")
            {
                /*ESTADOS POSIBLES
                 * En el centro provincial
                 */

                var penultimo_estado = estado_de_orden[estado_de_orden.Count - 2];

                if (penultimo_estado.estado == "En distribución desde la sucursal hacia el centro provincial")
                {
                    estado = $"El paquete se halla dentro del centro provincial de {elemento.origen.provincia}";
                }

                if (penultimo_estado.estado == "En distribución desde el centro regional hacia el centro provincial")
                {
                    estado = $"El paquete se halla dentro del centro provincial de {elemento.destino.provincia}";
                }

            }

            if (ultimo_estado.estado == "En centro regional")
            {
                /*ESTADOS POSIBLES
                 * En el centro regional
                 */

                var penultimo_estado = estado_de_orden[estado_de_orden.Count - 2];

                if (penultimo_estado.estado == "En distribución desde el centro regional hacia el centro regional")
                {
                    estado = $"El paquete se halla en las inmediaciones del centro regional de la región {elemento.destino.region}";
                }

                if (penultimo_estado.estado == "En distribución desde el centro provincial hacia el centro regional")
                {
                    estado = $"El paquete se halla dentro de las inmediaciones del centro regional correspondiente de la región {elemento.origen.region}";
                }

                if (elemento.destino.pais != "Argentina")// el caso en que se halla dentro de una sucursal y al mismo tiempo el paquete cuenta con el destino de dirigirse fuera del pais
                {
                    estado = "El paquete se halla dentro del centro regional de la región metropolitana en proceso para ser envíado hacia el exterior";
                }

            }

           if(ultimo_estado.estado=="En ditribución internacional")
            {
                estado = "El paquete se halla en distribución internacional";
            }

            return estado;
        }

        private static int menu_ingreso()
        {
            var lista = Orden_de_servicio.abrir_archivo();

            string ingreso = "";

            int codigo = 0;

            do
            {
                Console.WriteLine("\nIngrese el código de orden de servicio que desea consultar, el mismo esta compuesto como máximo de 7 elementos númericos y le fue otorgado en el momento en que se finaliza la realizacion del pedido como tal");
                Console.WriteLine("\nIngrese por consola el comando ATRAS para volver al menú anterior en caso de necesitarlo");
                ingreso = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("\nEl código de orden ingresado esta vacio y no se corresponde con la solicitud");
                    continue;

                }

                if (!int.TryParse(ingreso, out codigo))
                {
                    Console.WriteLine("\nEl código ingresado debe de ser del tipo númerico");
                    continue;
                }

                if (codigo < 0)
                {
                    Console.WriteLine("\nEl código ingresado debe de ser numérico positivo");
                    continue;
                }

                if (ingreso.Length > 7)
                {
                    Console.WriteLine("\nEl código ingresado cuenta con más de 7 elementos");
                    continue;
                }
                if (ingreso.Length == 0)
                {
                    Console.WriteLine("\nEl código ingresado cuenta con menos elementos de los esperados");
                    continue;
                }

                if(!validar_pertenecia_codigo(codigo))
                {
                    Console.WriteLine("\nEl código ingresado no se corresponde con ninguna de las ordenes de servicio contenidas dentro de nuestras bases de datos");
                    continue;
                }

                break;

            } while (true);

            return codigo;
        }

        private static bool validar_pertenecia_codigo(int codigo)
        {
            var lista = Orden_de_servicio.abrir_archivo();

            bool flag = false;

            foreach(var elemento in lista)
            {
                if(elemento.codigo_servicio==codigo)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        private static int asignar_seguro()
        {
            Random r = new Random();

            int seguro = r.Next(0, 10001);

            return seguro;//se genera un número aleatorio para el seguro, para luego ser cotejado por el sistema

        }
        private static DateTime asignar_fecha_ingreso()
        {
            return DateTime.Now;
        }
        private static DateTime? asignar_fecha_egreso()
        {
            return null;
        }
        private static int asignar_codigo_servicio()
        {
            Random r = new Random();

            int servicio = r.Next(0, 9999999);

            return servicio;
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

        }
        public static Orden_de_servicio hallar(int codigo_orden)
        {
            var lista = Orden_de_servicio.abrir_archivo();

            var orden = new Orden_de_servicio();

            foreach (var orden_servicio in lista)
            {
                if (orden_servicio.codigo_servicio == codigo_orden)
                {
                    orden = orden_servicio;
                    break;
                }
            }

            return orden;

        }
      
        public void finalizar()
        {
            /*
             * Es una de los métodos que forma parte de la clase pero dentro de las diferentes interacciones que dan respueta a los puntos propuestos no será necesario utilizar
             * Se expecifica que el método existe y es parte importante del sistema dado que permite inicializar ciertas propiedades para poder colocar a la orden de pedido como finalizada
             
             */
        }

        public static bool validar_codigo_orden(int codigo_ingresado)
        {
            var lista_ordenes = Orden_de_servicio.abrir_archivo();

            bool flag = false;

            foreach (var orden in lista_ordenes)
            {
                if (orden.codigo_servicio == codigo_ingresado)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }
    }
}
