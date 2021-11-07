﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class ConjuntoDeOrdenes
    {
        private List<Orden_de_servicio2> ordenes;

        public ConjuntoDeOrdenes()
        {
            ordenes = new List<Orden_de_servicio2>();
        }

        public string listado()
        {
            string retorno = "";

            foreach (Orden_de_servicio2 orden in ordenes)
            {
                retorno = retorno + orden.ToString() + "\n";
            }

            return (retorno);
        }

        public bool agregar(Orden_de_servicio2 orden)
        {
            if (!ordenes.Contains(orden))
            {
                ordenes.Add(orden);
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public Orden_de_servicio2 obtener(int codigo, string tipo)
        {
            Orden_de_servicio2 retorno = null;
            Orden_de_servicio2 aBuscar = new Orden_de_servicio2(codigo, "", 0, 0, "", "", "", 0);

            int posicion = this.ordenes.IndexOf(aBuscar);

            if (posicion != -1)
            {
                retorno = ordenes[posicion];
            }

            return (retorno);
        }

        public int asignar_codigo_servicio()
        {
            Random r = new Random();

            int servicio = r.Next(0, 9999999);

            return servicio;
        }
    }
}
