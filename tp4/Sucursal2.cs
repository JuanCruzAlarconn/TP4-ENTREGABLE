using System;
using System.Collections.Generic;
using System.Text;

namespace tp4
{
    class Sucursal2
    {
        private int codigo;
        private string barrio;
        private string provincia;
        private string region;

        public Sucursal2(int codigo, string barrio, string provincia, string region)
        {
            this.codigo = codigo;
            this.barrio = barrio;
            this.provincia = provincia;
            this.region = region;
        }

        public override bool Equals(object obj)
        {
            var sucursal = obj as Sucursal2;
            return sucursal != null &&
                   codigo == sucursal.codigo;
        }

        public override string ToString()
        {
            return codigo + " - " + barrio + " - " + provincia + " - " + region;
        }
    }
}