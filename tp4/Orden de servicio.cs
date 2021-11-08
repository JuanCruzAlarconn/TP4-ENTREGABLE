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
        public  Paquete paquete { get; set; }//caracteristicas del paquete      
        public int codigo_cliente { get; set; }//Quien es el iniciador del proceso
        public int codigo_seguro { get; set; }//Asignación de un seguro de acuerdo con el valor declarado por el cliente
        public decimal precio { get; set; }//Calculo del precio del servicio
        public Punto_logistico origen { get; set; }//puede ser una sucursal o ser retirado en domicilio
        public Punto_logistico destino { get; set; }//puede ser una sucursa, otro pais, o que el paquete deba de ser retirado dentro de la sucursal
        public Modalidad modalidad { get; set; }//Cuales son las formas de entrega del paquete dentro del servicio

        public static Orden_de_servicio crear(int codigo_cliente)
        {
            //Se da inicio cuando se hace el proceso de generar un nuevo servicio de entrega de algo
            Orden_de_servicio orden_de_servicio = new Orden_de_servicio();
            orden_de_servicio.codigo_cliente = codigo_cliente;//codigo de cliente corporativo
            orden_de_servicio.codigo_servicio = asignar_codigo_servicio();//codigo de la orden
            orden_de_servicio.estado = asignar_estado_inicial();//cuando se crea la orden cuenta con el estado de inicializado, hay que volcar los datos iniciales del envío
            orden_de_servicio.fecha_ingreso = DateTime.Now;          
            orden_de_servicio.codigo_seguro = asignar_seguro();//El paquete debe de tener un seguro según el enunciado
            orden_de_servicio.origen = Punto_logistico.crear("origen");//lugar de donde parte, puede ser una sucursal o que se halla retirado a domicilio
            orden_de_servicio.destino = Punto_logistico.crear("destino");//lugar en que se deposita al final puede ser una sucursal o lo envío a domicilio
            orden_de_servicio.paquete = asignar_paquetes();//Información sobre el paquete          
            orden_de_servicio.modalidad = asignar_modalidad();//aspectos acerca de como debe de llegar y entregar el paquete además de que si es o no urgente           
            
            orden_de_servicio.precio = asignar_precio();


            comunicar_codigo(orden_de_servicio.codigo_servicio);

            return orden_de_servicio;
        }

        private static void comunicar_codigo(int codigo_servicio)
        {
            throw new NotImplementedException();
        }

        private static Paquete asignar_paquetes()
        {
            //aprobado
            var paquete = Paquete.crear();

            return paquete;
        }

        private static decimal asignar_precio()
        {
            //En función de las propiedades del paquete, la distancia y si es urgente defino el precio que es información que debo de entregar al cliente junto con el codigo de estado para poder consultarlo
            throw new NotImplementedException();
        }

        private static Modalidad asignar_modalidad()
        {
            //Implementar
            var modalidad = Modalidad.crear();

            return modalidad;
        }

        private static List<Estado> asignar_estado_inicial()
        {
            //A implementar
            List<Estado> lista_estados = new List<Estado>();
            var estado_inicial = Estado.crear();

            lista_estados.Add(estado_inicial);

            return lista_estados;
        }



        public static void consultar_estado(int codigo_cliente)
        {
            archivo.generar_archivo();
            Orden_de_servicio.cargar_prueba();
            var codigo = menu_ingreso(codigo_cliente);

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


                if (penultimo_estado.estado == "En distribución desde centro provincial hacia sucursal" && elemento.modalidad.modo_entrega == "Sucursal")
                {
                    estado = "El paquete se halla en la sucursal disponible para el retiro por parte del destinatario";
                }

                if (penultimo_estado.estado == "En distribución desde centro provincial hacia sucursal" && elemento.modalidad.modo_entrega == "Domicilio")
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

            Console.WriteLine("\n******************ESTADO DE SOLCITUD*******************\n");
            Console.WriteLine(estado);
            Console.WriteLine("\n*******************************************************\n");
        }

        private static int menu_ingreso(int codigo_cliente)
        {
            
            string ingreso = "";

            int codigo = 0;

            do
            {
                Console.WriteLine("\nIngrese el código de orden de servicio que desea consultar, el mismo esta compuesto como máximo de 7 elementos númericos y le fue otorgado en el momento en que se finaliza la realizacion del pedido como tal");
                Console.WriteLine("\nIngrese por consola el comando ATRAS para volver al menú anterior en caso de necesitarlo\n");
                ingreso = Console.ReadLine();

                if (ingreso == "ATRAS")
                {
                    Console.WriteLine("\nHa ingresado el comando de para volver al menú anterior");
                    Console.WriteLine("\nSe lo redirijira al menú anterior a continuación");
                    Program.validar_eleccion(codigo_cliente);
                    break;
                }
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
            //Aprobado
            Random r = new Random();

            int seguro = r.Next(0, 10001);

            return seguro;//se genera un número aleatorio para el seguro, para luego ser cotejado por el sistema

        }
      
        private static int asignar_codigo_servicio()
        {
            //Generado a partir de consultar el último código de servicio que fue generado y guardado
            var lista = Orden_de_servicio.abrir_archivo();

            var elemento = lista.Last();

            var codigo = elemento.codigo_servicio;


            return codigo+1;
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
      
       
        public static bool validar_codigo_orden(int codigo_ingresado)
        {
            //Usada en Consultar estado
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

        public Orden_de_servicio()
        { }

        public Orden_de_servicio(int cs,int cseg,decimal precio, int cc, List<Estado> estado, DateTime f, Paquete p,Punto_logistico origen, Punto_logistico destino, Modalidad modalidad)
        {
            this.codigo_servicio = cs;
            this.codigo_seguro = cseg;
            this.precio = precio;
            this.codigo_cliente = cc;
            this.estado = estado;
            this.fecha_ingreso = f;
            this.paquete = p;
            this.origen = origen;
            this.destino = destino;
            this.modalidad = modalidad;

        }

        public static void cargar_prueba ()
        {
            List<Estado> prueba = new List<Estado>();
            var e1 = new Estado(0001, "Aplicación", "Inicializado", "12/07/2021");
            var e2 = new Estado(0002, "Transporte", "En distribución desde la sucursal hacia el centro provincial", "13/07/2021");
            var e3 = new Estado(0003, "Centro provincial", "En centro provincial", "13/07/2021");

            prueba.Add(e1);
            prueba.Add(e2);
            prueba.Add(e3);

            var orig = new Punto_logistico("Julian Alvarez", "calle falsa 1234", 0001, "Argentina");
            var dest = new Punto_logistico("Roman Riquelme", "calle falsa 4561", 0002, "Argentina");

            var mod = new Modalidad("Domicilio", "Sucursal", "urgente");
            var paq = new Paquete(1000, "Bultos", 1000);
            
            var orden1 = new Orden_de_servicio(0000001, 1234, 100, 00000001, prueba, Convert.ToDateTime("12/07/2021"),paq,orig,dest, mod);
            var orden2 = new Orden_de_servicio(0000002, 1234, 900, 00000001, prueba, Convert.ToDateTime("12/07/2021"), paq, orig, dest, mod);
            var orden3 = new Orden_de_servicio(0000003, 1234, 600, 00000001, prueba, Convert.ToDateTime("12/07/2021"), paq, orig, dest, mod);

            var e4 = new Estado(0002, "Transporte", "En distribución desde el centro provincial hacia el centro regional", "14/07/2021");

            prueba.Add(e4);
            var orden4 = new Orden_de_servicio(0000004, 1234, 600, 00000001, prueba, Convert.ToDateTime("12/07/2021"), paq, orig, dest, mod);


            var e5 = new Estado(0002, "Centro regional", "En centro regional", "15/07/2021");

            prueba.Add(e5);
            var orden5= new Orden_de_servicio(0000005, 1234, 600, 00000001, prueba, Convert.ToDateTime("12/07/2021"), paq, orig, dest, mod);
            List<Orden_de_servicio> lista = new List<Orden_de_servicio>();
            lista.Add(orden1);
            lista.Add(orden2);
            lista.Add(orden3);
            lista.Add(orden4);
            lista.Add(orden5);

            string LJson = JsonConvert.SerializeObject(lista);
            File.WriteAllText("Ordenes de servicio.Json", LJson);


        }
    }
}
