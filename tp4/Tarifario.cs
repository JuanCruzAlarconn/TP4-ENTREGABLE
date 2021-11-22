using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Tarifario
    {
        public string bulto { get; set; }
        public double local { get; set; }
        public double provincial { get; set; }
        public double regional { get; set; }
        public double nacional { get; set; }
        public double urgente { get; set; }
        public double enPuerta { get; set; }
        public double limitrofes { get; set; }
        public double restoALatina { get; set; }
        public double aNorte { get; set; }
        public double europa { get; set; }
        public double asia { get; set; }
        public List<Tarifario> ListaPrecios { get; set; }
       
        /*public static void carga_prueba_tarifario()
        {
            var a = new Tarifario();
            a.bulto = "500g";
            a.local = 250;
            a.provincial = 500;
            a.regional = 1000;
            a.nacional = 2000;
            a.urgente = 1.10;
            a.enPuerta = 20;
            a.limitrofes = 1000;
            a.restoALatina = 2000;
            a.aNorte = 3000;
            a.europa = 4000;
            a.asia = 5000;
           
            var b = new Tarifario();
            b.bulto = "10kg";
            b.local = 1000;
            b.provincial = 2000;
            b.regional = 3000;
            b.nacional = 4000;
            b.urgente = 1.10;
            b.enPuerta = 20;
            b.limitrofes = 1000;
            b.restoALatina = 2000;
            b.aNorte = 3000;
            b.europa = 4000;
            b.asia = 5000;

            var c = new Tarifario();
            c.bulto = "20kg";
            c.local = 2000;
            c.provincial = 4000;
            c.regional = 6000;
            c.nacional = 8000;
            c.urgente = 1.10;
            c.enPuerta = 20;
            c.limitrofes = 1000;
            c.restoALatina = 2000;
            c.aNorte = 3000;
            c.europa = 4000;
            c.asia = 5000;

            var d = new Tarifario();
            d.bulto = "30kg";
            d.local = 4000;
            d.provincial = 6000;
            d.regional = 8000;
            d.nacional = 10000;
            d.urgente = 1.10;
            d.enPuerta = 20;
            d.limitrofes = 1000;
            d.restoALatina = 2000;
            d.aNorte = 3000;
            d.europa = 4000;
            d.asia = 5000;

            List<Tarifario> lista = new List<Tarifario>();

            lista.Add(a);
            lista.Add(b);
            lista.Add(c);
            lista.Add(d);

            string precios = JsonConvert.SerializeObject(lista);

            File.WriteAllText("Tarifario.json", precios);
        }*/

        public static Tarifario hallar(string bulto)
        {
            var lista = Tarifario.abrir_archivo();

            var precios = new Tarifario();

            foreach (var tarifario in lista)
            {
                if (tarifario.bulto == bulto)
                {
                    precios = tarifario;
                    break;
                }
            }

            return precios;
        }

        public static List<Tarifario> abrir_archivo()
        {

            var lista = new List<Tarifario>();
            string tarifarioJson = File.ReadAllText("Tarifario.Json");

            lista = JsonConvert.DeserializeObject<List<Tarifario>>(tarifarioJson);

            return lista;
        }
    }
}
