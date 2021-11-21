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

        public string continente_asignado { get; set; }
       

        public static Punto_logistico crear(string modo, string campo)
        {
            
            Punto_logistico punto_geografico = new Punto_logistico();
           
            
            string cadena = "";
          

            if (modo == "origen")
            {
                if (campo == "Retirado en domicilio")
                {
                    cadena = "domicilio";
                }
                else if (campo == "Retirado en sucursal")
                {
                    cadena = "sucursal";
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n*********************************************************************************************************************");
                Console.WriteLine("INGRESE INFORMACIÓN CORRESPONDIENTE A DONDE SE HALLA UBICADO");
                Console.WriteLine("**********************************************************************************************************************\n");
                Console.ForegroundColor = ConsoleColor.White;
                
               
                
                if(cadena=="domicilio")
                {
                    punto_geografico.direccion = asignar("dirección");
                }
                else
                {
                    punto_geografico.direccion = "";
                }


                punto_geografico.codigo_postal = asignar_cp(punto_geografico.direccion);
                punto_geografico.localidad = asignar_localidad(punto_geografico.codigo_postal);
                punto_geografico.provincia = asignar_provincia(punto_geografico.codigo_postal);
                punto_geografico.region = asignar_region(punto_geografico.codigo_postal);
                punto_geografico.pais = "Argentina";


            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n*********************************************************************************************************************");
                Console.WriteLine("INGRESE LA INFORMACIÓN DE DESTINO DEL ENVÍO");
                Console.WriteLine("**********************************************************************************************************************\n");
                Console.ForegroundColor = ConsoleColor.White;
                if (campo == "Entregado en domicilio")
                {
                    cadena = "domicilio";
                }
                else if (campo == "Entregado en sucursal")
                {
                    cadena = "sucursal";
                }

                punto_geografico.pais = asignar("pais");

                if (punto_geografico.pais == "Argentina")
                {
                    if (cadena == "domicilio")
                    {
                        punto_geografico.direccion = asignar("dirección");
                        punto_geografico.nombre = asignar("Nombre");
                        punto_geografico.codigo_postal = asignar_cp(punto_geografico.direccion);
                    }
                    else
                    {
                        punto_geografico.direccion = "";
                        punto_geografico.nombre = asignar("Nombre");
                        punto_geografico.codigo_postal = asignar_cp(punto_geografico.direccion);
                    }


                    //Esto se asigna por sistema
                    punto_geografico.localidad = asignar_localidad(punto_geografico.codigo_postal);
                    punto_geografico.provincia = asignar_provincia(punto_geografico.codigo_postal);
                    punto_geografico.region = asignar_region(punto_geografico.codigo_postal);
                    punto_geografico.continente_asignado = "America del sur limitrofe";

                }
                else
                {
                    //En caso de tratarse de un envío internacional solo debo de hacerlo llegar hasta el centro regional metropolitano para que se despache hacia el exterior
                    punto_geografico.direccion = asingar_extranjero();
                    punto_geografico.nombre = asignar("Nombre");
                    punto_geografico.codigo_postal = 0000;
                    punto_geografico.localidad = null;
                    punto_geografico.provincia = null;
                    punto_geografico.region = null;
                    punto_geografico.continente_asignado = continente.pasar_continente(punto_geografico.pais);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n******INFORMACIÓN DE DESTINO********");
                    Console.WriteLine("PAIS DE DESTINO: {0}", punto_geografico.pais);
                    Console.WriteLine("CONTINENTE DE DESTINO: {0}", punto_geografico.continente_asignado);
                    Console.WriteLine("NOMBRE DE DESTINATARIO: {0}", punto_geografico.nombre);
                    Console.WriteLine("DIRECCIÓN DE DESTINO: {0}", punto_geografico.direccion);
                    Console.WriteLine("***************************************");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nIngrese S y luego ENTER para confirmar".ToUpper());
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nIngrese cualquier tecla y luego ENTER para abortar la operación".ToUpper());
                    Console.ForegroundColor = ConsoleColor.White;

                    string teclado = "";

                    teclado = Console.ReadLine();

                    teclado = teclado.ToUpper();

                    if (teclado != "S")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n***************OPERACIÓN ABORTADA******************\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Program.validar_cliente();
                        
                    }
                   
                }
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

        private static int asignar_cp(string direccion)
        {
            
            string cpJson = File.ReadAllText("Codigos postales.Json");
            var lista = JsonConvert.DeserializeObject<List<archivo>>(cpJson);

            string ingreso = "";
            int cp = 0;

            do
            {
                Console.WriteLine("\n--------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("INGRESE EL CÓDIGO POSTAL LUEGO PRESIONE ENTER");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nINGRESE LA FRASE SALIR MÁS ENTER PARA ABORTAR LA OPERACIÓN");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------------------------------------------\n");
                ingreso = Console.ReadLine();

                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.validar_cliente();
                    break;

                }
                else
                {
                    if (string.IsNullOrWhiteSpace(ingreso))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNo puede ingresar un código postal vacio");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }

                    if (!int.TryParse(ingreso, out cp))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nEl código postal debe de ser un valor numérico positivo que consta de 4 dígitos");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }

                    if (cp < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nEl código postal debe de ser un valor numérico positivo que consta de 4 dígitos");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    if (ingreso.Length != 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nEl código postal debe e ser un valor numérico positivo que consta de 4 dígitos");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }

                    if (!archivo.validar_cp(cp))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nEl código postal ingresado no se corresponde con ninguno del pais");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }

                }

                break;
            } while (true);

            string teclado = "";



            Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n*************INFORMACIÓN POSTAL**************");
                Console.WriteLine("Código Postal: {0}", archivo.hallar(cp).cp);
                Console.WriteLine("Localidad: {0}", archivo.hallar(cp).localidad);
                Console.WriteLine("Provincia:{0}", archivo.hallar(cp).provincia);
                Console.WriteLine("Región: {0}", archivo.hallar(cp).region);
                Console.WriteLine("País: {0}", "Argentina");
            if(direccion!="")
            {
                Console.WriteLine("Dirección: {0}", direccion);
            }
          
                Console.WriteLine("************************************************\n");
            Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nINGRESE S MAS ENTER PARA CONFIRMAR LOS DATOS INGRESADOS");
            Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nINGRESE CUALQUIER TECLA MÁS ENTER PARA ABORTAR LA OPERACIÓN");
            Console.ForegroundColor = ConsoleColor.White;
                teclado = Console.ReadLine();

                teclado = teclado.ToUpper();

                if(teclado!="S")
                {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n***************OPERACIÓN ABORTADA******************\n");
                Console.ForegroundColor = ConsoleColor.White;
                Program.validar_cliente();
                    return cp;
                }
                else
                {
                    return cp;
                }

              

           
        }

   
        private static string asingar_extranjero()
        {
            string ingreso = "";

            do
            {
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("PAQUETE CON DESTINO INTERNACIONAL");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("INGRESE EL DOMICILIO INTERNACIONAL DE DESTINO");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nIngrese SALIR y presione ENTER para abortar la operación".ToUpper());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------\n");
                ingreso = Console.ReadLine();

                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.validar_cliente();
                    break;

                }

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa dirección no debe de quedar vacia");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (ingreso.Length < 6)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa dirección ingresada debe de estar completa, detectamos que la cantidad de caracteres ingresados es insuficiente");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (!ingreso.Any(char.IsDigit))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa dirección en el extranjero debe de contar de manera estricta con el nombre de calle y la altura en número de donde se desea enviar");
                    Console.ForegroundColor = ConsoleColor.White;
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
                Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Ingrese los datos que se corresponde con el campo {campo} y luego presione la tecla enter".ToUpper());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nIngrese SALIR y presione ENTER para abortar la operación".ToUpper());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("-----------------------------------------------------------------------------------------------\n");
                ingreso = Console.ReadLine();


                if (ingreso == "SALIR" || ingreso == "salir")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nSe lo redirigirá a la pantalla inicial \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.validar_cliente();
                    break;

                }
                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nEl campo {campo} no puede permanecer vacio");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (ingreso.Length < 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nEl campo {campo} no pude contener pocos elementos de escritura");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                if ((!Punto_logistico.leer_pais(ingreso)) && campo=="pais")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEl elemento ingresado no se corresponde con un pais válido dentro de la lista");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (campo != "dirección")
                {
                    if (ingreso.Any(char.IsDigit))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nEl campo {campo} no puede tener elementos numéricos dentro de su definición");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                }

                if (campo=="dirección" && !ingreso.Any(char.IsDigit))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nDebe de agregar la altra de la calle ingresada");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
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

        public Punto_logistico()
        {

        }

        public Punto_logistico(string n, string domicilio, int cp, string pais)
        {
            this.nombre = n;
            this.direccion = domicilio;
            this.codigo_postal = cp;
            this.pais = pais;

            if (pais == "Argentina")
            {


                this.provincia = asignar_provincia(cp);
                this.localidad = asignar_localidad(cp);
                this.region = asignar_region(cp);
                
            }
        
        }
        /*MODULO CORRESPONDIENTE A LA CARGA DE ELEMENTOS DE PRUEBA, EMPLEADOS EN UNA VERSIÓN INICIAL DEL PROGRAMA
          * AHORA QUEDAN COMO COMENTARIOS A MODO DE EJEMPLO DADO QUE PARA EL MANEJO DE ARCHIVO SE DESARROLLARÁ UNA CARGA EXTERNA Y SU CONSIGUIENTE CARGA DENTRO DE LA CARPETA DEL PROGRAMA PARA SU UTILIZACIÓN
        public static void cargar_paises()
        {
            List<string> lista = new List<string>();

            lista.Add("Argentina");
            lista.Add("Chile");
            lista.Add("Paraguay");
            lista.Add("Estados Unidos");
            lista.Add("Canada");
            lista.Add("Colombia");
            lista.Add("Peru");
            lista.Add("Alemania");
            lista.Add("Italia");
            lista.Add("Japon");
            lista.Add("China");
            lista.Add("Australia");
            lista.Add("Nueva Zelanda");

            string listaJson = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Paises.Json", listaJson);

        }
*/
        public static bool leer_pais(string pais)
        {
            string cadena = File.ReadAllText("Paises.Json");
            var lista = JsonConvert.DeserializeObject<List<string>>(cadena);
            bool flag = false;
            foreach(var p in lista)
            {
                if (p == pais) 
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }


    }

    class continente
    {
        public string nombre { get; set; }

        public List<String> paises { get; set; }

        public continente()
        { }

        public static string pasar_continente (string pais)
        {
            string arreglo = File.ReadAllText("Continentes.Json");

            var lista = JsonConvert.DeserializeObject<List<continente>>(arreglo);
            string continente = "";
            foreach(var elemento in lista)
            {
                var lista_paises = elemento.paises;

                foreach(var p in lista_paises)
                {
                    if(p==pais)
                    {
                        continente = elemento.nombre;
                    }
                }
            }

            return continente;
        }
        /*
         * MODULO CORRESPONDIENTE A LA CARGA DE ELEMENTOS DE PRUEBA, EMPLEADOS EN UNA VERSIÓN INICIAL DEL PROGRAMA
 * AHORA QUEDAN COMO COMENTARIOS A MODO DE EJEMPLO DADO QUE PARA EL MANEJO DE ARCHIVO SE DESARROLLARÁ UNA CARGA EXTERNA Y SU CONSIGUIENTE CARGA DENTRO DE LA CARPETA DEL PROGRAMA PARA SU UTILIZACIÓN
        public static void generar_archivo()
        {
            var c1 = new continente();
            c1.nombre = "America del sur limitrofe";
            c1.paises =new List<string> { "Chile" ,"Paraguay" };
            var c2 = new continente();
            c2.nombre = "America del sur";
            c2.paises= new List<string> { "Colombia", "Peru" };
            var c3 = new continente();
            c3.nombre = "America del norte";
            c3.paises = new List<string> { "Estados Unidos", "Canada" };
            var c4 = new continente();
            c4.nombre = "Europa";
            c4.paises = new List<string> { "Alemania", "Italia" };
            var c5 = new continente();

            c5.nombre = "Asia";
            c5.paises = new List<string> { "Japon", "China" };
            var c6 = new continente();

            c6.nombre = "Oceania";
            c6.paises = new List<string> { "Nueva Zelanda", "Australia" };

            var lista = new List<continente>();
            lista.Add(c1);
            lista.Add(c2);
            lista.Add(c3);
            lista.Add(c4);
            lista.Add(c5);
            lista.Add(c6);

            string listaJson = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Continentes.Json", listaJson);
        }
        */
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
        /*
         * MODULO CORRESPONDIENTE A LA CARGA DE ELEMENTOS DE PRUEBA, EMPLEADOS EN UNA VERSIÓN INICIAL DEL PROGRAMA
 * AHORA QUEDAN COMO COMENTARIOS A MODO DE EJEMPLO DADO QUE PARA EL MANEJO DE ARCHIVO SE DESARROLLARÁ UNA CARGA EXTERNA Y SU CONSIGUIENTE CARGA DENTRO DE LA CARPETA DEL PROGRAMA PARA SU UTILIZACIÓN
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
        */
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
