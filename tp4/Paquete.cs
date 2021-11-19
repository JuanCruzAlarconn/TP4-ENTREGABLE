using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Paquete
    {
        public decimal peso { get; set; }
        public string tipo { get; set; }//Si es un sobre o un bulto

        public decimal valor_declarado { get; set; }

        public Paquete()
        {

        }

        public Paquete (decimal peso, string tipo, decimal valor)
        {
            this.peso = peso;
          
            this.valor_declarado = valor;
            this.tipo = tipo;

        }


        public static Paquete crear()
        {
            var paquete = new Paquete();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n*********************************************************************");
            Console.WriteLine("********COMPLETE INFORMACIÓN ACERCA DEL PAQUETE***********************");
            Console.WriteLine("***********************************************************************\n");
            Console.ForegroundColor = ConsoleColor.White;
            paquete.peso = asignar_peso();            
            paquete.tipo = asignar_tipo(paquete.peso);
            paquete.valor_declarado = asignar_valor_declarado();

            return paquete;
        }

        private static decimal asignar_valor_declarado()
        {
            string ingreso = "";
            decimal cotización = 0;

            do
            {
                Console.WriteLine("\n-----------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("INGRESE EL VALOR EN PESOS EL PAQUETE LUEGO PRESIONE ENTER");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ingrese SALIR y presione ENTER para abortar la operación".ToUpper()) ;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("-----------------------------------------------------------------------------------\n");
                ingreso = Console.ReadLine();
                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n***************OPERACIÓN ABORTADA******************");
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.validar_cliente();
                    break;

                }
                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo puede dejar el valor como vacio");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if(!decimal.TryParse(ingreso, out cotización))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nDebe de ingresar una cotización numérica");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if(cotización<=0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa cotización ingresada debe de ser positiva");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                break;


            } while (true);

            return cotización;
        }

        private static string asignar_tipo(decimal peso)
        {
            string tipo;
            if(peso > 500 )
            {
                tipo = "Bultos";
            }
            else
            {
                tipo = "Correspondencia";
            }

            return tipo;
        }

        private static decimal asignar_peso()
        {
            string ingreso = "";
            decimal peso = 0;

            do
            {
                Console.WriteLine("\n---------------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ingrese el peso del paquete (en gramos), luego ingrese la tecla enter".ToUpper());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ingrese SALIR y presione ENTER para abortar la operación");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------------------------------------------------------------------------------\n");
                ingreso = Console.ReadLine();
                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n***************OPERACIÓN ABORTADA******************");
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.validar_cliente();
                    break;

                }
                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo puede dejar el peso como vacio");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (!decimal.TryParse(ingreso, out peso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl peso del paquete debe de ser numérico");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (peso <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl peso del paquete debe de ser positivo");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }


                if (peso >= 30000)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("\nEl peso del paquete no puede superar los 30kg");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                break;
            } while (true);

            return peso;
        }

       
 
    }
}
