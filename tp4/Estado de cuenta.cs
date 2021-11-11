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
        
/*        public EstadoCuenta(int Codigo_cliente, int Codigo_operacion,  DateTime Fecha_operacion, string Concepto, int Cargos,  int Abonos)
        {
            codigo_cliente = Codigo_cliente;
            codigo_operacion = Codigo_operacion;
            fecha_operacion = Fecha_operacion;
            concepto = Concepto;
            cargos = Cargos;
            abonos = Abonos;
        }
 */       
        public static void carga_prueba_estadocuenta()
        {
            //Un método que permite cargar un cliente en json, para poder realizar un prueba de carga por consola
            var a = new EstadoCuenta();
            a.codigo_operacion = 12;
            a.codigo_cliente = 38456910;
            a.fecha_operacion = DateTime.Now;
            a.concepto = "Encomienda Cerro Tronador-Aldo Bonzi";
            a.cargos = 10000;
            a.abonos = 10000;
            a.estado = "Entregado";
            a.facturado = true;
            a.pagado = true;

            var b = new EstadoCuenta();
            b.codigo_operacion = 13;
            b.codigo_cliente = 38456910;
            b.fecha_operacion = DateTime.Now.AddDays(-50);
            b.concepto = "Encomienda Zárate-Tucumán";
            b.cargos = 20000;
            b.abonos = 10000;
            b.estado = "Entregado";
            b.facturado = true;
            b.pagado = true;


            var c = new EstadoCuenta();
            c.codigo_operacion = 14;
            c.codigo_cliente = 38456910;
            c.fecha_operacion = DateTime.Now.AddDays(-100);
            c.concepto = "Encomienda CABA-Río Luján";
            c.cargos = 20000;
            c.abonos = 30000;
            c.estado = "Entregado";
            c.facturado = true;
            c.pagado = true;


            var d = new EstadoCuenta();
            d.codigo_operacion = 15;
            d.codigo_cliente = 38456910;
            d.fecha_operacion = DateTime.Now.AddDays(-40);
            d.concepto = "Encomienda CABA-Santiago del Estero";
            d.cargos = 20000;
            d.abonos = 0;
            d.estado = "Entregado";
            d.facturado = true;
            d.pagado = false;


            var e = new EstadoCuenta();
            e.codigo_operacion = 16;
            e.codigo_cliente = 38456910;
            e.fecha_operacion = DateTime.Now.AddDays(-120);
            e.concepto = "Encomienda CABA-Río Luján";
            e.cargos = 60000;
            e.abonos = 30000;
            e.estado = "Entregado";
            e.facturado = true;
            e.pagado = true;

            var f = new EstadoCuenta();
            f.codigo_operacion = 1;
            f.codigo_cliente = 12345678;
            f.fecha_operacion = DateTime.Now.AddDays(-120);
            f.concepto = "Encomienda Quinterno-La Boca";
            f.cargos = 60000;
            f.abonos = 30000;
            f.estado = "Entregado";
            f.facturado = true;
            f.pagado = true;

            var g = new EstadoCuenta();
            g.codigo_operacion = 2;
            g.codigo_cliente = 12345678;
            g.fecha_operacion = DateTime.Now.AddDays(-20);
            g.concepto = "Encomienda Caño Rincon-La Yapa";
            g.cargos = 30000;
            g.abonos = 30000;
            g.estado = "Entregado";
            g.facturado = false;
            g.pagado = true;

            var h = new EstadoCuenta();
            h.codigo_operacion = 3;
            h.codigo_cliente = 12345678;
            h.fecha_operacion = DateTime.Now.AddDays(-25);
            h.concepto = "Encomienda Entre Ríos-Polonia";
            h.cargos = 40000;
            h.abonos = 30000;
            h.estado = "Tránsito";
            h.facturado = false;
            h.pagado = true;

            var i = new EstadoCuenta();
            i.codigo_operacion = 4;
            i.codigo_cliente = 12345678;
            i.fecha_operacion = DateTime.Now.AddDays(-50);
            i.concepto = "Encomienda Barbero Barbero -Barvero";
            i.cargos =10000;
            i.abonos = 0000;
            i.estado = "Tránsito";
            i.facturado = false;
            i.pagado = false;


            List<EstadoCuenta> lista = new List<EstadoCuenta>();
            lista.Add(a);
            lista.Add(b);
            lista.Add(c);
            lista.Add(d);
            lista.Add(e);
            lista.Add(f);
            lista.Add(g);
            lista.Add(h);       
            lista.Add(i);
            string estadoCliente = JsonConvert.SerializeObject(lista);

            File.WriteAllText("EstadoCuentaLista.json", estadoCliente);

        }

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



        public static void mostrar_menu_estado_de_cuenta()
        {
            Console.WriteLine("******************************************************************************************");
            Console.WriteLine("Ha Ingresado en el menú de estado de cuenta, por favor valide su clave secreta");
            Console.WriteLine("En conjunto con el HASH que aparecerá abajo");
            Console.WriteLine("******************************************************************************************\n");
        }
        public static bool ValidarClaveSecreta(string clave_secreta)
        {   bool bandera = true;
            int contador=1;
            int intentos = 3;
            string ingresoCaptcha = "";
            string ingresoClave = "";
            string Hash = EstadoCuenta.RandomString(5);
            Console.WriteLine("Por favor ingrese el CAPTCHA, presione ENTER y a continuación su clave secreta y presione ENTER");
            Console.WriteLine("CAPTCHA: "+Hash);
            do { 

                ingresoCaptcha = Console.ReadLine();
                ingresoClave = Console.ReadLine();
                if ((ingresoClave==clave_secreta && ingresoCaptcha==Hash) == false)
                {
                    intentos --;
                    Console.WriteLine("Ha ingresado un CAPTCHA y o clave secreta erroneos, le quedan "+ intentos+" intentos, le recordamos el hash "+Hash);}
                    contador ++;


                if (contador==4)
                {
                bandera = false;
                Console.WriteLine("Se devuelve al menú anterior");
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
            Console.WriteLine("Su saldo actual es de: "+saldo.ToString()+" pesos");
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
