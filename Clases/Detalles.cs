using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tienda.Interfaces;
using Tienda.Models;

namespace Tienda.Clases
{
    class Detalles : Icrud, IDetalle
    {
        public void Menu()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||      Menu  detalle     ||");
            Console.WriteLine("============================");
            Console.WriteLine("||   1) Buscar detalle    ||");
            Console.WriteLine("||   2) Crear detalle     ||");
            Console.WriteLine("||  3) Eliminar detalle   ||");
            Console.WriteLine("|| 4) Actualizar detalle  ||");
            Console.WriteLine("||  5) Regresar al menu   ||");
            Console.WriteLine("||       0) Salir         ||");
            Console.WriteLine("============================");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    Buscar();
                    break;
                case "2":
                    Crear();
                    break;
                case "3":
                    Editar();
                    break;
                case "4":
                    Eliminar();
                    break;
                case "5":
                    Program p = new Program();
                    p.menu();
                    break;
                case "0": return;
                default:
                    Console.WriteLine("Introduzca una opción valida");
                    Menu();
                    break;
            }
            Menu();
        }
        public void Buscar()
        {
            try
            {
                Console.WriteLine("Buscar detalle");
                Console.Write("Buscar: ");
                string buscar = Console.ReadLine();

                using (TiendaContext tiendaContext = new TiendaContext())
                {
                    IQueryable<Detalle> detalles = tiendaContext.Detalles.Where(d => d.Producto.Nombre.Contains(buscar));
                    if (detalles.Count() == 0)
                    {
                        Console.WriteLine("No se obtuvieron resultado en la busqueda");
                        Buscar();
                    }
                    else
                    {

                        foreach (Detalle detalle in detalles)
                        {
                            Console.WriteLine(detalles);
                        }

                    }
                }

            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                Buscar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente buscar el detalle nuevamente");
                Buscar();
            }
        }

        public void Crear()
        {
            try
            {
                Console.WriteLine("Crear detalle");
                Detalle detalle = new Detalle();
                detalle = LlenarDetalle(detalle);

                using (TiendaContext tiendaContext = new TiendaContext())
                {

                    tiendaContext.Add(detalle);
                    tiendaContext.SaveChanges();
                    Console.WriteLine("Detalle creado");

                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                Crear();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el detalle nuevamente");
                Crear();
            }

        }

        public void Editar()
        {
            try
            {
                Console.WriteLine("Actualizar detalle");
                Detalle detalle = SeleccionarDetalle();
                detalle = LlenarDetalle(detalle);

                using (TiendaContext tiendaContext = new TiendaContext())
                {
                    tiendaContext.Update(detalle);
                    tiendaContext.SaveChanges();
                    Console.WriteLine("Detalle actualizado exitosamente");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                Buscar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente actualizar el detalle nuevamente");
                Editar();
            }
        }

        public void Eliminar()
        {
            try
            {
                Console.WriteLine("Eliminar detalle");
                Detalle detalle = SeleccionarDetalle();

                using (TiendaContext tiendaContext = new TiendaContext())
                {
                    tiendaContext.Remove(detalle);
                    tiendaContext.SaveChanges();
                    Console.WriteLine("Detalle eliminado exitosamente :)");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                Eliminar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente eliminar el detalle nuevamente");
                Eliminar();
            }
        }

        public Detalle LlenarDetalle(Detalle detalle)
        {
            try
            {
                Console.WriteLine("Producto id");
                detalle.ProductoId = uint.Parse(Console.ReadLine());
                Console.WriteLine("Venta id");
                detalle.VentaId = uint.Parse(Console.ReadLine());
                Console.WriteLine("Subtotal");
                detalle.Subtotal = decimal.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("No es el formato correcto:( Intente crear el detalle nuevamente");
                Crear();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el detalle nuevamente");
                Crear();
            }
            return detalle;
        }

        public Detalle SeleccionarDetalle()
        {
            Buscar();
            Console.Write("Selecciona el código del detalle: ");
            uint id = uint.Parse(Console.ReadLine());

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                Detalle detalle = tiendaContext.Detalles.Find(id);
                if (detalle == null)
                {
                    Console.WriteLine("Selecciona el código correcto");
                    SeleccionarDetalle();
                }
                return detalle;
            }
        }
    }
}
