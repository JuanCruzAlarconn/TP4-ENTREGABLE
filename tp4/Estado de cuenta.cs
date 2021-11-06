﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            a.codigo_cliente = 1;
            a.fecha_operacion = DateTime.Now;
            a.concepto = "calle falsa 123";
            a.cargos = 10000;
            a.abonos = 10000;

            List<EstadoCuenta> lista = new List<EstadoCuenta>();
            lista.Add(a);
           


        }

   /*     public static List<EstadoCuenta> abrir_archivo()
        {
            string estadoCuentaJson = File.ReadAllText("estadoCuenta.Json");

            List<EstadoCuenta> lista = JsonConvert.DeserializeObject<List<EstadoCuenta>>(estadoCuentaJson);

            return lista;

        }
   */

        /*public static void mostrar_menu_estado_de_cuenta()
        {
            Console.WriteLine("******************************************************************************************");
            foreach (var a in ListaEstadoCuentaPorCliente) {System.Diagnostics.Debug.WriteLine(a.ToString()); }
            Console.WriteLine();
            Console.WriteLine("******************************************************************************************\n");
        }
        */
        private void  ValidarClaveSecreta(int clave_secreta)
        {
            Console.WriteLine("A implementar");
        }
        private void CalcularSaldoCuenta()
        {
            Console.WriteLine("A implementar");
        }
        private void FiltrarEstadoCuenta()
        {
            Console.WriteLine("A implementar");
        }
        private void HallarEstadoCuenta()
        {
            Console.WriteLine("A implementar");
        }

        public override string ToString()
        {
        return "Cliente: " + this.codigo_cliente + " " + this.codigo_operacion +  this.fecha_operacion + this.concepto + this.cargos + this.abonos;
        }


    }
}
