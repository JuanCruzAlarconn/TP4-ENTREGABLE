using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace tp4
{
    class Cliente_corportativo
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int codigo_cliente { get; set; }
        public string domicilio { get; set; }
        public int telefono { get; set; }
        public int codigo_cuenta_corriente { get; set; }
        public int clave_secreta { get; set; }
        public int codigo_postal { get; set; }

        public static void carga_prueba()
        {
            //Un método que permite cargar un cliente en json, para poder realizar un prueba de carga por consola
            var cliente_corporativo = new Cliente_corportativo();
            cliente_corporativo.nombre = "Juan Cruz";
            cliente_corporativo.apellido = "Alarcón";
            cliente_corporativo.codigo_cliente = 38456910;
            cliente_corporativo.domicilio = "calle falsa 123";
            cliente_corporativo.telefono = 1145788956;
            cliente_corporativo.codigo_cuenta_corriente = 0001;
            cliente_corporativo.clave_secreta = 1234;
            cliente_corporativo.codigo_postal = 1439;

            var cliente_corporativo2 = new Cliente_corportativo();
            cliente_corporativo2.nombre = "Roy";
            cliente_corporativo2.apellido = "Russo";
            cliente_corporativo2.codigo_cliente = 12345678;
            cliente_corporativo2.domicilio = "calle turbia 123";
            cliente_corporativo2.telefono = 1145237789;
            cliente_corporativo2.codigo_cuenta_corriente = 0002;
            cliente_corporativo2.clave_secreta = 1234;
            cliente_corporativo2.codigo_postal = 1414;

            List<Cliente_corportativo> lista = new List<Cliente_corportativo>();
            lista.Add(cliente_corporativo);
            lista.Add(cliente_corporativo2);
            string clienteJson = JsonConvert.SerializeObject(lista);

            File.WriteAllText("clientes.json", clienteJson);

           


        }

        public static List<Cliente_corportativo> abrir()
        {
            var lista_cliente = new List<Cliente_corportativo>();

            var listaclinteJson = File.ReadAllText("clientes.json");

            lista_cliente = JsonConvert.DeserializeObject<List<Cliente_corportativo>>(listaclinteJson);

            return lista_cliente;
        }

        public static Cliente_corportativo hallar(int codigo)
        {
            var lista_cliente = Cliente_corportativo.abrir();

            var cliente = new Cliente_corportativo();

            foreach (var c in lista_cliente)
            {
                if (c.codigo_cliente == codigo)
                {
                    cliente = c;
                    break;
                }
            }

            return cliente;
        }



        public static bool validar_cliente(int codigo)
        {
         



            return Cliente_corportativo.abrir().Any(a=>a.codigo_cliente==codigo);


        }
    }
}
