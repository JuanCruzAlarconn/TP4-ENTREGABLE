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
        public double precio { get; set; }//Calculo del precio del servicio
        public Punto_logistico origen { get; set; }//puede ser una sucursal o ser retirado en domicilio
        public Punto_logistico destino { get; set; }//puede ser una sucursa, otro pais, o que el paquete deba de ser retirado dentro de la sucursal
        public Modalidad modalidad { get; set; }//Cuales son las formas de entrega del paquete dentro del servicio


        
   

        public static List<Estado> asignar_estado_inicial()
        {
           
            List<Estado> lista_estados = new List<Estado>();


            var estado_inicial = Estado.crear();

            lista_estados.Add(estado_inicial);

            return lista_estados;
        }



        public static void consultar_estado(int codigo_cliente)
        {
          /*  archivo.generar_archivo();
            Orden_de_servicio.cargar_prueba();*/
            var codigo = menu_ingreso(codigo_cliente);

            var elemento = hallar(codigo);//saco una copia de la orden de servicio desde la base de datos para poder consultar

            var estado_de_orden = elemento.estado;//El estado es una lista de los diferentes estados por lo que pasa el conjunto de bultos hasta llegar al final del recorrido

           

            var ultimo_estado = estado_de_orden.Last();


            
          
            string estado="";

            
            if(ultimo_estado.estado=="Iniciado")
            {
                estado = "La orden de servicio correspondiente se halla inicializada y cargada dentro de la base de datos del sistema";
            }

            if (ultimo_estado.estado == "Entregado")
            {
                estado = "El paquete fue entragado a su destinatario";
            }
            if (ultimo_estado.entidad == "Sucursal" && ultimo_estado.estado != "Entregado")
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

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n******************ESTADO DE SOLCITUD*******************\n");
            Console.WriteLine(estado.ToUpper());
            Console.WriteLine("\n*******************************************************\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static int menu_ingreso(int codigo_cliente)
        {
            
            string ingreso = "";

            int codigo = 0;

            do
            {
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("\n******************************************");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("INGRESE LOS COMANDOS");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("******************************************");
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nINGRESE EL CÓDIGO DE ORDEN DE SERVICIO A CONSULTAR, LUEGO PRESIONE ENTER");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nINGRESE LA FRASE ATRAS Y PRESIONE ENTER PARA VOLVER A LA PANTALLA ANTERIOR");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");
                ingreso = Console.ReadLine();

                if (ingreso == "ATRAS" || ingreso=="atras")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    Console.WriteLine("\nSe lo redirijira al menú anterior a continuación".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.validar_eleccion(codigo_cliente);
                    break;
                }
                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEL CÓDIGO DE ORDEN NO ES VACIO");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;

                }

                if (!int.TryParse(ingreso, out codigo))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEL CÓDIGO DE ORDEN DEBE DE SER NUMÉRICO");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (codigo < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEL CÓDIGO DE ORDEN DEBE DE SER UN NÚMERO POSITIVO");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (ingreso.Length > 7)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEL CÓDIGO DE ORDEN DEBE DE CONTAR CON 7 CIFRAS");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                if (ingreso.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEL CÓDIGO DE ORDEN DEBE DE CONTAR CON 7 CIFRAS");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if(!validar_pertenecia_codigo(codigo, codigo_cliente))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\ncódigo de orden ingresado inexistente".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                

                break;

            } while (true);

            return codigo;
        }

        private static bool validar_pertenecia_codigo(int codigo, int cod_cliente)
        {
            var lista = Orden_de_servicio.abrir_archivo();

            bool flag = false;

            foreach(var elemento in lista)
            {
                if(elemento.codigo_servicio==codigo && elemento.codigo_cliente==cod_cliente)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        public static int asignar_seguro()
        {
            //Aprobado
            Random r = new Random();

            int seguro = r.Next(0, 10001);

            return seguro;//se genera un número aleatorio para el seguro, para luego ser cotejado por el sistema

        }
      
        public static int asignar_codigo_servicio()
        {
            //Generado a partir de consultar el último código de servicio que fue generado y guardado
            var lista = Orden_de_servicio.abrir_archivo();
            int codigo = 0;
            if(lista.Count>0)
            {
                var elemento = lista.Last();

                codigo = elemento.codigo_servicio;
            }

            codigo++;


            return codigo;
        }
        public static List<Orden_de_servicio> abrir_archivo()
        {
            
            var lista = new List<Orden_de_servicio>();
            string ordendeservicioJson = File.ReadAllText("Ordenes de servicio.Json");

            lista = JsonConvert.DeserializeObject<List<Orden_de_servicio>>(ordendeservicioJson);

            return lista;
        }

        public static void grabar(Orden_de_servicio orden)
        {
            var lista = Orden_de_servicio.abrir_archivo();

            lista.Add(orden);

            Orden_de_servicio.actualizar_archivo(lista);

            Console.WriteLine("\nLA ORDEN DE SERVICIO FUE CORRECTAMENTE ALMACENADA");
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

        public Orden_de_servicio(int codigo_servicio,int codigo_seguro,double precio, int codigo_cliente, List<Estado> estado, DateTime fecha, Paquete paquete,Punto_logistico origen, Punto_logistico destino, Modalidad modalidad)
        {
            this.codigo_servicio = codigo_servicio;
            this.codigo_seguro = codigo_seguro;
            this.precio = precio;
            this.codigo_cliente = codigo_cliente;
            this.estado = estado;
            this.fecha_ingreso = fecha;
            this.paquete = paquete;
            this.origen = origen;
            this.destino = destino;
            this.modalidad = modalidad;

        }
/* MODULO CORRESPONDIENTE A LA CARGA DE ELEMENTOS DE PRUEBA, EMPLEADOS EN UNA VERSIÓN INICIAL DEL PROGRAMA
 * AHORA QUEDAN COMO COMENTARIOS A MODO DE EJEMPLO DADO QUE PARA EL MANEJO DE ARCHIVO SE DESARROLLARÁ UNA CARGA EXTERNA Y SU CONSIGUIENTE CARGA DENTRO DE LA CARPETA DEL PROGRAMA PARA SU UTILIZACIÓN
        public static void cargar_prueba ()
        {
            List<Estado> prueba = new List<Estado>();
            List<Estado> prueba2 = new List<Estado>();
            List<Estado> prueba3 = new List<Estado>();
            List<Estado> prueba4 = new List<Estado>();
            List<Estado> prueba5 = new List<Estado>();
            var e1 = new Estado(0001, "Aplicación", "Inicializado", "12/07/2021");
            var e2 = new Estado(0002, "Transporte", "En distribución desde la sucursal hacia el centro provincial", "13/07/2021");
            var e3 = new Estado(0003, "Centro provincial", "En centro provincial", "13/07/2021");

            prueba.Add(e1);
            prueba.Add(e2);
            prueba.Add(e3);

            prueba2.Add(new Estado(0001, "Aplicación", "Iniciado", "13/08/2020"));

            prueba3.Add(new Estado(0001, "Aplicación", "Iniciado", "23/08/2020") );
            prueba3.Add(new Estado(0002, "Sucursal", "En sucursal", "24/08/2020"));
            prueba3.Add(new Estado(0002, "Sucursal", "En distribución hacia el destinatario", "30/08/2020"));

            prueba4.Add(new Estado(0001, "Aplicación", "Iniciado", "30/08/2020"));
            prueba4.Add(new Estado(0002, "Sucursal", "En distribución hacia la sucursal de origen", "01/09/2020"));

            prueba5.Add(new Estado(0001, "Aplicación", "Iniciado", "23/08/2020"));
            prueba5.Add(new Estado(9999, "Transporte", "En distribución desde el centro provincial hacia el centro regional", "30/08/2020"));

            var orig = new Punto_logistico("Julian Alvarez", "calle falsa 1234", 0001, "Argentina");
            var dest = new Punto_logistico("Roman Riquelme", "calle falsa 4561", 0002, "Argentina");

            var mod = new Modalidad("Domicilio", "Sucursal", "urgente");
            var paq = new Paquete(1000, "Bultos", 1000);
            
            var orden1 = new Orden_de_servicio(0000001, 1234, 100, 00000001, prueba, Convert.ToDateTime("12/07/2021"),paq,orig,dest, mod);
            var orden2 = new Orden_de_servicio(0000002, 1234, 900, 00000001, prueba2, Convert.ToDateTime("12/07/2021"), paq, orig, dest, mod);
            var orden3 = new Orden_de_servicio(0000003, 1234, 600, 00000001, prueba3, Convert.ToDateTime("12/07/2021"), paq, orig, dest, mod);

            var e4 = new Estado(0002, "Transporte", "En distribución desde el centro provincial hacia el centro regional", "14/07/2021");

            prueba.Add(e4);
            var orden4 = new Orden_de_servicio(0000004, 1234, 600, 00000001, prueba, Convert.ToDateTime("12/07/2021"), paq, orig, dest, mod);


            var e5 = new Estado(0002, "Centro regional", "En centro regional", "15/07/2021");

            prueba.Add(e5);
            var orden5= new Orden_de_servicio(0000005, 1234, 600, 00000001, prueba4, Convert.ToDateTime("12/07/2021"), paq, orig, dest, mod);

            var orig2 = new Punto_logistico("Enzo Perez", "calle falsa 4561", 0003, "Argentina");
            var e6 = new Estado(0002,"Sucursal", "Entregado", "20/07/2021");
            prueba.Add(e6);
            var orden6 = new Orden_de_servicio(0000006, 1234, 600, 00000002, prueba5, Convert.ToDateTime("12/07/2021"), paq, orig2, dest, mod);

            var dest2 = new Punto_logistico("Lionel Messi", "calle falsa 4561", 5689, "Francia");

            var e7 = new Estado(0032, "Transporte", "En ditribución internacional", "20/07/2021");

            prueba.Add(e7);

            var orden7 = new Orden_de_servicio(0000007, 1234, 600, 00000002, prueba, Convert.ToDateTime("12/07/2021"), paq, orig2, dest2, mod);

            List<Orden_de_servicio> lista = new List<Orden_de_servicio>();
            lista.Add(orden1);
            lista.Add(orden2);
            lista.Add(orden3);
            lista.Add(orden4);
            lista.Add(orden5);
            lista.Add(orden6);
            lista.Add(orden7);

            string LJson = JsonConvert.SerializeObject(lista);
            File.WriteAllText("Ordenes de servicio.Json", LJson);


        }
*/
        public static void enviar_comunicado(Orden_de_servicio orden, DateTime fecha)
        {
            if (orden.destino.localidad == orden.origen.localidad)
            {
                Sucursal.asignar_orden_servicio(orden, fecha);
            }
            if (orden.destino.localidad != orden.origen.localidad && orden.origen.provincia == orden.destino.provincia)
            {
                Sucursal.asignar_orden_servicio(orden, fecha);
                Centro_Provincial.asignar_orden_servicio(orden, fecha);
            }
            if (orden.origen.provincia != orden.destino.provincia)
            {
                Sucursal.asignar_orden_servicio(orden, fecha);
                Centro_Provincial.asignar_orden_servicio(orden, fecha);
                Centro_Regional.asignar_orden_servicio(orden, fecha);
            }

        }
    }
}
