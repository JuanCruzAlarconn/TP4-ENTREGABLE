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
        public List<EstadoCuenta> ListaEstadoCuentaPorCliente { get; set; }

        
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
            a.concepto = "calle falsa 123";
            a.cargos = 10000;
            a.abonos = 10000;

            var b = new EstadoCuenta();
            b.codigo_operacion = 13;
            b.codigo_cliente = 38456910;
            b.fecha_operacion = DateTime.Now.AddDays(50);
            b.concepto = "calle falsa 123";
            b.cargos = 20000;
            b.abonos = 10000;

            var c = new EstadoCuenta();
            c.codigo_operacion = 13;
            c.codigo_cliente = 38456910;
            c.fecha_operacion = DateTime.Now.AddDays(100);
            c.concepto = "calle falsa 123";
            c.cargos = 20000;
            c.abonos = 30000;

            List<EstadoCuenta> lista = new List<EstadoCuenta>();
            lista.Add(a);
            lista.Add(b);
            lista.Add(c);
           
            string estadoCliente = JsonConvert.SerializeObject(lista);

            File.WriteAllText("EstadoCuentaLista.json", estadoCliente);

        }

        public static EstadoCuenta hallar(int codigo)
        {
            var lista_estadoscuenta = EstadoCuenta.abrir();

            var estado = new EstadoCuenta();

<<<<<<< HEAD
            foreach (var e in lista_estadoscuenta)
            {
                if (e.codigo_cliente == codigo)
                {
                    estado = e;
                    break;
                }
            }
=======
            return lista; 
>>>>>>> 46f8903f55a89dc33a57e0419de0d49b1f80eb1e

            return estado;
        }
<<<<<<< HEAD

        public static EstadoCuenta mostrarCuenta(int codigo)
=======
   */

        /*public static void mostrar_menu_estado_de_cuenta()
        {
            // Hola vengo a flotar 4
            Console.WriteLine("******************************************************************************************");
            foreach (var a in ListaEstadoCuentaPorCliente) {System.Diagnostics.Debug.WriteLine(a.ToString()); }
            Console.WriteLine();
            Console.WriteLine("******************************************************************************************\n");
        }
        */
        private void  ValidarClaveSecreta(int clave_secreta)
>>>>>>> 46f8903f55a89dc33a57e0419de0d49b1f80eb1e
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
        foreach (var e in lista_estadoscuenta)
            {
                if (e.codigo_cliente == codigo && (Ini<=e.fecha_operacion || e.fecha_operacion<=Fin) )
                {

                    estado = e;
                    Console.WriteLine(e.ToString());
                }
                   
            } 
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
        private void  ValidarClaveSecreta(int clave_secreta)
        {
            Console.WriteLine("A implementar");
        }

<<<<<<< HEAD
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
        return  this.codigo_operacion +"  "+  this.fecha_operacion +" "+ this.concepto +" "+ this.cargos +" "+ this.abonos;
=======
        public override string ToString()
        {
        return "Cliente: " + this.codigo_cliente + " " + this.codigo_operacion +  this.fecha_operacion + this.concepto + this.cargos + this.abonos;
>>>>>>> 46f8903f55a89dc33a57e0419de0d49b1f80eb1e
        }


    }
}
