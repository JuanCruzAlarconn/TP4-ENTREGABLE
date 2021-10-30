﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace tp4
{
    class Estado
    {//implica los estados por los que pasara el paquete en su transito por donde sea asignado 
        public int codigo_entidad { get; set; }

        public string entidad { get; set; }//Puede ser una sucursal, centro provincial, centro regional, transporte de la empresa propia de cada dimesión de traslado

        public string estado { get; set; }//Espacio donde la  entidad que lo recibe coloca en que estado se hallan los bultos

      

        public static void cargar_prueba()
        {
            List<Estado> lista_prueba = new List<Estado>();

            Estado estado1 = new Estado();
            estado1.codigo_entidad = 0001;
            estado1.entidad = "transporte";
            estado1.estado = "En distribución desde centro provincial hacia sucursal";

            lista_prueba.Add(estado1);

            string estadosJson = JsonConvert.SerializeObject(lista_prueba);

            File.WriteAllText("Estados.Json", estadosJson); 
        }

        public static Estado crear()
        {
            var estado = new Estado();

            estado.codigo_entidad = asignar_codigo();
            estado.entidad = asignar_entidad();
            estado.estado = asignar_estado_inicial();

            return estado;


        }

        private static string asignar_estado_inicial()
        {
            throw new NotImplementedException();
        }

        private static string asignar_entidad()
        {
            throw new NotImplementedException();
        }

        private static int asignar_codigo()
        {
            throw new NotImplementedException();
        }
    }
}
