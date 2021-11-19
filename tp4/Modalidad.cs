using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Modalidad
    {
        public string modo_retiro { get; set; }
        public string modo_entrega { get; set; }
        public string tipo_envio { get; set; } //si es normal o urgente, muy importante a la hora de las designaciones y de poder fijar el precio por el servicio

        public static Modalidad crear()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n***************************************************************************************************");
            Console.WriteLine("*******************COMPLETE LA INFORMACIÓN SOLICITADA PARA GENERAR EL ENVÍO***********************");
            Console.WriteLine("***************************************************************************************************\n");
            Console.ForegroundColor = ConsoleColor.White;
            var modalidad = new Modalidad();
            modalidad.modo_retiro = asignar_retiro();
            modalidad.modo_entrega = asignar_entrega();
            modalidad.tipo_envio = asignar_tipo();

            return modalidad;
        }

        private static string asignar_entrega()
        {
            string ingreso = "";

            do
            {
                
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("------------------------------------------FORMA DE ENTREGA A DESTINATARIO-------------------------------------");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------\n");
               
                Console.WriteLine("INGRESE LA OPCIÓN Y LUEGO PRESIONE ENTER");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("A.EL DESTINATARIO RECOGERA EL PAQUETE DENTRO DE LA SUCURSAL");
                Console.WriteLine("B.EL PAQUETE LLEGARA AL DOMICILIO DEL DESTINATARIO");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("INGRESE LA FRASE SALIR Y PRESIONE ENTER PARA ABORTAR LA OPERACIÓN");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------\n");

                ingreso = Console.ReadLine().ToUpper();
                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n***************OPERACIÓN ABORTADA******************");
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.validar_cliente();
                    break;

                }
                if (string.IsNullOrEmpty(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo puede ingresar vacio como un a opción válida");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (int.TryParse(ingreso, out int salida))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa opción ingresada no puede ser numérica");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (ingreso == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nHa seleccionado que el destinatario recibira el paquete dentro de la sucursal pertinente".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    return "Entregado en sucursal";
                    

                }

                if (ingreso == "B")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nHa seleccionado que desea que el paquete llegue al domicilio indicado para el destinatario".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    return "Entregado en domicilio";

                  
                }

                if (ingreso != "A" && ingreso != "B")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa opción ingresada no se corresponde con ninguna de las opciones disponibles");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            } while (true);

            return ingreso;
        }

        private static string asignar_tipo()
        {
            string ingreso = "";

            do
            {
                
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("---------------------------------TIEMPOS DE ENVÍO---------------------------------------------------------------");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------\n");
                
                Console.WriteLine("INGRESE LA OPCIÓN Y PRESIONE ENTER");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("A.NORMAL, EL TIEMPO DE ENVÍO DEPENDERA DE LA DISTANCIA");
                Console.WriteLine("B.Urgente, EL PAQUETE DEMORARA 48 HORAS EN LLEGAR A DESTINO");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("INGRESE LA FRASE SALIR Y LUEGO PRESIONE ENTER PARA ABORTAR LA OPERACIÓN");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------\n");
                ingreso = Console.ReadLine().ToUpper();
                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n***************OPERACIÓN ABORTADA******************");
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.validar_cliente();
                    break;

                }
                if (string.IsNullOrEmpty(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo puede ingresar vacio como un a opción válida");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (int.TryParse(ingreso, out int salida))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa opción ingresada no puede ser numérica");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (ingreso == "B")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nHa seleccionado que quiere una entrega urgente".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    return "Urgente";
                    

                }

                if (ingreso == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nHa seleccionado que quiere una entrega normal".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    return "Normal";

                    
                }

                if (ingreso != "A" && ingreso != "B")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa opción ingresada no se corresponde con ninguna de las opciones disponibles");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            } while (true);

            return ingreso;
        }

        private static string asignar_retiro()
        {
            
            string ingreso = "";

            do
            {
                
                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("-----------------------------------FORMA EN QUE DEPOSITARA EL PAQUETE AL INICIO DE LA OPERACIÓN----------------------");
               Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                
                Console.WriteLine("INGRESE LA OPCIÓN Y LUEGO PRESIONE ENTER");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("A.LLEVO EL PAQUETE HASTA LA SUCURSAL");
                Console.WriteLine("B.QUIERO QUE RETIREN EL PAQUETE EN MI DOMICILIO");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("INGRESE LA FRASE SALIR Y PRESIONE ENTER PARA ABORTAR LA OPERACIÓN");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------\n");
                ingreso = Console.ReadLine().ToUpper();

                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n***************OPERACIÓN ABORTADA******************");
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.validar_cliente();
                    break;

                }

                if (string.IsNullOrEmpty(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo puede ingresar vacio como un a opción válida");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if(int.TryParse(ingreso, out int salida))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa opción ingresada no puede ser numérica");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (ingreso == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nHa seleccionado que dejara el paquete dentro de la sucursal de su localidad".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    return "Retirado en sucursal";
                    
                  

                }

                if(ingreso=="B")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nHa seleccionado que se pase a retirar el paquete a su domicilio".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;
                    return "Retirado en domicilio";

                    
                }

                if(ingreso!="A" && ingreso!="B")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa opción ingresada no se corresponde con ninguna de las opciones disponibles");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            } while (true);

            return ingreso;
        }

        public Modalidad()
        {

        }

        public Modalidad(string ret, string ent, string tipo)
        {
            this.modo_entrega = ent;
            this.modo_retiro = ret;
            this.tipo_envio = "urgente";

        }
    }
}
