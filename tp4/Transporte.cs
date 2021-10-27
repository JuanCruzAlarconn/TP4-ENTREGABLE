using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class Transporte
    {
        public int codigo { get; set; }
        public string categoria { get; set; }
        public string estado { get; set; } //habla del estado de la unidad si puede seguir prestando servicio o debe de remitirse a mentenimiento
        public List<int> codigos_ordenes_asignadas { get; set; }//A pertir del código pueden consultar y modificar los estados de las ordenes de servicio       
        public string cobertura { get; set; }//implica que puede ser entre localidades, cubrir viajes entre provincias o regionales
        public string zona_cobertura { get; set; }//sobre que localidadesm, provincias o regiones puede circular, con ello puedo asignar transporte dependiendo de las necesidades  de transporte del servicio como tal      


        public void actualizar_estado()
        {
            throw new NotImplementedException();
        }

        public void asignar_orden_de_servicio(int codigo_servicio, string condicion_servicio)
        {
            //hay que pasar ciertos parametros de la orden de servicio iniciada, para que se pueda tomar la desición de poder designar cierto transporte de los disponibles para la tarea de transporte
            //Una vez fianlizada la operación se debe de colocar la designación dentro de un objeto transporte de la misma forma que la orden de servicio contara con el código de transporte designado para poder llevar adelante el envío correpondiente
            //Con la condición de servicio le estoy indicando desde donde hasta donde debo de dirigirme
            throw new NotImplementedException();
        }


        public static int? ver_disponibilidad()
        {
            //al pasar una cantida de parametros se evalúa pasando por la lista de transporte si puedo contar con alguno de los disponibles para poder designar en uno de los viajes que se platearon en las iteraciones del viaje
            throw new NotImplementedException();
        }

        private bool soportar_peso(Paquete paquete)
        {
            //A partie de las caracteristicas del mismo objeto que la invoca debe de cotejar si resiste la carga de un paquete de las propiedades del objeto paquete
            throw new NotImplementedException();
        }

        public static List<Transporte> abrir_archivo()
        {
            string transportesJson = File.ReadAllText("Transportes.Json");

            List<Transporte> lista = JsonConvert.DeserializeObject<List<Transporte>>(transportesJson);

            return lista;

        }

        public static Transporte hallar(int codigo)
        {
            //Paso el código de transporte y me devulve el objeto transporte con el que puedo trabajar
            var lista = Transporte.abrir_archivo();
            var transporte = new Transporte();//envío una copia de la información

            foreach (var t in lista)
            {
                if (t.codigo == codigo)
                {
                    transporte = t;
                    break;
                }
            }

            return transporte;
        }

        public void paquete_a_bordo()
        {
            //Cambia el estado del paquete afirmando que lo tiene en viaje a una fecha
            throw new NotImplementedException();
        }

        public void fin_viaje()
        {
            //Cambia el estado colocando que el viaje designado termino en una cierta fecha
            throw new NotImplementedException();
        }

        public bool validar_cobertura(string zona)
        {
            throw new NotImplementedException();
        }


        public bool validar_carga(Paquete paquete)
        {
            throw new NotImplementedException();
        }

        public static Transporte asignar_transporte()
        {
            //Le paso el codigo de la orden de servicio a la entidad para que en el momento en que tome contacto físico con la misma pueda modificar el estado partiendo de la base de datos unificada
            throw new NotImplementedException();
        }
        public void actuliazar_archivo()
        {
            //paso final luego de haber incorporado el nuevo elemento dentro de la lista
            throw new NotImplementedException();
        }
    }
}
