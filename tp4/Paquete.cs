﻿using System;
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
        public int codigo { get; set; }
        public string tipo { get; set; }//Si es un sobre o un bulto

        public decimal valor_declarado { get; set; }


        public static Paquete crear()
        {
            var paquete = new Paquete();

            paquete.codigo = asignar_codigo();
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
                Console.WriteLine("\nIngrese el valor designado para el paquete, al finanlizar ingrese la tecla enter");
                ingreso = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("\nNo puede dejar el valor como vacio");
                    continue;
                }

                if(!decimal.TryParse(ingreso, out cotización))
                {
                    Console.WriteLine("\nDebe de ingresar una cotización numérica");
                    continue;
                }

                if(cotización<=0)
                {
                    Console.WriteLine("\nLa cotización ingresada debe de ser positiva");
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
                Console.WriteLine("\nIngrese el peso del paquete (en gramos) que esta interesado en enviar, luego ingrese la tecla enter");
                ingreso = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("\nNo puede dejar el peso como vacio");
                    continue;
                }

                if (!decimal.TryParse(ingreso, out peso))
                {
                    Console.WriteLine("\nEl peso del paquete debe de ser numérico");
                    continue;
                }

                if (peso <= 0)
                {
                    Console.WriteLine("\nEl peso del paquete debe de ser positivo");
                    continue;
                }


                if (peso >= 30000)
                {
                    Console.WriteLine("\nEl peso del paquete no puede superar los 30kg");
                    continue;
                }

                break;
            } while (true);

            return peso;
        }

       
        private static int asignar_codigo()
        {
            var ramdom = new Random();

            return ramdom.Next(0, 999999);
        }
    }
}
