using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class AppCrearOrdenes
    {
        private Validador validador;
        private ConjuntoDeOrdenes ordenes;
        private ConjuntoDeSucursales sucursales;

        public AppCrearOrdenes()
        {
            validador = new Validador();
            ordenes = new ConjuntoDeOrdenes();
            sucursales = new ConjuntoDeSucursales();
        }

        public void ejecutar()
        {
            int sucursal = 0;
            string continuar = "";
            decimal costo = 0; //quitar luego de desarrollar el metodo para calcular el costo

            Console.WriteLine("Completa los datos de tu envío" + "\n");
            string tipo = validador.pedirTipo("¿Qué tipo de paquete quieres enviar? (nacional/internacional)\n");
            int postal = validador.pedirInteger("Ingrese el código postal del destino\n", 0, 999999);

            do
            {
                Console.WriteLine("Listado de sucursales" + "\n");
                string mensajeAMostrar = sucursales.listado();
                Console.WriteLine(mensajeAMostrar);
                sucursal = validador.pedirInteger("Ingrese la sucursal de origen\n", 0, 999);

                if (!sucursales.existe(sucursal))
                {
                    Console.WriteLine("El codigo ingresado no se corresponde con ninguna de las sucursales que se encuentran dentro de la base de datos,\n por favor ingrese una sucursal nuevamente\n");
                }
                else
                {
                    Paquete paquete = Paquete.crear();

                    string tipo2 = validador.pedirTipo2("Elija el tipo de entrega: estandar/urgente (sin acentos)\n");
                    string nombre = validador.pedirChar("Ingrese el nombre del destinatario\n", 30);
                    string direccion = validador.pedirString("Ingrese la dirección del destinatario\n");

                    //decimal costo = calcularCosto(tipo, postal, paquete.peso, tipo2);
                    //Console.WriteLine("El costo del servicio es:" + costo);

                    continuar = validador.pedirSoN("Desea confirmar la orden? S/N\n");

                    if (continuar == "S")
                    {
                        int codigo = ordenes.asignar_codigo_servicio();
                        ordenes.agregar(new Orden_de_servicio2(codigo, tipo, postal, sucursal, tipo2, nombre, direccion, costo));
                        Console.WriteLine("Orden de servicio generada Nro:" + codigo);
                    }
                }
            } while (!sucursales.existe(sucursal));
        }

        
    }

    
}
