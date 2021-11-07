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
                    Console.WriteLine("\nEjecución de la rutira generar envío");
                    validar_eleccion(codigo_cliente);
                    break;
                }

                if (opcion==2)
                {
                    Console.WriteLine("\nEjecución de la rutina consultar estado de envío");
                    Orden_de_servicio.consultar_estado(codigo_cliente);
                    validar_eleccion(codigo_cliente);
                    break;
                }
                if (opcion==3)
                {
                    Console.WriteLine("\nEjecución de la rutina consultar estado de cuenta");
                   /* validar_eleccion(codigo_cliente);*/
                    EstadoCuenta.carga_prueba_estadocuenta();
                   /* EstadoCuenta.mostrar_menu_estado_de_cuenta();*/
                    string nombreyapellido = (EstadoCuenta.hallar(codigo_cliente).codigo_cliente).ToString();
                    Console.WriteLine($"\nBienvenido {nombreyapellido}");
                    EstadoCuenta.mostrarCuenta(codigo_cliente);
                    EstadoCuenta.CalcularSaldoCta(codigo_cliente);
                    EstadoCuenta.filtrarPorFechas(codigo_cliente, DateTime.Now, DateTime.Now.AddDays(10));



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
            Console.WriteLine("\nIngrese por teclado cualquiera de las siguientes opciones según la tarea que desee realizar, y luego presione la tecla enter");
            Console.WriteLine("\n1. GENERAR ENVÍO");
            Console.WriteLine("\n2. CONSULTAR ESTADO DE ENVÍO");
            Console.WriteLine("\n3.CONSULTAR ESTADO DE CUENTA");
            Console.WriteLine("\n4.SALIR");
            Console.WriteLine("******************************************************************************************\n");
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
                    string nombreyapellido = Cliente_corportativo.hallar(codigo_cliente).nombre+" "+ Cliente_corportativo.hallar(codigo_cliente).apellido;
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
