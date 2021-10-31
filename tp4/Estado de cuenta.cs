using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp4
{
    public class EstadoCuenta
    {
        public int codigo_operacion { get; set; }
        public int codigo_cliente { get; set; }
        public DateTime fecha_operacion { get; set; }
        public string concepto { get; set; }
        public int cargos { get; set; }
        public int abonos { get; set; }


        public EstadoCuenta(int Codigo_operacion, int Codigo_cliente, DateTime Fecha_operacion, string Concepto, int Cargos,  int Abonos)
        {
            codigo_operacion = Codigo_operacion;
            codigo_cliente = Codigo_cliente;
            fecha_operacion = Fecha_operacion;
            concepto = Concepto;
            cargos = Cargos;
            abonos = Abonos;
        }

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



    }
}
