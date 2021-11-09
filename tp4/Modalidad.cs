﻿using System;
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
            Console.WriteLine("\n***************************************************************************************************");
            Console.WriteLine("*******************COMPLETE LA INFORMACIÓN SOLICITADA PARA GENERAR EL ENVÍO***********************");
            Console.WriteLine("***************************************************************************************************\n");
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
                Console.WriteLine("Ingrese las siguientes opciones cuando a como quiere que el paquete se entregue al destinatario, luego presione enter");
                Console.WriteLine("A.El destinatario recibira el paquete dentro de la sucursal");
                Console.WriteLine("B.El destinatario recibira el paquete dentro del domicilio indicado");
                Console.WriteLine("Ingrese SALIR y presione ENTER para abortar la operación");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------\n");

                ingreso = Console.ReadLine().ToUpper();
                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Program.validar_cliente();
                    break;

                }
                if (string.IsNullOrEmpty(ingreso))
                {
                    Console.WriteLine("\nNo puede ingresar vacio como un a opción válida");
                    continue;
                }

                if (int.TryParse(ingreso, out int salida))
                {
                    Console.WriteLine("\nLa opción ingresada no puede ser numérica");
                    continue;
                }

                if (ingreso == "A")
                {
                    Console.WriteLine("\nHa seleccionado que el destinatario recibira el paquete dentro de la sucursal pertinente");
                    return "Entregado en sucursal";
                    

                }

                if (ingreso == "B")
                {
                    Console.WriteLine("\nHa seleccionado que desea que el paquete llegue al domicilio indicado para el destinatario");
                    return "Entregado en domicilio";

                  
                }

                if (ingreso != "A" && ingreso != "B")
                {
                    Console.WriteLine("\nLa opción ingresada no se corresponde con ninguna de las opciones disponibles");
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
                Console.WriteLine("---------------------------------QUE TAN RÁPIDO NECESITA QUE SE CONCRETE EL ENVÍO----------------------------");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------\n");
                Console.WriteLine("Ingrese las siguientes opciones que se corresponden con la modalidad del envío, luego presione enter");
                Console.WriteLine("A.Normal, se demorara de acuerdo con la distancia establecida del recorrido");
                Console.WriteLine("B.Urgente, desea que el paquete sea entrgado dentro de las próximas 48 hs");
                Console.WriteLine("Ingrese SALIR y presione ENTER para abortar la operación");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------\n");
                ingreso = Console.ReadLine().ToUpper();
                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Program.validar_cliente();
                    break;

                }
                if (string.IsNullOrEmpty(ingreso))
                {
                    Console.WriteLine("\nNo puede ingresar vacio como un a opción válida");
                    continue;
                }

                if (int.TryParse(ingreso, out int salida))
                {
                    Console.WriteLine("\nLa opción ingresada no puede ser numérica");
                    continue;
                }

                if (ingreso == "B")
                {
                    Console.WriteLine("\nHa seleccionado que quiere una entrega urgente");
                    return "Urgente";
                    

                }

                if (ingreso == "A")
                {
                    Console.WriteLine("\nHa seleccionado que quiere una entrega normal");
                    return "Normal";

                    
                }

                if (ingreso != "A" && ingreso != "B")
                {
                    Console.WriteLine("\nLa opción ingresada no se corresponde con ninguna de las opciones disponibles");
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
                Console.WriteLine("Ingrese las siguientes opciones cuando a como quiere que el paquete ingrese sea retirado por la empresa, luego presione enter");
                Console.WriteLine("A.Llevara el paquete hasta la sucursal que se corresponde con su localidad");
                Console.WriteLine("B.Quiero que se retire el paquete en mi domicilio");
                Console.WriteLine("Ingrese SALIR y presione ENTER para abortar la operación");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------\n");
                ingreso = Console.ReadLine().ToUpper();

                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Program.validar_cliente();
                    break;

                }

                if (string.IsNullOrEmpty(ingreso))
                {
                    Console.WriteLine("\nNo puede ingresar vacio como un a opción válida");
                    continue;
                }

                if(int.TryParse(ingreso, out int salida))
                {
                    Console.WriteLine("\nLa opción ingresada no puede ser numérica");
                    continue;
                }

                if (ingreso == "A")
                {
                    Console.WriteLine("\nHa seleccionado que dejara el paquete dentro de la sucursal de su localidad");
                    return "Retirado en sucursal";
                    
                  

                }

                if(ingreso=="B")
                {
                    Console.WriteLine("\nHa seleccionado que se pase a retirar el paquete a su domicilio");
                    return "Retirado en domicilio";

                    
                }

                if(ingreso!="A" && ingreso!="B")
                {
                    Console.WriteLine("\nLa opción ingresada no se corresponde con ninguna de las opciones disponibles");
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
