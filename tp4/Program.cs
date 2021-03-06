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


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nIngrese una tecla para poder detener la ejecución".ToUpper());
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
                    Console.WriteLine("\nNo puede ingresar un espacio vacio como opción a elegir".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if(int.TryParse(ingreso, out int salida))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLas opciones disponibles no son numéricas".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if(ingreso.Length!=1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nSolo debe de ingresar na de las opciones disponibles dentro del menu".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (ingreso == "A")
                {
                    
                    Program.validar_identidad();
                    
                  
                    



                    break;
                }

                if (ingreso == "B")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHa seleccionado la opción salir de la aplicación".ToUpper());
                    Console.WriteLine("\nGracias por utilizar nuestros servicios".ToUpper());
                    Console.WriteLine("\nLa ventana de consola se cerrara al cabo de 10 segundos".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(10000);

                    Environment.Exit(0);

                    break;
                   
                }
                if (ingreso!="B" && ingreso!="A")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nCOMANDO INVÁLIDO");
                    Console.ForegroundColor = ConsoleColor.White;
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNO PUEDE INGRESAR UNA OPCIÓN VACÍA");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if(!Int32.TryParse(ingreso,out opcion))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLA OPCIÓN DEBE DE SER NUMÉRICA");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if(ingreso.Count()!=1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nSOLO DEBE DE INGRESAR UNA OPCIÓN");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
              

                if (opcion==1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n**************************************************");
                    Console.WriteLine("*****************GENERAR ENVÍO************************");
                    Console.WriteLine("****************************************************\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    AppCrearOrdenes app = new AppCrearOrdenes();
                    app.ejecutar(codigo_cliente);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nSE LO REDIRIGIRÁ AL MENÚ ANTERIOR\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    validar_eleccion(codigo_cliente);
                    break;
                }

                if (opcion==2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n**************************************************");
                    Console.WriteLine("******************CONSULTAR ESTADO DE ENVÍO***********");
                    Console.WriteLine("****************************************************\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Orden_de_servicio.consultar_estado(codigo_cliente);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nSE LO REDIRIGIRÁ AL MENÚ ANTERIOR\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    validar_eleccion(codigo_cliente);
                    break;
                }
                if (opcion==3)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n**************************************************");
                    Console.WriteLine("******************CONSULTAR ESTADO DE CUENTA***********");
                    Console.WriteLine("****************************************************\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    estado_cuenta(codigo_cliente);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nSE LO REDIRIGIRÁ AL MENÚ ANTERIOR\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    validar_eleccion(codigo_cliente);
                    break;

                }
                if (opcion==4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHa seleccionado la opción salir de la aplicación".ToUpper());
                    Console.WriteLine("\nGracias por utilizar nuestros servicios".ToUpper());
                    Console.WriteLine("\nLa ventana de consola se cerrara al cabo de 10 segundos".ToUpper());
                    Thread.Sleep(10000);
                    Console.ForegroundColor = ConsoleColor.White;
                    Environment.Exit(0);

                    break;
                }
                if (opcion!=1 && opcion !=2 && opcion !=3 && opcion !=4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa opción registrada no se correponde con ninguna de las opciones disponibles dentro del sistema".ToUpper());
                    
                    Console.WriteLine("\nSE LO REDIRIGIRÁ AL MENÚ ANTERIOR PARA QUE PUEDA SEGUIR OPERANDO\n");
                    Console.ForegroundColor = ConsoleColor.White;
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
            Console.WriteLine("\nINGRESE LA OPCIÓN Y LUEGO PRESIONE ENTER");
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
                    Console.WriteLine("\nEl el nº de orden no debe de ser vacio".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                if (!Int32.TryParse(ingreso, out codigo_orden))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl nº de orden debe de ser numérico".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                if (codigo_orden < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl nº de orden debe de ser positivo".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                if (!Orden_de_servicio.validar_codigo_orden(codigo_orden))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNÚMERO DE ORDEN INGRESADO INVÁLIDO");
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
               Console.ForegroundColor = ConsoleColor.DarkCyan;
               Console.WriteLine("******************************************************************************************\n");
               Console.WriteLine("\nEjecución de la rutina consultar estado de cuenta".ToUpper());
               string clavesecreta = (Cliente_corportativo.hallar(codigo_cliente).clave_secreta).ToString();
               Console.ForegroundColor = ConsoleColor.Green;
               Console.WriteLine($"\nBienvenido, su clave secreta es  {clavesecreta}" + " por si no la recuerda\n".ToUpper());
               Console.ForegroundColor = ConsoleColor.White;
               if  ((EstadoCuenta.ValidarClaveSecreta(clavesecreta))== true)
               {
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine("");
               Console.WriteLine("A continuación se le mostrará el estado de cuenta".ToUpper());
               Console.WriteLine("");
               Console.ForegroundColor = ConsoleColor.DarkCyan;
               Console.WriteLine("******************************************************************************************\n");
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine("{0,-10} | {1,-10} | {2,5:SI;0;NO} | {3,5} | {4,5: dd/MM/yyyy}| {5,10} | {6,10} | {7,10} ", "Cod operac", "Estado", "Fact ", "Pago", " Fecha      ", "Cargos", "Abonos  ", "Descripción   ");
               EstadoCuenta.mostrarCuenta(codigo_cliente);
               EstadoCuenta.CalcularSaldoCta(codigo_cliente);
               Console.WriteLine("");
               Console.ForegroundColor = ConsoleColor.DarkCyan;
               Console.WriteLine("Si desea filtrar entre dos fechas presione la tecla A, de otra forma regresará al menú anterior".ToUpper());
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine("Si presiona cualquier otra tecla lo llevará al menú anterior".ToUpper());
               Console.ForegroundColor = ConsoleColor.DarkCyan;
               Console.WriteLine("\n******************************************************************************************\n");
               Console.ForegroundColor = ConsoleColor.White;

                    if (Console.ReadKey(true).Key == ConsoleKey.A)
                    {
                            do
                            {
                                           Console.ForegroundColor = ConsoleColor.Yellow;
                                           Console.WriteLine("");
                                           Console.WriteLine("Por favor ingrese dos fechas válidas para las que quiera filtrar\n".ToUpper());
                                           Console.WriteLine("Por favor ingrese la fecha inicial con el formato dd/mm/aaaa\n".ToUpper());
                                           fechaINI = Console.ReadLine();
                                           Console.WriteLine("Por favor ingrese la fecha final con el formato dd/mm/aaaa\n".ToUpper());
                                           fechaFIN = Console.ReadLine();
                                           Console.ForegroundColor = ConsoleColor.DarkCyan;
                                           Console.WriteLine("\n******************************************************************************************\n");
                                           Console.ForegroundColor = ConsoleColor.White;
                                           DateTime.TryParse(fechaINI, out inicial);
                                           DateTime.TryParse(fechaFIN, out final);
                                           if (inicial!= null &&  final!=null)
                                           {
                                           Console.ForegroundColor = ConsoleColor.Cyan;
                                           Console.WriteLine("{0,-10} | {1,-10} | {2,5:SI;0;NO} | {3,5} | {4,5: dd/MM/yyyy}| {5,10} | {6,10} | {7,10} ", "Cod operac", "Estado", "Fact ", "Pago", " Fecha      ", "Cargos", "Abonos  ", "Descripción   ");   
                                           EstadoCuenta.filtrarPorFechas(codigo_cliente, inicial, final);
                                           Console.ForegroundColor = ConsoleColor.White;
                                            }
                                           if (!DateTime.TryParse(fechaINI, out inicial) || !DateTime.TryParse(fechaFIN, out final))
                                            {
                                           Console.ForegroundColor = ConsoleColor.Red;
                                           Console.WriteLine("No ha ingresado una fecha válida, por favor intente nuveamente\n".ToUpper()); 
                                           Console.ForegroundColor = ConsoleColor.White;

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
                    Console.WriteLine("\nHa decidido salir definitivamente de la aplicación \nGracias por usar nuestros servicio\nLa ventana de consola se cerrara al cabo de 10 segundos".ToUpper());
                    Thread.Sleep(10000);

                   Environment.Exit(0);
                    Console.ForegroundColor = ConsoleColor.White;

                }

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl código de cliente corporativo no puede ser vacio".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*************************************************************************************************************\n");
                    continue;
                }
                if (!Int32.TryParse(ingreso, out codigo_cliente))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    Console.WriteLine("\nEL CÓDIGO DE CLIENTE DEBE SER NUMÉRICO");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*************************************************************************************************************\n");
                    continue;
                }
                if (codigo_cliente < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEL CÓDIGO DE CLIENTE DEBE SER UN NÚMERO POSITIVO");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*****************************************************************************************************************\n");
                    continue;
                }
                if (ingreso.Count() != 8)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEL CÓDIGOD DE CLIENTE DEBE TENER 8 CIFRAS");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("******************************************************************************************************************\n");
                    continue;
                }

                if (!Cliente_corportativo.validar_cliente(codigo_cliente))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nCÓDIGO DE CLIENTE INVÁLIDO");
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
