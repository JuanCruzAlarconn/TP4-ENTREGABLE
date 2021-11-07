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
        }

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
