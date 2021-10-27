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


        public static List<Cliente_corportativo> abrir()
        {
            var lista_cliente = new List<Cliente_corportativo>();

            var listaclinteJson = File.ReadAllText("clientes corporativoc.Json");

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
            var cliente = Cliente_corportativo.hallar(codigo);

            if (cliente == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
