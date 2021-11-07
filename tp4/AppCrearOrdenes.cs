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
            
            Console.WriteLine("Completa los datos de tu envío" + "\n");
            string tipo = validador.pedirTipo("¿Qué tipo de paquete quieres enviar? (Nacional/Internacional)\n");
            int postal = validador.pedirInteger("Ingrese el código postal del destino\n", 0, 999999);

            do
            {
                sucursal = validador.pedirInteger("Ingrese la sucursal de origen\n", 0, 999);
                if (!sucursales.existe(sucursal))
                {
                    Console.WriteLine("El codigo ingresado no se corresponde con ninguna de las sucursales que se encuentran dentro de la base de datos, por favor ingrese una sucursal nuevamente");
                }
                else
                {

                }
            } while (!sucursales.existe(sucursal));
        }

        private void cargaInicial()
        {
            sucursales.agregar(new Sucursal2(100, "Microcentro", "CABA", "Metropolitana"));
            sucursales.agregar(new Sucursal2(115, "Liniers", "CABA", "Metropolitana"));
            sucursales.agregar(new Sucursal2(150, "La Plata", "Buenos Aires", "Metropolitana"));
            sucursales.agregar(new Sucursal2(175, "Olavarria", "Buenos Aires", "Metropolitana");
            sucursales.agregar(new Sucursal2(200, "Villa Central Norte", "Resistencia", "Norte"));
            sucursales.agregar(new Sucursal2(300, "Alberdi", "Ciudad de Córdoba", "Centro"));
            sucursales.agregar(new Sucursal2(311, "Arguello", "Ciudad de Córdoba", "Centro"));
            sucursales.agregar(new Sucursal2(405, "Santa Clara", "Viedma", "Sur"));
        }
    }

    
}
