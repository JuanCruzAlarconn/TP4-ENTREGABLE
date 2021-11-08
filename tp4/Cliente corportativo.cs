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
        public string nombreyapellido { get; set; }
        public int codigo_cliente { get; set; }

        public int codigo_cuenta_corriente { get; set; }
        public int clave_secreta { get; set; }
        public Cliente_corportativo()
            {}

        public Cliente_corportativo(string nombre,int cc, int ccc)
        
        {
            this.nombreyapellido = nombre;
            this.codigo_cliente = cc;
            this.codigo_cuenta_corriente = ccc;
            this.clave_secreta = 0000;

        }

        public static void carga_prueba()
        {
            //Un método que permite cargar un cliente en json, para poder realizar un prueba de carga por consola
            var cliente_corporativo = new Cliente_corportativo();
            cliente_corporativo.nombreyapellido = "Juan Cruz Alarcón";         
            cliente_corporativo.codigo_cliente = 38456910;            
            cliente_corporativo.codigo_cuenta_corriente = 0001;
            cliente_corporativo.clave_secreta = 1234;
           

            var cliente_corporativo2 = new Cliente_corportativo();
            cliente_corporativo2.nombreyapellido = "Roy Russo";           
            cliente_corporativo2.codigo_cliente = 12345678;
            cliente_corporativo2.codigo_cuenta_corriente = 0002;
            cliente_corporativo2.clave_secreta = 1234;


            var c3 = new Cliente_corportativo("Julian Alvarez", 00000001, 0003);
            var c4 = new Cliente_corportativo("Enzo perez", 00000002, 0004);
            var c5 = new Cliente_corportativo("Franco Armani", 00000003, 0005);
            var c6 = new Cliente_corportativo("Roberto Rojas", 00000004, 0006);
            var c7 = new Cliente_corportativo("Brian Romero", 00000005, 0007);
            var c8 = new Cliente_corportativo("Jorge Carrascal", 00000006, 0008);
            var c9 = new Cliente_corportativo("Paulo Días", 00000007, 0009);
            var c10 = new Cliente_corportativo("Santiago Simon", 00000008, 0010);

            List<Cliente_corportativo> lista = new List<Cliente_corportativo>();
            lista.Add(cliente_corporativo);
            lista.Add(cliente_corporativo2);
            lista.Add(c9);
            lista.Add(c10);
            lista.Add(c3);
            lista.Add(c4);
            lista.Add(c5);
            lista.Add(c6);
            lista.Add(c7);
            lista.Add(c8);
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
