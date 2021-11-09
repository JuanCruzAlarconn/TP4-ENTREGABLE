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

            continuar = validador.pedirSoN("Desea confirmar la orden? S/N\n Presione ENTER A CONTINUACIÓN");
            

            if (continuar == "S")
            {
                int codigo = Orden_de_servicio.asignar_codigo_servicio();//Asigna un código de orden de servicio
                List<Estado> estado = Orden_de_servicio.asignar_estado_inicial();//Genera la lista y le coloca el 1º estado
                var fecha = DateTime.Now;
                int codigo_seguro = Orden_de_servicio.asignar_seguro();//Aleatoria
                var orden=new Orden_de_servicio(codigo, codigo_seguro,costo,codigo_cliente,estado,fecha,paquete,origen, destino, modalidad);
                Console.WriteLine("Orden de servicio generada Nro:" + codigo);
                Orden_de_servicio.grabar(orden);
                //METODO PARA PASAR OBJETO A ESTADO DE CUENTA
            }


        }

        private double calcularCosto(Punto_logistico origen, Punto_logistico destino, Modalidad modalidad, decimal peso)
        {
            double costo = 0;
            double costoInternacional = 0;
            double urgente = 1.10;
            double enPuerta = 20;
            
            if (destino.continente_asignado == "America del sur limitrofe")
            {
                costoInternacional = 1000;
            }
            else
            {
                if (destino.continente_asignado == "America del sur")
                {
                    costoInternacional = 2000;
                }
                else
                {
                    if (destino.continente_asignado == "America del norte")
                    {
                        costoInternacional = 3000;
                    }
                    else
                    {
                        if (destino.continente_asignado == "Europa")
                        {
                            costoInternacional = 4000;
                        }
                        else
                        {
                            if (destino.continente_asignado == "Asia")
                            {
                                costoInternacional = 5000;
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
                                            costo = 10000 * urgente + (enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 10000 * urgente + enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 10000 * urgente + enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 10000 * urgente + costoInternacional;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 10000 + (enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 10000 + enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 10000 + enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 10000 + costoInternacional;
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
                                                costo = 8000 * urgente + (enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 8000 * urgente + enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 8000 * urgente + enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 8000 * urgente + costoInternacional;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 8000 + (enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 8000 + enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 8000 + enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 8000 + costoInternacional;
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
                                                    costo = 4000 * urgente + (enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 4000 * urgente + enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 4000 * urgente + enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 4000 * urgente + costoInternacional;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 4000 + (enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 4000 + enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 4000 + enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 4000 + costoInternacional;
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
                                                    costo = 2000 * urgente + (enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 2000 * urgente + enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 2000 * urgente + enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 2000 * urgente + costoInternacional;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 2000 + (enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 2000 + enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 2000 + enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 2000 + costoInternacional;
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
                                            costo = 8000 * urgente + (enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 8000 * urgente + enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 8000 * urgente + enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 8000 * urgente + costoInternacional;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 8000 + (enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 8000 + enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 8000 + enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 8000 + costoInternacional;
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
                                                costo = 6000 * urgente + (enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 6000 * urgente + enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 6000 * urgente + enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 6000 * urgente + costoInternacional;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 6000 + (enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 6000 + enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 6000 + enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 6000 + costoInternacional;
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
                                                    costo = 3000 * urgente + (enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 3000 * urgente + enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 3000 * urgente + enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 3000 * urgente + costoInternacional;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 3000 + (enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 3000 + enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 3000 + enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 3000 + costoInternacional;
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
                                                    costo = 1000 * urgente + (enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 1000 * urgente + enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 1000 * urgente + enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 1000 * urgente + costoInternacional;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 1000 + (enPuerta * 2) + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 1000 + enPuerta + costoInternacional;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 1000 + enPuerta + costoInternacional;
                                                }
                                                else
                                                {
                                                    costo = 1000 + costoInternacional;
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
                                        costo = 6000 * urgente + (enPuerta * 2) + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = 6000 * urgente + enPuerta + costoInternacional;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 6000 * urgente + enPuerta + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = 6000 * urgente + costoInternacional;
                                    }
                                }
                            }
                            else
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 6000 + (enPuerta * 2) + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = 6000 + enPuerta + costoInternacional;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 6000 + enPuerta + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = 6000 + costoInternacional;
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
                                            costo = 4000 * urgente + (enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 4000 * urgente + enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 4000 * urgente + enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 4000 * urgente + costoInternacional;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 4000 + (enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 4000 + enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 4000 + enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 4000 + costoInternacional;
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
                                                costo = 2000 * urgente + (enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 2000 * urgente + enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 2000 * urgente + enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 2000 * urgente + costoInternacional;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 2000 + (enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 2000 + enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 2000 + enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 2000 + costoInternacional;
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
                                                costo = 500 * urgente + (enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 500 * urgente + enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 500 * urgente + enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 500 * urgente + costoInternacional;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 500 + (enPuerta * 2) + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 500 + enPuerta + costoInternacional;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 500 + enPuerta + costoInternacional;
                                            }
                                            else
                                            {
                                                costo = 500 + costoInternacional;
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
                                    costo = 4000 * urgente + (enPuerta * 2) + costoInternacional;
                                }
                                else
                                {
                                    costo = 4000 * urgente + enPuerta + costoInternacional;
                                }
                            }
                            else
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = 4000 * urgente + enPuerta + costoInternacional;
                                }
                                else
                                {
                                    costo = 4000 * urgente + costoInternacional;
                                }
                            }
                        }
                        else
                        {
                            if (modalidad.modo_entrega == "Entregado en domicilio")
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = 4000 + (enPuerta * 2) + costoInternacional;
                                }
                                else
                                {
                                    costo = 4000 + enPuerta + costoInternacional;
                                }
                            }
                            else
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = 4000 + enPuerta + costoInternacional;
                                }
                                else
                                {
                                    costo = 4000 + costoInternacional;
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
                                        costo = 2000 * urgente + (enPuerta * 2) + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = 2000 * urgente + enPuerta + costoInternacional;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 2000 * urgente + enPuerta + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = 2000 * urgente + costoInternacional;
                                    }
                                }
                            }
                            else
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 2000 + (enPuerta * 2) + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = 2000 + enPuerta + costoInternacional;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 2000 + enPuerta + costoInternacional;
                                    }
                                    else
                                    {
                                        costo = 2000 + costoInternacional;
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
                                            costo = 1000 * urgente + (enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 1000 * urgente + enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 1000 * urgente + enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 1000 * urgente + costoInternacional;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 1000 + (enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 1000 + enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 1000 + enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 1000 + costoInternacional;
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
                                            costo = 250 * urgente + (enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 250 * urgente + enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 250 * urgente + enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 250 * urgente + costoInternacional;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 250 + (enPuerta * 2) + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 250 + enPuerta + costoInternacional;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 250 + enPuerta + costoInternacional;
                                        }
                                        else
                                        {
                                            costo = 250 + costoInternacional;
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
                                            costo = 10000 * urgente + (enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = 10000 * urgente + enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
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
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
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
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
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
                                if (peso > 10000)
                                {
                                    if (modalidad.tipo_envio == "Urgente")
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 8000 * urgente + (enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = 8000 * urgente + enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 8000 * urgente + enPuerta;
                                            }
                                            else
                                            {
                                                costo = 8000 * urgente;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 8000 + (enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = 8000 + enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 8000 + enPuerta;
                                            }
                                            else
                                            {
                                                costo = 8000;
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
                                                    costo = 4000 * urgente + (enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = 4000 * urgente + enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 4000 * urgente + enPuerta;
                                                }
                                                else
                                                {
                                                    costo = 4000 * urgente;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 4000 + (enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = 4000 + enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 4000 + enPuerta;
                                                }
                                                else
                                                {
                                                    costo = 4000;
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
                                                    costo = 2000 * urgente + (enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = 2000 * urgente + enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 2000 * urgente + enPuerta;
                                                }
                                                else
                                                {
                                                    costo = 2000 * urgente;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 2000 + (enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = 2000 + enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 2000 + enPuerta;
                                                }
                                                else
                                                {
                                                    costo = 2000;
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
                                            costo = 8000 * urgente + (enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = 8000 * urgente + enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 8000 * urgente + enPuerta;
                                        }
                                        else
                                        {
                                            costo = 8000 * urgente;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 8000 + (enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = 8000 + enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 8000 + enPuerta;
                                        }
                                        else
                                        {
                                            costo = 8000;
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
                                                costo = 6000 * urgente + (enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = 6000 * urgente + enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 6000 * urgente + enPuerta;
                                            }
                                            else
                                            {
                                                costo = 6000 * urgente;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 6000 + (enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = 6000 + enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 6000 + enPuerta;
                                            }
                                            else
                                            {
                                                costo = 6000;
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
                                                    costo = 3000 * urgente + (enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = 3000 * urgente + enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 3000 * urgente + enPuerta;
                                                }
                                                else
                                                {
                                                    costo = 3000 * urgente;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 3000 + (enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = 3000 + enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 3000 + enPuerta;
                                                }
                                                else
                                                {
                                                    costo = 3000;
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
                                                    costo = 1000 * urgente + (enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = 1000 * urgente + enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 1000 * urgente + enPuerta;
                                                }
                                                else
                                                {
                                                    costo = 1000 * urgente;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_entrega == "Entregado en domicilio")
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 1000 + (enPuerta * 2);
                                                }
                                                else
                                                {
                                                    costo = 1000 + enPuerta;
                                                }
                                            }
                                            else
                                            {
                                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                                {
                                                    costo = 1000 + enPuerta;
                                                }
                                                else
                                                {
                                                    costo = 1000;
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
                                        costo = 6000 * urgente + (enPuerta * 2);
                                    }
                                    else
                                    {
                                        costo = 6000 * urgente + enPuerta;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 6000 * urgente + enPuerta;
                                    }
                                    else
                                    {
                                        costo = 6000 * urgente;
                                    }
                                }
                            }
                            else
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 6000 + (enPuerta * 2);
                                    }
                                    else
                                    {
                                        costo = 6000 + enPuerta;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 6000 + enPuerta;
                                    }
                                    else
                                    {
                                        costo = 6000;
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
                                            costo = 4000 * urgente + (enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = 4000 * urgente + enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 4000 * urgente + enPuerta;
                                        }
                                        else
                                        {
                                            costo = 4000 * urgente;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 4000 + (enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = 4000 + enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 4000 + enPuerta;
                                        }
                                        else
                                        {
                                            costo = 4000;
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
                                                costo = 2000 * urgente + (enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = 2000 * urgente + enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 2000 * urgente + enPuerta;
                                            }
                                            else
                                            {
                                                costo = 2000 * urgente;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 2000 + (enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = 2000 + enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 2000 + enPuerta;
                                            }
                                            else
                                            {
                                                costo = 2000;
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
                                                costo = 500 * urgente + (enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = 500 * urgente + enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 500 * urgente + enPuerta;
                                            }
                                            else
                                            {
                                                costo = 500 * urgente;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_entrega == "Entregado en domicilio")
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 500 + (enPuerta * 2);
                                            }
                                            else
                                            {
                                                costo = 500 + enPuerta;
                                            }
                                        }
                                        else
                                        {
                                            if (modalidad.modo_retiro == "Retirado en domicilio")
                                            {
                                                costo = 500 + enPuerta;
                                            }
                                            else
                                            {
                                                costo = 500;
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
                                    costo = 4000 * urgente + (enPuerta * 2);
                                }
                                else
                                {
                                    costo = 4000 * urgente + enPuerta;
                                }
                            }
                            else
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = 4000 * urgente + enPuerta;
                                }
                                else
                                {
                                    costo = 4000 * urgente;
                                }
                            }
                        }
                        else
                        {
                            if (modalidad.modo_entrega == "Entregado en domicilio")
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = 4000 + (enPuerta * 2);
                                }
                                else
                                {
                                    costo = 4000 + enPuerta;
                                }
                            }
                            else
                            {
                                if (modalidad.modo_retiro == "Retirado en domicilio")
                                {
                                    costo = 4000 + enPuerta;
                                }
                                else
                                {
                                    costo = 4000;
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
                                        costo = 2000 * urgente + (enPuerta * 2);
                                    }
                                    else
                                    {
                                        costo = 2000 * urgente + enPuerta;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 2000 * urgente + enPuerta;
                                    }
                                    else
                                    {
                                        costo = 2000 * urgente;
                                    }
                                }
                            }
                            else
                            {
                                if (modalidad.modo_entrega == "Entregado en domicilio")
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 2000 + (enPuerta * 2);
                                    }
                                    else
                                    {
                                        costo = 2000 + enPuerta;
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_retiro == "Retirado en domicilio")
                                    {
                                        costo = 2000 + enPuerta;
                                    }
                                    else
                                    {
                                        costo = 2000;
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
                                            costo = 1000 * urgente + (enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = 1000 * urgente + enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 1000 * urgente + enPuerta;
                                        }
                                        else
                                        {
                                            costo = 1000 * urgente;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 1000 + (enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = 1000 + enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 1000 + enPuerta;
                                        }
                                        else
                                        {
                                            costo = 1000;
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
                                            costo = 250 * urgente + (enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = 250 * urgente + enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 250 * urgente + enPuerta;
                                        }
                                        else
                                        {
                                            costo = 250 * urgente;
                                        }
                                    }
                                }
                                else
                                {
                                    if (modalidad.modo_entrega == "Entregado en domicilio")
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 250 + (enPuerta * 2);
                                        }
                                        else
                                        {
                                            costo = 250 + enPuerta;
                                        }
                                    }
                                    else
                                    {
                                        if (modalidad.modo_retiro == "Retirado en domicilio")
                                        {
                                            costo = 250 + enPuerta;
                                        }
                                        else
                                        {
                                            costo = 250;
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
