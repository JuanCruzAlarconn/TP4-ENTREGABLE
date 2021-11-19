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
            string ingreso = "";

            do
            {
                mostrar_menu_inicio();
                ingreso = Console.ReadLine();

                ingreso = ingreso.ToUpper();

                if(string.IsNullOrEmpty(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo puede ingresar un espacio vacio como opción a elegir");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if(int.TryParse(ingreso, out int salida))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLas opciones disponibles no son numéricas");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if(ingreso.Length!=1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nSolo debe de ingresar na de las opciones disponibles dentro del menu");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (ingreso == "A")
                {
                    
                    Program.validar_identidad();
                    Console.WriteLine("\nA continuación se le presentaran las opciones disponible");
                  
                    



                    break;
                }

                if (ingreso == "B")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHa seleccionado la opción salir de la aplicación");
                    Console.WriteLine("\nGracias por utilizar nuestros servicios");
                    Console.WriteLine("\nHa decidido salir definitivamente de la aplicación \nGracias por usar nuestros servicio\nLa ventana de consola se cerrara al cabo de 10 segundos");
                    Thread.Sleep(10000);

                    Environment.Exit(0);

                    break;
                   
                }
                if (ingreso!="B" && ingreso!="A")
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
                    Console.WriteLine("\n**************************************************");
                    Console.WriteLine("*****************GENERAR ENVÍO************************");
                    Console.WriteLine("****************************************************\n");
                    AppCrearOrdenes app = new AppCrearOrdenes();
                    app.ejecutar(codigo_cliente);
                    Console.WriteLine("\nSE LO REDIRIGIRÁ AL MENÚ ANTERIOR PARA QUE PUEDA SEGUIR OPERANDO\n");
                    validar_eleccion(codigo_cliente);
                    break;
                }

                if (opcion==2)
                {
                    Console.WriteLine("\n**************************************************");
                    Console.WriteLine("******************CONSULTAR ESTADO DE ENVÍO***********");
                    Console.WriteLine("****************************************************\n");
                    Orden_de_servicio.consultar_estado(codigo_cliente);

                    Console.WriteLine("\nSE LO REDIRIGIRÁ AL MENÚ ANTERIOR PARA QUE PUEDA SEGUIR OPERANDO\n");

                    validar_eleccion(codigo_cliente);
                    break;
                }
                if (opcion==3)
                {
                    Console.WriteLine("\n**************************************************");
                    Console.WriteLine("******************CONSULTAR ESTADO DE CUENTA***********");
                    Console.WriteLine("****************************************************\n");

                    estado_cuenta(codigo_cliente);
                    Console.WriteLine("\nSE LO REDIRIGIRÁ AL MENÚ ANTERIOR PARA QUE PUEDA SEGUIR OPERANDO\n");
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
                if (opcion!=1 && opcion !=2 && opcion !=3 && opcion !=4)
                {
                    Console.WriteLine("\nLa opción registrada no se correponde con ninguna de las opciones disponibles dentro del sistema");
                    Console.WriteLine("\nSe lo devolvera al menú anterior para poder volver a ingresar una opción");
                    Console.WriteLine("\nSE LO REDIRIGIRÁ AL MENÚ ANTERIOR PARA QUE PUEDA SEGUIR OPERANDO\n");
                    validar_eleccion(codigo_cliente);
                    continue;
                }

                break;

            } while (true);
        }

        public static void mostrar_menu_inicio()
        {
            Console.WriteLine("******************************************************************************************");
            Console.WriteLine("\nBIENVENIDO AL PROGRAMA DE GESTIÓN INTEGRAL DE ENCOMIENDAS");
            Console.WriteLine("\nIngrese cualquiera de las siguientes opciones y luego presione ENTER");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nA: INICIAR APLICACIÓN");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nB: SALIR");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n******************************************************************************************\n");
        }
        public static void mostrar_menu_opciones()
        {
            
            Console.WriteLine("******************************************************************************************");
            Console.WriteLine("***************APLICACIÓN DE CLIENTE CORPORATIVO*******************************************");
            Console.WriteLine("******************************************************************************************");
            Console.WriteLine("\nIngrese por teclado cualquiera de las siguientes opciones según la tarea que desee realizar, y luego presione la tecla enter\n");
            Console.WriteLine("\n1.GENERAR ENVÍO");
            Console.WriteLine("\n2.CONSULTAR ESTADO DE ENVÍO");
            Console.WriteLine("\n3.CONSULTAR ESTADO DE CUENTA");
            Console.WriteLine("\n4.SALIR");
            Console.WriteLine("******************************************************************************************\n");
            
        }


       
        public static void estado_servicio()
        {
            string ingreso = "";
            int codigo_orden = 0;
            do
            {
                Console.WriteLine("\n******************************************************************");
                Console.WriteLine("Ingrese el nº de orden de servicio".ToUpper());
                Console.WriteLine("********************************************************************\n");
                ingreso = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl el nº de orden no debe de ser vacio, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                if (!Int32.TryParse(ingreso, out codigo_orden))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl nº de orden debe de ser numérico, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                if (codigo_orden < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl nº de orden debe de ser positivo, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                if (!Orden_de_servicio.validar_codigo_orden(codigo_orden))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl nº de orden ingresado no se corresponde con ninguno de los elementos dentro de la base de datos del repositorio, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.ForegroundColor = ConsoleColor.White;
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
               Console.WriteLine("******************************************************************************************\n");  
               Console.WriteLine("{0,-10} | {1,-10} | {2,5:SI;0;NO} | {3,5} | {4,5: dd/MM/yyyy}| {5,10} | {6,10} | {7,10} ", "Cod operac", "Estado", "Fact ", "Pago", " Fecha      ", "Cargos", "Abonos  ", "Descripción   ");
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
                                           Console.WriteLine("Por favor ingrese la fecha inicial con el formato dd/mm/aaaa\n");
                                           fechaINI = Console.ReadLine();
                                           Console.WriteLine("Por favor ingrese la fecha final con el formato dd/mm/aaaa\n");
                                           fechaFIN = Console.ReadLine();
                                           Console.WriteLine("\n******************************************************************************************\n");

                                           DateTime.TryParse(fechaINI, out inicial);
                                           DateTime.TryParse(fechaFIN, out final);
                                           if (inicial!= null &&  final!=null)
                                           {
                                            Console.WriteLine("{0,-10} | {1,-10} | {2,5:SI;0;NO} | {3,5} | {4,5: dd/MM/yyyy}| {5,10} | {6,10} | {7,10} ", "Cod operac", "Estado", "Fact ", "Pago", " Fecha      ", "Cargos", "Abonos  ", "Descripción   ");   
                                            EstadoCuenta.filtrarPorFechas(codigo_cliente, inicial, final);
                                           }
                                           else
                                           {
                                            Console.WriteLine("No ha ingresado una fecha válida, por favor intente nuveamente\n");
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n**********************************************************");
                Console.WriteLine("****************INGRESO DE CÓDIGO DE CLIENTE***************");
                Console.WriteLine("***********************************************************\n");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\n---------------------------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("INGRESE SU CÓDIGO DE CLIENTE CORPORATIVO Y PRESIONE ENTER");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nINGRESE LA FRASE SALIR Y PRESIONE LA TECLA ENTER SI DESEA SALIR DEL PROGRAMA");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
                ingreso = Console.ReadLine();
               

                if(ingreso=="SALIR")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHa decidido salir definitivamente de la aplicación \nGracias por usar nuestros servicio\nLa ventana de consola se cerrara al cabo de 10 segundos");
                    Thread.Sleep(10000);

                   Environment.Exit(0);
                    Console.ForegroundColor = ConsoleColor.White;

                }

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl código de cliente corporativo no puede ser vacio, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*************************************************************************************************************\n");
                    continue;
                }
                if (!Int32.TryParse(ingreso, out codigo_cliente))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    Console.WriteLine("\nEl código de cliente corporativo debe de ser del tipo numérico, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*************************************************************************************************************\n");
                    continue;
                }
                if (codigo_cliente < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl código de cliente corporativo debe de ser positivo, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*****************************************************************************************************************\n");
                    continue;
                }
                if (ingreso.Count() != 8)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl código de cliente corporativo debe de contener 8 digitos numéricos en su extensión, se lo redirigira al campo de ingreso anterior para que tenga otra oportunidad de ingresar el dato solicitado");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("******************************************************************************************************************\n");
                    continue;
                }

                if (!Cliente_corportativo.validar_cliente(codigo_cliente))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl código de cliente corporativo ingresado no se corresponde con ninguno de los elementos ingresados dentro de la base de datos");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*******************************************************************************************************************\n");
                    continue;
                }

                if(Cliente_corportativo.validar_cliente(codigo_cliente))
                {
                    string nombreyapellido = Cliente_corportativo.hallar(codigo_cliente).nombreyapellido;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n********************************************************************");
                    Console.WriteLine("********************************************************************");
                    Console.WriteLine("********************************************************************");
                    Console.WriteLine($"\n                    B I E N V E N I D O \n\n                    {nombreyapellido.ToUpper()}");
                    Console.WriteLine("\n******************************************************************");
                    Console.WriteLine("********************************************************************");
                    Console.WriteLine("********************************************************************");
                    Console.WriteLine("\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    validar_eleccion(codigo_cliente);
                    break;
                }

                break;

            } while (true);

            
        }
       
    }
}
