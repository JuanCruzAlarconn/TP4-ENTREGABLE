using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Punto_logistico
    {
        public string nombre_y_apellido { get; set; }

        public int DNI_o_Documentacion_correspondiente { get; set; }

        public string direccion { get; set; }
        public string localidad { get; set; }

        public string provincia { get; set; }

        public string region { get; set; }

        public string pais { get; set; }

        public int codigo_sucursal { get; set; }
        public int codigo_centro_provincial { get; set; }
        public int codigo_centro_regional { get; set; }

        public static Punto_logistico crear(string modo)
        {
            Punto_logistico punto_geografico = new Punto_logistico();

            if(modo=="origen")
            {
                Console.WriteLine("\nA continuación se solicitara el ingreso de los datos correspondientes con el punto de origen de la operación\n");
            }
            else
            {
                Console.WriteLine("\nA continuación se solicitara el ingreso de los datos correspondientes con el punto de destino de la operación\n");
            }

            punto_geografico.pais = asignar("pais");

            if (punto_geografico.pais == "Argentina")
            {
                punto_geografico.nombre_y_apellido = asignar("nombre y apellido");
                punto_geografico.DNI_o_Documentacion_correspondiente = asignar_documentacion();
                punto_geografico.direccion = asignar("dirección");
                punto_geografico.localidad = asignar("localidad");
                punto_geografico.provincia = asignar("provincia");
                

                //Esto se asigna por sistema
                punto_geografico.codigo_sucursal = asignar_sucursal(punto_geografico.localidad);
                punto_geografico.codigo_centro_provincial = asignar_centro_provincial(punto_geografico.provincia);
                punto_geografico.codigo_centro_regional = asignar_centro_regional(punto_geografico.codigo_centro_provincial);
            }
            else
            {
                //En caso de tratarse de un envío internacional solo debo de hacerlo llegar hasta el centro regional metropolitano para que se despache hacia el exterior
                punto_geografico.direccion = asingar_extranjero();
                punto_geografico.nombre_y_apellido = asignar("nombre y apellido");
                punto_geografico.DNI_o_Documentacion_correspondiente = asignar_documentacion();
                punto_geografico.localidad = null;
                punto_geografico.provincia = null;
                punto_geografico.region = null;
            }

            return punto_geografico;

        }

        private static int asignar_documentacion()
        {
            string ingreso = "";
            int documento = 0;

            do
            {
                Console.WriteLine("\nIngrese la documentación correpondiente");
                ingreso = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("\nLa documentación ingresado no puede ser nula");
                    continue;
                }

                if (!int.TryParse(ingreso, out documento))
                {
                    Console.WriteLine("\nLa documentación ingresada debe de ser numérica");
                    continue;
                }

                if (documento <= 0)
                {
                    Console.WriteLine("\nLa documentación ingresada debe de ser un número positivo");
                    continue;
                }

                if (ingreso.Length < 7)
                {
                    Console.WriteLine("\nLa documentación ingresado debe de contar como mínimo con 7 elementos numéricos positivos");
                    continue;
                }

                break;
            } while (true);

            return documento;
        }

        private static int asignar_centro_regional(int codigo_provincia)
        {
            var lista = Centro_Regional.abrir_archivo();

            int codigo_regional = 0;

            foreach(var reg in lista)
            {
                if(reg.codigos_centros_provinciales_incluidos.Contains(codigo_provincia))
                {
                    codigo_regional = reg.codigo;
                    break;
                }
            }

            return codigo_regional;

        }

        private static int asignar_centro_provincial(string provincia)
        {
            var lista = Centro_Provincial.abrir_archivo();
            int codigo_provincia = 0;

            foreach(var prov in lista)
            {
                if(prov.ubicacion==provincia)
                {
                    codigo_provincia = prov.codigo;
                }

            }

            return codigo_provincia;
        }

        private static int asignar_sucursal(string localidad)
        {
            var lista = Sucursal.abrir_archivo();
            int codigo_sucursal = 0;

            foreach(var sucursal in lista)
            {
                if(sucursal.localidad_dominante=="localidad")
                {
                    codigo_sucursal = sucursal.codigo_sucursal;
                }
            }

            return codigo_sucursal;
        }

        private static string asingar_extranjero()
        {
            string ingreso = "";

            do
            {
                Console.WriteLine("\nHa ingresado que el paquete cuenta con un destino internacional, se solicitara que ingrese de forma estricta la dirección en el extranjero a la que hace referencia");
                ingreso = Console.ReadLine();


                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("\nLa dirección no debe de quedar vacia");
                    continue;
                }

                if (ingreso.Length < 6)
                {
                    Console.WriteLine("\nLa dirección ingresada debe de estar completa, detectamos que la cantidad de caracteres ingresados es insuficiente");
                    continue;
                }

                if (!ingreso.Any(char.IsDigit))
                {
                    Console.WriteLine("\nLa dirección en el extranjero debe de contar de manera estricta con el nombre de calle y la altura en número de donde se desea enviar");
                    continue;
                }

                break;
            } while (true);

            return ingreso;
        }

        private static string asignar(string campo)
        {
            string ingreso = "";

            do
            {
                Console.WriteLine($"\nIngrese los datos que se corresponde con el {campo}");
                ingreso = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine($"\nEl campo {campo} no puede permanecer vacio");
                    continue;
                }

                if (ingreso.Length < 5)
                {
                    Console.WriteLine($"\nEl campo {campo} no pude contener pocos elementos de escritura");
                    continue;
                }

                if (campo != "dirección")
                {
                    if (ingreso.Any(char.IsDigit))
                    {
                        Console.WriteLine($"\nEl campo {campo} no puede tener elementos numéricos dentro de su definición");
                        continue;
                    }
                }

                break;
            } while (true);

            return ingreso;
        }

        public static string tipo_envio(Punto_logistico A, Punto_logistico B)
        {
            //Con esto puedo ver como debo de asignar los elementos de transporte y como debo de realizar los cobros
            string tipo = "";

            if (A.localidad == B.localidad)
            {
                tipo = "local";

            }

            if(A.provincia==B.provincia)
            {
                tipo = "provincial";
            }

            if (A.region == B.region)
            {
                tipo = "regional";
            }

            if (A.pais != B.pais)
            {
                tipo = "internacional";
            }

            return tipo;
        }
    }
}
