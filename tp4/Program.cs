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
            interaccion_validar_cliente();

            Console.WriteLine("\nIngrese una tecla para poder detener la ejecución");
            Console.ReadKey();
        }

        public static void interaccion_validar_cliente()
        {


            do
            {
                mostrar_menu_inicio();


                if (Console.ReadKey(true).Key == ConsoleKey.A)
                {
                    Console.WriteLine("\nSe ejecutara la ruta de validación de la identidad del cliente");
                    Program.validar_identidad();
                    Console.WriteLine("\nA continuación se le presentaran las opciones disponible");
                  
                    validar_eleccion();



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

        public static void validar_eleccion()
        {



            do
            {
                mostrar_menu_opciones();

                if (Console.ReadKey(true).Key == ConsoleKey.NumPad1 && Console.ReadKey(true).Key == ConsoleKey.D1)
                {
                    Console.WriteLine("\nEjecución de la rutira generar envío");
                    validar_eleccion();
                    break;
                }

                if (Console.ReadKey(true).Key == ConsoleKey.NumPad2 && Console.ReadKey(true).Key == ConsoleKey.D2)
                {
                    Console.WriteLine("\nEjecución de la rutina consultar estado de envío");
                    validar_eleccion();
                    break;
                }
                if (Console.ReadKey(true).Key == ConsoleKey.NumPad3 && Console.ReadKey(true).Key == ConsoleKey.D3)
                {
                    Console.WriteLine("\nEjecución de la rutina consultar estado de cuenta");
                    validar_eleccion();
                    break;
                }
                if (Console.ReadKey(true).Key == ConsoleKey.NumPad4 && Console.ReadKey(true).Key == ConsoleKey.D4)
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
                    validar_eleccion();
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
            Console.WriteLine("\nIngrese por teclado cualquiera de las siguientes opciones según la tarea que desee realizar");
            Console.WriteLine("\n1. GENERAR ENVÍO");
            Console.WriteLine("\n2. CONSULTAR ESTADO DE ENVÍO");
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
        public static string estado_servicio()
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

            string estado = Orden_de_servicio.consultar_estado(codigo_orden);

            return $"\nEl estado de la orden de servicio de código {codigo_orden} es: {estado}";
        }
        public static string estado_cuenta()
        {
            throw new NotImplementedException();
        }
        public static int validar_identidad()
        {
            string ingreso = "";
            int codigo_cliente = 0;

            do
            {
                Console.WriteLine("\nIngrese su código de cliente corporativo para poder acceder a las funciones del sistema");
                Console.WriteLine("\nSi desea detener la ejecución del programa introduzca por teclado la frase SALIR tal y como se le comunico");
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
                    continue;
                }
                if (!Int32.TryParse(ingreso, out codigo_cliente))
                {
                    Console.WriteLine("\nEl código de cliente corporativo debe de ser del tipo numérico, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    continue;
                }
                if (codigo_cliente < 0)
                {
                    Console.WriteLine("\nEl código de cliente corporativo debe de ser positivo, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    continue;
                }
                if (ingreso.Count() != 8)
                {
                    Console.WriteLine("\nEl código de cliente corporativo debe de contener 8 digitos numéricos en su extensión, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    continue;
                }

                if (!Cliente_corportativo.validar_cliente(codigo_cliente))
                {
                    Console.WriteLine("\nEl código de cliente corporativo ingresado no se corresponde con ninguno de los elemenos ingresados dentro de la base de datos");
                    continue;
                }

                if(Cliente_corportativo.validar_cliente(codigo_cliente))
                {
                    Console.WriteLine("\nSe lo redirigira al menu con las opciones");
                    validar_eleccion();
                    break;
                }

                break;

            } while (true);

            return codigo_cliente;
        }
        public static int validar_numerico()
        {
            throw new NotImplementedException();
        }
        public static string validar_texto()
        {
            throw new NotImplementedException();
        }

        public static string validar_formato_texto()
        {
            throw new NotImplementedException();
        }
        public static int validar_formato_numerico()
        {
            throw new NotImplementedException();
        }
        public static void volver_atras()
        {
            throw new NotImplementedException();
        }
    }
}
