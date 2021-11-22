using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;



namespace tp4
{
    public class EstadoCuenta
    {
        public int codigo_cliente { get; set; }
        public int codigo_operacion { get; set; }
        public DateTime fecha_operacion { get; set; }
        public string concepto { get; set; }
        public int cargos { get; set; }
        public int abonos { get; set; }
        public string estado { get; set; }
        public bool pagado { get; set; }
        public bool facturado { get; set; }
        public List<EstadoCuenta> ListaEstadoCuentaPorCliente { get; set; }
        private static Random random = new Random();
        

        public static EstadoCuenta hallar(int codigo)
        {
            var lista_estadoscuenta = EstadoCuenta.abrir();

            var estado = new EstadoCuenta();

            foreach (var e in lista_estadoscuenta)
            {
                if (e.codigo_cliente == codigo)
                {
                    estado = e;
                    break;
                }
            }

            return estado;
        }

        public static EstadoCuenta mostrarCuenta(int codigo)
        {
        var lista_estadoscuenta = EstadoCuenta.abrir();    
        var estado = new EstadoCuenta();
        foreach (var e in lista_estadoscuenta)
            {
                if (e.codigo_cliente == codigo)
                {

                    estado = e;
                    Console.WriteLine(e.ToString());
                }
                   
            } 
        return estado;
        }

        public static EstadoCuenta filtrarPorFechas(int codigo, DateTime Ini, DateTime Fin)
        {
        var lista_estadoscuenta = EstadoCuenta.abrir();    
        var estado = new EstadoCuenta();
        int saldo = 0;
        foreach (var e in lista_estadoscuenta)
            {//x.StartDate >= sdate && x.EndDate <= edate)

                if (e.codigo_cliente == codigo && (e.fecha_operacion >= Ini && Fin>=e.fecha_operacion) )
                {

                    estado = e;
                    Console.WriteLine(e.ToString());
                    saldo = saldo + e.abonos -e.cargos;

                }          
            } 
        Console.WriteLine("Su saldo entre ambas fechas es de: "+saldo.ToString()+" pesos");     
        Console.WriteLine("******************************************************************************************\n");            
        return estado;
        }


        public static List<EstadoCuenta> abrir()
        {
            var lista_estadoscuenta = new List<EstadoCuenta>();

            var listaEstadoCuentaJson = File.ReadAllText("EstadoCuentaLista.json");

            lista_estadoscuenta = JsonConvert.DeserializeObject<List<EstadoCuenta>>(listaEstadoCuentaJson);

            return lista_estadoscuenta;
        }



        public static bool ValidarClaveSecreta(string clave_secreta)
        {   bool bandera = true;
            int contador=1;
            int intentos = 3;
            string ingresoCaptcha = "";
            string ingresoClave = "";
            string Hash = EstadoCuenta.RandomString(5);
            Console.WriteLine("Por favor ingrese el CAPTCHA, presione ENTER y a continuación su clave secreta y presione ENTER");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("CAPTCHA: "+Hash);
            Console.ForegroundColor = ConsoleColor.White;
            do { 

                ingresoCaptcha = Console.ReadLine();
                ingresoClave = Console.ReadLine();
                if ((ingresoClave == clave_secreta && ingresoCaptcha == Hash) == false)
                {
                    intentos--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ingreso un CAPTCHA y o clave secreta erroneos, restan " + intentos + " intentos".ToUpper());
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Hash: " + Hash);
                    Console.ForegroundColor = ConsoleColor.White;
                    contador++;
                }

                if (contador==4)
                {
                bandera = false;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Se devuelve al menú anterior".ToUpper());
                Console.ForegroundColor = ConsoleColor.White;
                }
                }

                
            while (!((ingresoClave==clave_secreta && ingresoCaptcha==Hash)|| 4<=contador));
            return bandera; 
        }              



        public static string RandomString(int length)
        {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public static EstadoCuenta CalcularSaldoCta(int codigo)
        {
            var lista_estadoscuenta = EstadoCuenta.abrir();
            var estado = new EstadoCuenta();
            int saldo = 0;

            foreach (var e in lista_estadoscuenta)
            {
                if (e.codigo_cliente == codigo)
                {

                    estado = e;
                    saldo = saldo + e.abonos -e.cargos;

                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Su saldo actual es de: "+saldo.ToString()+" pesos");
            Console.ForegroundColor = ConsoleColor.White;
            return estado;
        }


        public override string ToString()
        {
            string yesNo=(facturado==true)? "Si":"No";
            string yesNo2=(pagado==true)? "Si":"No"; 
            return string.Format("{0,-10} | {1,-10} | {2,5:SI;0;NO} | {3,5} | {4,5: dd/MM/yyyy} | {5,10} | {6,10} | {7,10} ", codigo_operacion, estado, yesNo, yesNo2,  fecha_operacion, cargos, abonos, concepto);

        }


    }
}
