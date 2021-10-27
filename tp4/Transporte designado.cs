using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Transporte_designado
    {
        public int codigo_transporte { get; set; }

        public string descripción_transporte { get; set; }
        /*POSIBLES TRANSPORTES IMPLICADOS EN LA OPERATORIA, SE AGREGARA A LA LISTA DEPENDIENDO DE LAS NECESIDADES DEL ENVÍO
       * transporte_sucursal_a_provincia
         transporte_provincia_a_regional
         transporte_regional_a_provincia 
         transporte_provincia_a_sucursal
         transporte_regional_a_regional translada paquete entre regiones
        
      */

        //EL HECHO DE QUE LE ASIGNE UN PAQUETE A UN TRANSPORTE NO IMPLICA QUE ESTE TRANSPORTE DISPONGA DEL PAQUETE SINO QUE LO TIENE ASIGNADO, CUANDO TOME CONTACTO CON EL MISMO DEFINIRÁ LOS CAMBIOS DE ESTADOS CORRESPONDIENTES

        public Transporte_designado(int codigo, string descripcion)

        {
            this.codigo_transporte = codigo;
            this.descripción_transporte = descripción_transporte;

        }

        public Transporte_designado()
        {

        }




        public static Transporte_designado agregar(int codigo, string descripcion)
        {
            Transporte_designado transporte_designado = new Transporte_designado();

            transporte_designado.codigo_transporte = codigo;
            transporte_designado.descripción_transporte = descripcion;//como será el viaje encomendado

            return transporte_designado;
        }
    }
}
