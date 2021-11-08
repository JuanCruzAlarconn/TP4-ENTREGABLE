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
        private ConjuntoDeOrdenes ordenes;
        private ConjuntoDeSucursales sucursales;

        public AppCrearOrdenes()
        {
            validador = new Validador();
            ordenes = new ConjuntoDeOrdenes();
            sucursales = new ConjuntoDeSucursales();
        }

        public void ejecutar(int codigo_cliente)
        {
            int cliente = codigo_cliente;
            //int sucursal = 0;
            string continuar = "";
            double costo = 0; //quitar luego de desarrollar el metodo para calcular el costo

            Console.WriteLine("Completa los datos de tu envío" + "\n");
            //int postal = validador.pedirInteger("Ingrese el código postal del destino\n", 0, 999999);

            /*do
            {
                Console.WriteLine("Listado de sucursales" + "\n");
                string mensajeAMostrar = sucursales.listado();
                Console.WriteLine(mensajeAMostrar);
                sucursal = validador.pedirInteger("Ingrese la sucursal de origen\n", 0, 999);

                if (!sucursales.existe(sucursal))
                {
                    Console.WriteLine("El codigo ingresado no se corresponde con ninguna de las sucursales que se encuentran dentro de la base de datos,\n por favor ingrese una sucursal nuevamente\n");
                }
                else
                {
                    Paquete paquete = Paquete.crear();

                    string nombre = validador.pedirChar("Ingrese el nombre del destinatario\n", 30);
                    string direccion = validador.pedirString("Ingrese la dirección del destinatario\n");

                    //costo = calcularCosto(tipo, postal, paquete.peso, tipo2);
                    //Console.WriteLine("El costo del servicio es:" + costo + "\n");

                    continuar = validador.pedirSoN("Desea confirmar la orden? S/N\n");

                    if (continuar == "S")
                    {
                        int codigo = ordenes.asignar_codigo_servicio();
                        string estado = "Inicializado";
                        DateTime fecha = DateTime.Now;
                        ordenes.agregar(new Orden_de_servicio2(codigo, cliente, postal, logistica, modalidad, sucursal, nombre, direccion, costo, estado, fecha));
                        Console.WriteLine("Orden de servicio generada Nro:" + codigo);
                    }
                }
            } while (!sucursales.existe(sucursal));*/

            Punto_logistico origen = Punto_logistico.crear("origen");
            Punto_logistico destino = Punto_logistico.crear("destino");
            Modalidad modalidad = Modalidad.crear();
            Paquete paquete = Paquete.crear();

            costo = calcularCosto(origen, destino, modalidad, paquete.peso);
            Console.WriteLine("El costo del servicio es:" + costo + "\n");

            continuar = validador.pedirSoN("Desea confirmar la orden? S/N\n");

            if (continuar == "S")
            {
                int codigo = ordenes.asignar_codigo_servicio();
                string estado = "Inicializado";
                DateTime fecha = DateTime.Now;
                ordenes.agregar(new Orden_de_servicio2(codigo, cliente, origen, destino, modalidad, costo, estado, fecha));
                Console.WriteLine("Orden de servicio generada Nro:" + codigo);
            }


        }

        private double calcularCosto(Punto_logistico origen, Punto_logistico destino, Modalidad modalidad, decimal peso)
        {
            double costo = 0;        
            double urgente = 1.10;
            double enPuerta = 20;

            if (destino.pais != "Argentina") 
            {
                if(peso > 20000)
                {
                    if (modalidad.tipo_envio == "Urgente")
                    {
                        if(modalidad.modo_entrega == "Retirado en domicilio")
                        {
                            if(modalidad.modo_retiro == "Entregado en domicilio")
                            {
                                costo = 10000 * urgente + (enPuerta * 2);
                            }
                            else
                            {
                                costo = 10000 * urgente + enPuerta;
                            }
                        }
                        else
                        {
                            if (modalidad.modo_retiro == "Entregado en domicilio")
                            {
                                costo = 10000 * urgente + enPuerta;
                            }
                            else
                            {
                                costo = 10000 * urgente;
                            }
                        }
                    }
                    else
                    {
                        if (modalidad.modo_entrega == "Retirado en domicilio")
                        {
                            if (modalidad.modo_retiro == "Entregado en domicilio")
                            {
                                costo = 10000 + (enPuerta * 2);
                            }
                            else
                            {
                                costo = 10000 + enPuerta;
                            }
                        }
                        else
                        {
                            if (modalidad.modo_retiro == "Entregado en domicilio")
                            {
                                costo = 10000 + enPuerta;
                            }
                            else
                            {
                                costo = 10000;
                            }
                        }
                    }

                }
                else
                {
                    if(peso > 10000)
                    {
                        if (modalidad.tipo_envio == "Urgente")
                        {
                            if (modalidad.modo_entrega == "Retirado en domicilio")
                            {
                                if (modalidad.modo_retiro == "Entregado en domicilio")
                                {
                                    costo = 10000 * urgente + (enPuerta * 2);
                                }
                                else
                                {
                                    costo = 10000 * urgente + enPuerta;
                                }
                            }
                            else
                            {
                                if (modalidad.modo_retiro == "Entregado en domicilio")
                                {
                                    costo = 10000 * urgente + enPuerta;
                                }
                                else
                                {
                                    costo = 10000 * urgente;
                                }
                            }
                        }
                        else
                        {
                            if (modalidad.modo_entrega == "Retirado en domicilio")
                            {
                                if (modalidad.modo_retiro == "Entregado en domicilio")
                                {
                                    costo = 10000 + (enPuerta * 2);
                                }
                                else
                                {
                                    costo = 10000 + enPuerta;
                                }
                            }
                            else
                            {
                                if (modalidad.modo_retiro == "Entregado en domicilio")
                                {
                                    costo = 10000 + enPuerta;
                                }
                                else
                                {
                                    costo = 10000;
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
