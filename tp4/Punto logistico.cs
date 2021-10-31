using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Punto_logistico
    {//
        public string nombre_y_apellido { get; set; }

        public int DNI_o_Documentacion_correspondiente { get; set; }

        public string direccion { get; set; }
        public string localidad { get; set; }

        public string provincia { get; set; }

        public string region { get; set; }

        public string pais { get; set; }

        public int codigo_sucursal { get; set; }
        public int codigo_centro_provincial { get; set; }
        public int codigo_centro_regional { get; set; }

        public static Punto_logistico crear()
        {
            Punto_logistico punto_geografico = new Punto_logistico();

            punto_geografico.pais = asignar("pais");

            if (punto_geografico.pais == "Argentina")
            {

                punto_geografico.direccion = asignar("dirección");
                punto_geografico.localidad = asignar("localidad");
                punto_geografico.provincia = asignar("provincia");
                punto_geografico.region = asignar("región");
                punto_geografico.codigo_sucursal = asignar_sucursal();
                punto_geografico.codigo_centro_provincial = asignar_centro_provincial();
                punto_geografico.codigo_centro_regional = asignar_centro_regional();
            }
            else
            {
                punto_geografico.direccion = asingar_extranjero();
                punto_geografico.localidad = null;
                punto_geografico.provincia = null;
                punto_geografico.region = null;
            }

            return punto_geografico;

        }

        private static int asignar_centro_regional()
        {
            throw new NotImplementedException();
        }

        private static int asignar_centro_provincial()
        {
            throw new NotImplementedException();
        }

        private static int asignar_sucursal()
        {
            throw new NotImplementedException();
        }

        private static string asingar_extranjero()
        {
            throw new NotImplementedException();
        }

        private static string asignar(string campo)
        {
            throw new NotImplementedException();
        }

        public static string tipo_envio(Punto_logistico A, Punto_logistico B)
        {
            //Con esto puedo ver como debo de asignar los elementos de transporte y como debo de realizar los cobros
            string tipo = "";

            if (A.localidad == B.localidad)
            {
                tipo = "local";

            }

            if (A.region == B.region)
            {
                tipo = "regional";
            }

            if (A.pais == B.pais)
            {
                tipo = "internacional";
            }

            return tipo;
        }
    }
}
