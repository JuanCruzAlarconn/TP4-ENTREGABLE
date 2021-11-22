using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace tp4
{
    class AppCrearOrdenes
    {
        private Validador validador;
        
        public AppCrearOrdenes()
        {
            validador = new Validador();
        }
        
        public void ejecutar(int codigo_cliente)
        {
            int cliente = codigo_cliente;
            string continuar = "";
            double costo = 0;

            Console.WriteLine("Completa los datos de tu envío" + "\n");
           
            Modalidad modalidad = Modalidad.crear();
            Punto_logistico origen = Punto_logistico.crear("origen",modalidad.modo_retiro);
            Punto_logistico destino = Punto_logistico.crear("destino",modalidad.modo_entrega);
           
            Paquete paquete = Paquete.crear();

            costo = calcularCosto(origen, destino, modalidad, paquete.peso);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nEl costo del servicio es:" + costo + "\n".ToUpper());
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Green;
            continuar = validador.pedirSoN("Desea confirmar la orden? S/N\n Presione ENTER A CONTINUACIÓN");
            Console.ForegroundColor = ConsoleColor.White;
            if (continuar == "S")
            {
                int codigo = Orden_de_servicio.asignar_codigo_servicio();//Asigna un código de orden de servicio
                List<Estado> estado = Orden_de_servicio.asignar_estado_inicial();//Genera la lista y le coloca el 1º estado
                var fecha = DateTime.Now;
                int codigo_seguro = Orden_de_servicio.asignar_seguro();//Aleatoria
                var orden=new Orden_de_servicio(codigo, codigo_seguro,costo,codigo_cliente,estado,fecha,paquete,origen, destino, modalidad);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Orden de servicio generada Nro:".ToUpper() + codigo);
                Console.ForegroundColor = ConsoleColor.White;
                Orden_de_servicio.grabar(orden);
                Orden_de_servicio.enviar_comunicado(orden, fecha);
            }
        }

        private double calcularCosto(Punto_logistico origen, Punto_logistico destino, Modalidad modalidad, decimal peso)
        {
            double costo = 0;
            double costoInternacional = 0;
            //double urgente = 1.10;
            //double enPuerta = 20;

            //Tarifario.carga_prueba_tarifario();
            var precio = Tarifario.hallar("500g");
            var precio2 = Tarifario.hallar("10kg");
            var precio3 = Tarifario.hallar("20kg");
            var precio4 = Tarifario.hallar("30kg");

            if (destino.continente_asignado == "America del sur limitrofe")
            {
                costoInternacional = precio.limitrofes;
            }
            else
            {
                if (destino.continente_asignado == "America del sur")
                {
                    costoInternacional = precio.restoALatina;
                }
                else
                {
                    if (destino.continente_asignado == "America del norte")
                    {
                        costoInternacional = precio.aNorte;
                    }
                    else
                    {
                        if (destino.continente_asignado == "Europa")
                        {
                            costoInternacional = precio.europa;
                        }
                        else
                        {
                            if (destino.continente_asignado == "Asia")
                            {
                                costoInternacional = precio.asia;
                            }
                            else
                            {
                                costoInternacional = 5000;
                            }
                        }
                    }
                }
            }

            if (destino.pais != "Argentina") 
            {
                if (destino.localidad != origen.localidad)
                {
                    if (destino.provincia != origen.provincia)
                    {
                        if (destino.region != origen.region)
                        {
                            if (peso > 20000)
                            {
                                if (modalidad.tipo_envio == "Urgente")
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.nacional * precio4.urgente + (precio4.enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio4.nacional * precio4.urgente + precio4.enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.nacional * precio4.urgente + precio4.enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio4.nacional * precio4.urgente + costoInternacional;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.nacional + (precio4.enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio4.nacional + precio4.enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.nacional + precio4.enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio4.nacional + costoInternacional;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                if (peso > 10000)
                                {
                                    if (modalidad.tipo_envio == "Urgente")
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.nacional * precio3.urgente + (precio3.enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio3.nacional * precio3.urgente + precio3.enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.nacional * precio3.urgente + precio3.enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio3.nacional * precio3.urgente + costoInternacional;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.nacional + (precio3.enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio3.nacional + precio3.enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.nacional + precio3.enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio3.nacional + costoInternacional;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (peso > 500)
                                    {
                                        if (modalidad.tipo_envio == "Urgente")
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.nacional * precio2.urgente + (precio2.enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio2.nacional * precio2.urgente + precio2.enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.nacional * precio2.urgente + precio2.enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio2.nacional * precio2.urgente + costoInternacional;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.nacional + (precio2.enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio2.nacional + precio2.enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.nacional + precio2.enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio2.nacional + costoInternacional;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.tipo_envio == "Urgente")
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.nacional * precio.urgente + (precio.enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio.nacional * precio.urgente + precio.enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.nacional * precio.urgente + precio.enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio.nacional * precio.urgente + costoInternacional;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.nacional + (precio.enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio.nacional + precio.enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.nacional + precio.enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio.nacional + costoInternacional;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (peso > 20000)
                            {
                                if (modalidad.tipo_envio == "Urgente")
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.regional * precio4.urgente + (precio4.enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio4.regional * precio4.urgente + precio4.enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.regional * precio4.urgente + precio4.enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio4.regional * precio4.urgente + costoInternacional;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.regional + (precio4.enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio4.regional + precio4.enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.regional + precio4.enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio4.regional + costoInternacional;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                if (peso > 10000)
                                {
                                    if (modalidad.tipo_envio == "Urgente")
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.regional * precio3.urgente + (precio3.enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio3.regional * precio3.urgente + precio3.enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.regional * precio3.urgente + precio3.enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio3.regional * precio3.urgente + costoInternacional;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.regional + (precio3.enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio3.regional + precio3.enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.regional + precio3.enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio3.regional + costoInternacional;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (peso > 500)
                                    {
                                        if (modalidad.tipo_envio == "Urgente")
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.regional * precio2.urgente + (precio2.enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio2.regional * precio2.urgente + precio2.enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.regional * precio2.urgente + precio2.enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio2.regional * precio2.urgente + costoInternacional;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.regional + (precio2.enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio2.regional + precio2.enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.regional + precio2.enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio2.regional + costoInternacional;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.tipo_envio == "Urgente")
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.regional * precio.urgente + (precio.enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio.regional * precio.urgente + precio.enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.regional * precio.urgente + precio.enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio.regional * precio.urgente + costoInternacional;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.regional + (precio.enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio.regional + precio.enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.regional + precio.enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = precio.regional + costoInternacional;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (peso > 20000)
                        {
                            if (modalidad.tipo_envio == "Urgente")
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio4.provincial * precio4.urgente + (precio4.enPuerta * 2) + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = precio4.provincial * precio4.urgente + precio4.enPuerta + costoInternacional;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio4.provincial * precio4.urgente + precio4.enPuerta + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = precio4.provincial * precio4.urgente + costoInternacional;
                                    }
                                }
                            }
                            else
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio4.provincial + (precio4.enPuerta * 2) + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = precio4.provincial + precio4.enPuerta + costoInternacional;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio4.provincial + precio4.enPuerta + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = precio4.provincial + costoInternacional;
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (peso > 10000)
                            {
                                if (modalidad.tipo_envio == "Urgente")
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio3.provincial * precio3.urgente + (precio3.enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio3.provincial * precio3.urgente + precio3.enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio3.provincial * precio3.urgente + precio3.enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio3.provincial * precio3.urgente + costoInternacional;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio3.provincial + (precio3.enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio3.provincial + precio3.enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio3.provincial + precio3.enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio3.provincial + costoInternacional;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (peso > 500)
                                {
                                    if (modalidad.tipo_envio == "Urgente")
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio2.provincial * precio2.urgente + (precio2.enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio2.provincial * precio2.urgente + precio2.enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio2.provincial * precio2.urgente + precio2.enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio2.provincial * precio2.urgente + costoInternacional;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio2.provincial + (precio2.enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio2.provincial + precio2.enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio2.provincial + precio2.enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio2.provincial + costoInternacional;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.tipo_envio == "Urgente")
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio.provincial * precio.urgente + (precio.enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio.provincial * precio.urgente + precio.enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio.provincial * precio.urgente + precio.enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio.provincial * precio.urgente + costoInternacional;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio.provincial + (precio.enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio.provincial + precio.enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio.provincial + precio.enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = precio.provincial + costoInternacional;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (peso > 20000)
                    {
                        if (modalidad.tipo_envio == "Urgente")
                        {
                            if (modalidad.modo_entrega == "Entregado en domicilio")
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = precio4.local * precio4.urgente + (precio4.enPuerta * 2) + costoInternacional;
                                }
                                else
                                {
                                    costo = precio4.local * precio4.urgente + precio4.enPuerta + costoInternacional;
                                }
                            }
                            else
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = precio4.local * precio4.urgente + precio4.enPuerta + costoInternacional;
                                }
                                else
                                {
                                    costo = precio4.local * precio4.urgente + costoInternacional;
                                }
                            }
                        }
                        else
                        {
                            if (modalidad.modo_entrega == "Entregado en domicilio")
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = precio4.local + (precio4.enPuerta * 2) + costoInternacional;
                                }
                                else
                                {
                                    costo = precio4.local + precio4.enPuerta + costoInternacional;
                                }
                            }
                            else
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = precio4.local + precio4.enPuerta + costoInternacional;
                                }
                                else
                                {
                                    costo = precio4.local + costoInternacional;
                                }
                            }
                        }

                    }
                    else
                    {
                        if (peso > 10000)
                        {
                            if (modalidad.tipo_envio == "Urgente")
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio3.local * precio3.urgente + (precio3.enPuerta * 2) + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = precio3.local * precio3.urgente + precio3.enPuerta + costoInternacional;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio3.local * precio3.urgente + precio3.enPuerta + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = precio3.local * precio3.urgente + costoInternacional;
                                    }
                                }
                            }
                            else
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio3.local + (precio3.enPuerta * 2) + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = precio3.local + precio3.enPuerta + costoInternacional;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio3.local + precio3.enPuerta + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = precio3.local + costoInternacional;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (peso > 500)
                            {
                                if (modalidad.tipo_envio == "Urgente")
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio2.local * precio2.urgente + (precio2.enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio2.local * precio2.urgente + precio2.enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio2.local * precio2.urgente + precio2.enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio2.local * precio2.urgente + costoInternacional;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio2.local + (precio2.enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio2.local + precio2.enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio2.local + precio2.enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio2.local + costoInternacional;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (modalidad.tipo_envio == "Urgente")
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio.local * precio.urgente + (precio.enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio.local * precio.urgente + precio.enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio.local * precio.urgente + precio.enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio.local * precio.urgente + costoInternacional;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio.local + (precio.enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio.local + precio.enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio.local + precio.enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = precio.local + costoInternacional;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (destino.localidad != origen.localidad)
                {
                    if (destino.provincia != origen.provincia)
                    {
                        if (destino.region != origen.region)
                        {
                            if (peso > 20000)
                            {
                                if (modalidad.tipo_envio == "Urgente")
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.nacional * precio4.urgente + (precio4.enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = precio4.nacional * precio4.urgente + precio4.enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.nacional * precio4.urgente + precio4.enPuerta;
                                        }
                                        else
                                        {
                                            costo = precio4.nacional * precio4.urgente;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.nacional + (precio4.enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = precio4.nacional + precio4.enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.nacional + precio4.enPuerta;
                                        }
                                        else
                                        {
                                            costo = precio4.nacional;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                if (peso > 10000)
                                {
                                    if (modalidad.tipo_envio == "Urgente")
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.nacional * precio3.urgente + (precio3.enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = precio3.nacional * precio3.urgente + precio3.enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.nacional * precio3.urgente + precio3.enPuerta;
                                            }
                                            else
                                            {
                                                costo = precio3.nacional * precio3.urgente;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.nacional + (precio3.enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = precio3.nacional + precio3.enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.nacional + precio3.enPuerta;
                                            }
                                            else
                                            {
                                                costo = precio3.nacional;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (peso > 500)
                                    {
                                        if (modalidad.tipo_envio == "Urgente")
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.nacional * precio2.urgente + (precio2.enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = precio2.nacional * precio2.urgente + precio2.enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.nacional * precio2.urgente + precio2.enPuerta;
                                                }
                                                else
                                                {
                                                    costo = precio2.nacional * precio2.urgente;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.nacional + (precio2.enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = precio2.nacional + precio2.enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.nacional + precio2.enPuerta;
                                                }
                                                else
                                                {
                                                    costo = precio2.nacional;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.tipo_envio == "Urgente")
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.nacional * precio.urgente + (precio.enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = precio.nacional * precio.urgente + precio.enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.nacional * precio.urgente + precio.enPuerta;
                                                }
                                                else
                                                {
                                                    costo = precio.nacional * precio.urgente;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.nacional + (precio.enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = precio.nacional + precio.enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.nacional + precio.enPuerta;
                                                }
                                                else
                                                {
                                                    costo = precio.nacional;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (peso > 20000)
                            {
                                if (modalidad.tipo_envio == "Urgente")
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.regional * precio4.urgente + (precio4.enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = precio4.regional * precio4.urgente + precio4.enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.regional * precio4.urgente + precio4.enPuerta;
                                        }
                                        else
                                        {
                                            costo = precio4.regional * precio4.urgente;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.regional + (precio4.enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = precio4.regional + precio4.enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio4.regional + precio4.enPuerta;
                                        }
                                        else
                                        {
                                            costo = precio4.regional;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                if (peso > 10000)
                                {
                                    if (modalidad.tipo_envio == "Urgente")
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.regional * precio3.urgente + (precio3.enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = precio3.regional * precio3.urgente + precio3.enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.regional * precio3.urgente + precio3.enPuerta;
                                            }
                                            else
                                            {
                                                costo = precio3.regional * precio3.urgente;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.regional + (precio3.enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = precio3.regional + precio3.enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio3.regional + precio3.enPuerta;
                                            }
                                            else
                                            {
                                                costo = precio3.regional;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (peso > 500)
                                    {
                                        if (modalidad.tipo_envio == "Urgente")
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.regional * precio2.urgente + (precio2.enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = precio2.regional * precio2.urgente + precio2.enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.regional * precio2.urgente + precio2.enPuerta;
                                                }
                                                else
                                                {
                                                    costo = precio2.regional * precio2.urgente;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.regional + (precio2.enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = precio2.regional + precio2.enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio2.regional + precio2.enPuerta;
                                                }
                                                else
                                                {
                                                    costo = precio2.regional;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.tipo_envio == "Urgente")
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.regional * precio.urgente + (precio.enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = precio.regional * precio.urgente + precio.enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.regional * precio.urgente + precio.enPuerta;
                                                }
                                                else
                                                {
                                                    costo = precio.regional * precio.urgente;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.regional + (precio.enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = precio.regional + precio.enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = precio.regional + precio.enPuerta;
                                                }
                                                else
                                                {
                                                    costo = precio.regional;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (peso > 20000)
                        {
                            if (modalidad.tipo_envio == "Urgente")
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio4.provincial * precio4.urgente + (precio4.enPuerta * 2);
                                    }
                                    else
                                    {
                                        costo = precio4.provincial * precio4.urgente + precio4.enPuerta;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio4.provincial * precio4.urgente + precio4.enPuerta;
                                    }
                                    else
                                    {
                                        costo = precio4.provincial * precio4.urgente;
                                    }
                                }
                            }
                            else
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio4.provincial + (precio4.enPuerta * 2);
                                    }
                                    else
                                    {
                                        costo = precio4.provincial + precio4.enPuerta;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio4.provincial + precio4.enPuerta;
                                    }
                                    else
                                    {
                                        costo = precio4.provincial;
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (peso > 10000)
                            {
                                if (modalidad.tipo_envio == "Urgente")
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio3.provincial * precio3.urgente + (precio3.enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = precio3.provincial * precio3.urgente + precio3.enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio3.provincial * precio3.urgente + precio3.enPuerta;
                                        }
                                        else
                                        {
                                            costo = precio3.provincial * precio3.urgente;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio3.provincial + (precio3.enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = precio3.provincial + precio3.enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio3.provincial + precio3.enPuerta;
                                        }
                                        else
                                        {
                                            costo = precio3.provincial;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (peso > 500)
                                {
                                    if (modalidad.tipo_envio == "Urgente")
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio2.provincial * precio2.urgente + (precio2.enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = precio2.provincial * precio2.urgente + precio2.enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio2.provincial * precio2.urgente + precio2.enPuerta;
                                            }
                                            else
                                            {
                                                costo = precio2.provincial * precio2.urgente;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio2.provincial + (precio2.enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = precio2.provincial + precio2.enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio2.provincial + precio2.enPuerta;
                                            }
                                            else
                                            {
                                                costo = precio2.provincial;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.tipo_envio == "Urgente")
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio.provincial * precio.urgente + (precio.enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = precio.provincial * precio.urgente + precio.enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio.provincial * precio.urgente + precio.enPuerta;
                                            }
                                            else
                                            {
                                                costo = precio.provincial * precio.urgente;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio.provincial + (precio.enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = precio.provincial + precio.enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = precio.provincial + precio.enPuerta;
                                            }
                                            else
                                            {
                                                costo = precio.provincial;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (peso > 20000)
                    {
                        if (modalidad.tipo_envio == "Urgente")
                        {
                            if (modalidad.modo_entrega == "Entregado en domicilio")
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = precio4.local * precio4.urgente + (precio4.enPuerta * 2);
                                }
                                else
                                {
                                    costo = precio4.local * precio4.urgente + precio4.enPuerta;
                                }
                            }
                            else
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = precio4.local * precio4.urgente + precio4.enPuerta;
                                }
                                else
                                {
                                    costo = precio4.local * precio4.urgente;
                                }
                            }
                        }
                        else
                        {
                            if (modalidad.modo_entrega == "Entregado en domicilio")
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = precio4.local + (precio4.enPuerta * 2);
                                }
                                else
                                {
                                    costo = precio4.local + precio4.enPuerta;
                                }
                            }
                            else
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = precio4.local + precio4.enPuerta;
                                }
                                else
                                {
                                    costo = precio4.local;
                                }
                            }
                        }

                    }
                    else
                    {
                        if (peso > 10000)
                        {
                            if (modalidad.tipo_envio == "Urgente")
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio3.local * precio3.urgente + (precio3.enPuerta * 2);
                                    }
                                    else
                                    {
                                        costo = precio3.local * precio3.urgente + precio3.enPuerta;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio3.local * precio3.urgente + precio3.enPuerta;
                                    }
                                    else
                                    {
                                        costo = precio3.local * precio3.urgente;
                                    }
                                }
                            }
                            else
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio3.local + (precio3.enPuerta * 2);
                                    }
                                    else
                                    {
                                        costo = precio3.local + precio3.enPuerta;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = precio3.local + precio3.enPuerta;
                                    }
                                    else
                                    {
                                        costo = precio3.local;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (peso > 500)
                            {
                                if (modalidad.tipo_envio == "Urgente")
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio2.local * precio2.urgente + (precio2.enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = precio2.local * precio2.urgente + precio2.enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio2.local * precio2.urgente + precio2.enPuerta;
                                        }
                                        else
                                        {
                                            costo = precio2.local * precio2.urgente;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio2.local + (precio2.enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = precio2.local + precio2.enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio2.local + precio2.enPuerta;
                                        }
                                        else
                                        {
                                            costo = precio2.local;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (modalidad.tipo_envio == "Urgente")
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio.local * precio.urgente + (precio.enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = precio.local * precio.urgente + precio.enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio.local * precio.urgente + precio.enPuerta;
                                        }
                                        else
                                        {
                                            costo = precio.local * precio.urgente;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio.local + (precio.enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = precio.local + precio.enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = precio.local + precio.enPuerta;
                                        }
                                        else
                                        {
                                            costo = precio.local;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return costo;
        }
    }

    
}
