using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp4
{
    class ConjuntoDeSucursales
    {
        private List<Sucursal2> sucursales;

        public ConjuntoDeSucursales()
        {
            sucursales = new List<Sucursal2>();
            cargaInicial();
        }

        private void cargaInicial()
        {
            sucursales.Add(new Sucursal2(100, "Microcentro", "CABA", "Metropolitana"));
            sucursales.Add(new Sucursal2(115, "Liniers", "CABA", "Metropolitana"));
            sucursales.Add(new Sucursal2(150, "La Plata", "Buenos Aires", "Metropolitana"));
            sucursales.Add(new Sucursal2(175, "Olavarria", "Buenos Aires", "Metropolitana"));
            sucursales.Add(new Sucursal2(200, "Villa Central Norte", "Resistencia", "Norte"));
            sucursales.Add(new Sucursal2(300, "Alberdi", "Ciudad de Córdoba", "Centro"));
            sucursales.Add(new Sucursal2(311, "Arguello", "Ciudad de Córdoba", "Centro"));
            sucursales.Add(new Sucursal2(405, "Santa Clara", "Viedma", "Sur"));
        }

        /*internal Sucursal2 obtenerSucursal(int sucursal)
        {
            return (sucursales[sucursales.IndexOf(new Sucursal2(sucursal, "", "", ""))]); ;
        }*/

        public bool existe(int codigo)
        {
            return (sucursales.Contains(new Sucursal2(codigo, "", "", "")));
        }

        public string listado()
        {
            string retorno = "";

            foreach (Sucursal2 sucursal in sucursales)
            {
                retorno = retorno + sucursal.ToString() + "\n";
            }

            return (retorno);
        }

        public bool agregar(Sucursal2 sucursal)
        {
            if (!sucursales.Contains(sucursal))
            {
                sucursales.Add(sucursal);
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public Sucursal2 obtener(int codigo)
        {
            Sucursal2 retorno = null;
            Sucursal2 aBuscar = new Sucursal2(codigo, "", "", "");

            int posicion = this.sucursales.IndexOf(aBuscar);

            if (posicion != -1)
            {
                retorno = sucursales[posicion];
            }

            return (retorno);
        }
    }
}
