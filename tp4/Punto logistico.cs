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
        public string nombre { get; set; }

        public string direccion { get; set; }

        public int codigo_postal { get; set; }// Ingresa el codigo postal y después consulta en archivo para poder completar el resto de campos




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

            if (modo == "origen")
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

                punto_geografico.direccion = asignar("dirección");
                punto_geografico.nombre = asignar("Nombre");
                punto_geografico.codigo_postal = asignar_cp();


                //Esto se asigna por sistema
                punto_geografico.localidad = asignar_localidad(punto_geografico.codigo_postal);
                punto_geografico.provincia = asignar_provincia(punto_geografico.codigo_postal);
                punto_geografico.region = asignar_region(punto_geografico.codigo_postal);
                punto_geografico.codigo_sucursal = asignar_sucursal(punto_geografico.localidad);
                punto_geografico.codigo_centro_provincial = asignar_centro_provincial(punto_geografico.provincia);
                punto_geografico.codigo_centro_regional = asignar_centro_regional(punto_geografico.region);
            }
            else
            {
                //En caso de tratarse de un envío internacional solo debo de hacerlo llegar hasta el centro regional metropolitano para que se despache hacia el exterior
                punto_geografico.direccion = asingar_extranjero();
                punto_geografico.nombre = asignar("Nombre");
                punto_geografico.localidad = null;
                punto_geografico.provincia = null;
                punto_geografico.region = null;
            }

            return punto_geografico;

        }

        private static string asignar_provincia(int codigo_postal)
        {
            var elemento = archivo.hallar(codigo_postal);

            return elemento.provincia;


        }

        private static string asignar_region(int codigo_postal)
        {
            var elemento = archivo.hallar(codigo_postal);

            return elemento.region;
        }

        private static string asignar_localidad(int codigo_postal)
        {
            var elemento = archivo.hallar(codigo_postal);

            return elemento.localidad;
        }

        private static int asignar_cp()
        {
            string cpJson = File.ReadAllText("Codigos postales.Json");
            var lista = JsonConvert.DeserializeObject<List<archivo>>(cpJson);

            string ingreso = "";
            int cp = 0;

            do
            {
                Console.WriteLine("\nIngrese el código postal y luego presione enter");
                ingreso = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("\nNo puede ingresar un código postal vacio");
                    continue;
                }

                if (!int.TryParse(ingreso, out cp))
                {
                    Console.WriteLine("\nEl código postal debe de ser un valor numérico positivo que consta de 4 dígitos");
                    continue;
                }

                if (cp < 0)
                {
                    Console.WriteLine("\nEl código postal debe de ser un valor numérico positivo que consta de 4 dígitos");
                    continue;
                }
                if (ingreso.Length != 4)
                {
                    Console.WriteLine("\nEl código postal debe de ser un valor numérico positivo que consta de 4 dígitos");
                    continue;
                }

                if (!archivo.validar_cp(cp))
                {
                    Console.WriteLine("\nEl código postal ingresado no se corresponde con ninguno del pais");
                    continue;
                }

                break;
            } while (true);

            return cp;
        }

        private static int asignar_centro_regional(string region)
        {
            var lista = Centro_Regional.abrir_archivo();

            int codigo_regional = 0;

            foreach (var reg in lista)
            {
                if (reg.nombre==region)
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

            foreach (var prov in lista)
            {
                if (prov.nombre == provincia)
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

            foreach (var sucursal in lista)
            {
                if (sucursal.localidad_dominante == localidad)
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

            if (A.provincia == B.provincia)
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

    class archivo
    {
        public int cp { get; set; }

        public string localidad { get; set; }

        public string provincia { get; set; }

        public string region { get; set; }

        public archivo(int cp, string loc, string prov, string region)
        {
            this.cp = cp;
            this.localidad = loc;
            this.provincia = prov;
            this.region = region;

        }

        public static void generar_archivo()
        {
            var carga1 = new archivo(0001, "Duende", "Tucuman", "Norte");
            var carga2 = new archivo(0002, "Dragon", "Cordoba", "Centro");
            var carga3 = new archivo(0003, "Hada", "Buenos Aires", "Metropolitana");
            var carga4 = new archivo(0004, "Golem", "Chubut", "Sur");
            var carga5 = new archivo(0005, "Mago", "CABA", "Metropolitano");
            var carga6 = new archivo(0006, "Ninfa", "Tucuman", "Norte");
            var carga7 = new archivo(0007, "Fantasma", "Cordoba", "Centro");
            var carga8 = new archivo(0008, "Chupacabras", "Buenos Aires", "Metropolitana");
            var carga9 = new archivo(0009, "Vampiro", "Chubut", "Sur");
            var carga10 = new archivo(0010, "Momia", "CABA", "Metropolitano");


            var lista = new List<archivo>();

            lista.Add(carga1);
            lista.Add(carga2);
            lista.Add(carga3);
            lista.Add(carga4);
            lista.Add(carga5);
            lista.Add(carga6);
            lista.Add(carga7);
            lista.Add(carga8);
            lista.Add(carga9);
            lista.Add(carga10);

            string listaJson = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Codigos postales.Json", listaJson);

        }

        public static bool validar_cp(int cp)
        {
            string cpJson = File.ReadAllText("Codigos postales.Json");
            var lista = JsonConvert.DeserializeObject<List<archivo>>(cpJson);
            bool flag = false;
            foreach(var elemento in lista)
            {
                if(elemento.cp==cp)
                {
                    flag = true;
                }
            }

            return flag;
        }

        public archivo()
        { }

        public static archivo hallar(int cp)
        {
            var arch = new archivo();
            string cpJson = File.ReadAllText("Codigos postales.Json");
            var lista = JsonConvert.DeserializeObject<List<archivo>>(cpJson);

            foreach(var elemento in lista)
            {
                if(elemento.cp==cp)
                {
                    arch = elemento;
                    break;

                }

            }

            return arch;
        }
    }
}
