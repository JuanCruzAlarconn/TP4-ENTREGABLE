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
    {
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
                punto_geografico.nombre_y_apellido = asignar("nombre y apellido");
                punto_geografico.DNI_o_Documentacion_correspondiente = asignar("Documentación");
                punto_geografico.direccion = asignar("dirección");
                punto_geografico.localidad = asignar("localidad");
                punto_geografico.provincia = asignar("provincia");
                punto_geografico.region = asignar("región");
                punto_geografico.codigo_sucursal = asignar_sucursal(punto_geografico.localidad);
                punto_geografico.codigo_centro_provincial = asignar_centro_provincial(punto_geografico.provincia);
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
            //SE PUEDE GENERAR UN ARCHIVO PARA PODER RASTREAR A QUE REGIÓN SE CORREPSONDE UNA DETERMINADA PROVINCIA
        }

        private static int asignar_centro_provincial(string provincia)
        {
            var lista = Centro_Provincial.abrir_archivo();
            int codigo_provincia = 0;

            foreach(var prov in lista)
            {
                if(prov.ubicacion==provincia)
                {
                    codigo_provincia = prov.codigo;
                }

            }

            return codigo_provincia;
        }

        private static int asignar_sucursal(string localidad)
        {
            var lista = Sucursal.abrir_archivo();
            int codigo_sucursal = 0;

            foreach(var sucursal in lista)
            {
                if(sucursal.localidad_dominante=="localidad")
                {
                    codigo_sucursal = sucursal.codigo_sucursal;
                }
            }

            return codigo_sucursal;
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
