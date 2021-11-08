using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace tp4
{
    class Program
    {
        static void Main(string[] args)
        {
            validar_cliente();

            Console.WriteLine("\nIngrese una tecla para poder detener la ejecución");
            Console.ReadKey();
        }

        public static void validar_cliente()
        {


            do
            {
                mostrar_menu_inicio();


                if (Console.ReadKey(true).Key == ConsoleKey.A)
                {
                    
                    Program.validar_identidad();
                    Console.WriteLine("\nA continuación se le presentaran las opciones disponible");
                  
                    



                    break;
                }

                if (Console.ReadKey(true).Key == ConsoleKey.B)
                {
                    Console.WriteLine("\nHa decidido finanlizar la ejecución del programa");
                    Console.WriteLine("\nGracias por utilizar nuestra aplicación");

                    break;
                    //función para detener la ejecución
                }
                if (Console.ReadKey(true).Key != ConsoleKey.B && Console.ReadKey(true).Key != ConsoleKey.A)
                {
                    Console.WriteLine("\nEl comando ingresado no se correponde con ninguno de los comandos válidos disponibles, se lo redirigira a la línea anterior para que tenga la posibilidad de ingresar nuevamente el comando deseado");

                    continue;
                }
                break;
            } while (true);


        }

        public static void validar_eleccion(int codigo_cliente)
        {

            string ingreso = "";
            int opcion = 0;

            do
            {
                mostrar_menu_opciones();

                ingreso = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("\nNo puede dejar la opción como vacio");
                    continue;
                }

                if(!Int32.TryParse(ingreso,out opcion))
                {
                    Console.WriteLine("\nDebe de ingresar una de las opciones numéricas indicadas");
                    continue;
                }

                if(ingreso.Count()!=1)
                {
                    Console.WriteLine("\nSolo debe de ingresar un elemento dentro del campo de opción para poder ejecutar alguna de las funciones");
                    continue;
                }
              

                if (opcion==1)
                {
                    Console.WriteLine("\nEjecución de la rutina generar envío\n");
                    AppCrearOrdenes app = new AppCrearOrdenes();
                    app.ejecutar(codigo_cliente);
                    validar_eleccion(codigo_cliente);
                    break;
                }

                if (opcion==2)
                {
                    Console.WriteLine("\nEjecución de la rutina consultar estado de envío\n");
                    Orden_de_servicio.consultar_estado(codigo_cliente);
                    validar_eleccion(codigo_cliente);
                    break;
                }
                if (opcion==3)
                {
          Console.WriteLine("\nEjecución de la rutina consultar estado de cuenta\n");
                  /*validar_eleccion(codigo_cliente);
                    EstadoCuenta.carga_prueba_estadocuenta();
                    EstadoCuenta.mostrar_menu_estado_de_cuenta();
                    string nombreyapellido = (EstadoCuenta.hallar(codigo_cliente).codigo_cliente).ToString();
                    Console.WriteLine($"\nBienvenido {nombreyapellido}");
                    EstadoCuenta.mostrarCuenta(codigo_cliente);
                    EstadoCuenta.CalcularSaldoCta(codigo_cliente);
                    EstadoCuenta.filtrarPorFechas(codigo_cliente, DateTime.Now, DateTime.Now.AddDays(10));
                  */
                    estado_cuenta(codigo_cliente);
                    validar_eleccion(codigo_cliente);
                    break;

                }
                if (opcion==4)
                {
                    Console.WriteLine("\nHa seleccionado la opción salir de la aplicación");
                    Console.WriteLine("\nGracias por utilizar nuestros servicios");
                    Console.WriteLine("\nHa decidido salir definitivamente de la aplicación \nGracias por usar nuestros servicio\nLa ventana de consola se cerrara al cabo de 10 segundos");
                    Thread.Sleep(10000);

                    Environment.Exit(0);

                    break;
                }
                if (Console.ReadKey(true).Key != ConsoleKey.NumPad1 && Console.ReadKey(true).Key != ConsoleKey.NumPad2 && Console.ReadKey(true).Key != ConsoleKey.NumPad3 && Console.ReadKey(true).Key != ConsoleKey.NumPad4)
                {
                    Console.WriteLine("\nLa opción registrada no se correponde con ninguna de las opciones disponibles dentro del sistema");
                    validar_eleccion(codigo_cliente);
                    continue;
                }

                break;

            } while (true);
        }

        public static void mostrar_menu_inicio()
        {
            Console.WriteLine("******************************************************************************************");
            Console.WriteLine("\nBienvenido al programa de gestión integral de encomiendas");
            Console.WriteLine("\nIngrese A para comenzar");
            Console.WriteLine("\nIngrese B en caso de querer detener la aplicación en curso");
            Console.WriteLine("\n******************************************************************************************\n");
        }
        public static void mostrar_menu_opciones()
        {
            Console.WriteLine("******************************************************************************************");
            Console.WriteLine("\nIngrese por teclado cualquiera de las siguientes opciones según la tarea que desee realizar, y luego presione la tecla enter\n");
            Console.WriteLine("\n1.GENERAR ENVÍO");
            Console.WriteLine("\n2.CONSULTAR ESTADO DE ENVÍO");
            Console.WriteLine("\n3.CONSULTAR ESTADO DE CUENTA");
            Console.WriteLine("\n4.SALIR");
            Console.WriteLine("******************************************************************************************\n");
        }


        public static void salir()
        {
            throw new NotImplementedException();
        }

        public static string crear_order_de_servicio()
        {
            throw new NotImplementedException();
        }
        public static void estado_servicio()
        {
            string ingreso = "";
            int codigo_orden = 0;
            do
            {
                Console.WriteLine("\nIngrese el nº de orden de servicio del cual desea consultar su estado");
                ingreso = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("\nEl el nº de orden no debe de ser vacio, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    continue;
                }
                if (!Int32.TryParse(ingreso, out codigo_orden))
                {
                    Console.WriteLine("\nEl nº de orden debe de ser numérico, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    continue;
                }
                if (codigo_orden < 0)
                {
                    Console.WriteLine("\nEl nº de orden debe de ser positivo, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    continue;
                }
                if (!Orden_de_servicio.validar_codigo_orden(codigo_orden))
                {
                    Console.WriteLine("\nEl nº de orden ingresado no se correpsonde con ninguno de los elementos dentro de la base de datos del repositorio, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    continue;
                }

                break;


            } while (true);

            Orden_de_servicio.consultar_estado(codigo_orden);
        }




        public static void estado_cuenta(int codigo_cliente)
        {

         string ingreso = "";
         int opcion = 0;
         string fechaINI;
         string fechaFIN;
         DateTime inicial;
         DateTime final;

               do
               {
               Console.WriteLine("******************************************************************************************\n");
               Console.WriteLine("\nEjecución de la rutina consultar estado de cuenta");
               EstadoCuenta.carga_prueba_estadocuenta();
               string clavesecreta = (Cliente_corportativo.hallar(codigo_cliente).clave_secreta).ToString();
               Console.WriteLine($"\nBienvenido, su clave secreta es  {clavesecreta}" + " por si no la recuerda\n");
               if  ((EstadoCuenta.ValidarClaveSecreta(clavesecreta))== true)
                { 
               Console.WriteLine("");
               Console.WriteLine("A continuación se le mostrará el estado de cuenta");
               Console.WriteLine("");
               EstadoCuenta.mostrarCuenta(codigo_cliente);
               EstadoCuenta.CalcularSaldoCta(codigo_cliente);
               Console.WriteLine("");
               Console.WriteLine("Si desea filtrar entre dos fechas presione la tecla A, de otra forma regresará al menú anterior");
               Console.WriteLine("\n******************************************************************************************\n");
               
               if (Console.ReadKey(true).Key == ConsoleKey.A)
                    {
                            do
                            { 
                                           Console.WriteLine("");
                                           Console.WriteLine("Por favor ingrese dos fechas válidas para las que quiera filtrar\n");
                                           Console.WriteLine("Por favor ingrese la fecha inicial\n");
                                           fechaINI = Console.ReadLine();
                                           Console.WriteLine("Por favor ingrese la fecha final\n");
                                           fechaFIN = Console.ReadLine();
                                           DateTime.TryParse(fechaINI, out inicial);
                                           DateTime.TryParse(fechaFIN, out final);
                                           if (inicial!= null &&  final!=null)
                                           {
                                            EstadoCuenta.filtrarPorFechas(codigo_cliente, inicial, final);
                                           }

                                           break;


                            } while (true);
                    }

                }


                break;

               } while (true);

        }




        public static void validar_identidad()
        {
            string ingreso = "";
            int codigo_cliente = 0;

            do
            {
                Console.WriteLine("\nIngrese su código de cliente corporativo para poder acceder a las funciones del sistema y luego presione la tecla enter");
                Console.WriteLine("\nSi desea detener la ejecución del programa introduzca por teclado la frase SALIR tal y como se le comunico");
                Console.WriteLine("\n38456910");
                ingreso = Console.ReadLine();
                Cliente_corportativo.carga_prueba();

                if(ingreso=="SALIR")
                {
                    Console.WriteLine("\nHa decidido salir definitivamente de la aplicación \nGracias por usar nuestros servicio\nLa ventana de consola se cerrara al cabo de 10 segundos");
                    Thread.Sleep(10000);

                   Environment.Exit(0);


                }

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("\nEl código de cliente corporativo no puede ser vacio, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.WriteLine("******************************************************************************************\n");
                    continue;
                }
                if (!Int32.TryParse(ingreso, out codigo_cliente))
                {
                    Console.WriteLine("\nEl código de cliente corporativo debe de ser del tipo numérico, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.WriteLine("******************************************************************************************\n");
                    continue;
                }
                if (codigo_cliente < 0)
                {
                    Console.WriteLine("\nEl código de cliente corporativo debe de ser positivo, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.WriteLine("******************************************************************************************\n");
                    continue;
                }
                if (ingreso.Count() != 8)
                {
                    
                    Console.WriteLine("\nEl código de cliente corporativo debe de contener 8 digitos numéricos en su extensión, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.WriteLine("******************************************************************************************\n");
                    continue;
                }

                if (!Cliente_corportativo.validar_cliente(codigo_cliente))
                {
                    Console.WriteLine("\nEl código de cliente corporativo ingresado no se corresponde con ninguno de los elemenos ingresados dentro de la base de datos");
                    Console.WriteLine("******************************************************************************************\n");
                    continue;
                }

                if(Cliente_corportativo.validar_cliente(codigo_cliente))
                {
                    EstadoCuenta.mostrar_menu_estado_de_cuenta();
                    string nombreyapellido = Cliente_corportativo.hallar(codigo_cliente).nombreyapellido;
                    Console.WriteLine($"\nBienvenido {nombreyapellido}");
                    Console.WriteLine("\nSe lo redirigira al menu con las opciones");
                    validar_eleccion(codigo_cliente);
                    break;
                }

                break;

            } while (true);

            
        }
       
    }
}
