using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tienda.Interfaces;
using Tienda.Models;

namespace Tienda.Clases
{
     class Ventas : Icrud, IVenta
    {
        public void Menu()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||       Menu ventas      ||");
            Console.WriteLine("============================");
            Console.WriteLine("||    1) Buscar venta     ||");
            Console.WriteLine("||    2) Crear venta      ||");
            Console.WriteLine("||   3) Eliminar venta    ||");
            Console.WriteLine("||  4) Actualizar venta   ||");
            Console.WriteLine("||   5) Regresar al menu  ||");
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
                Console.WriteLine("Buscar venta");
                Console.Write("Buscar: ");
                string buscar = Console.ReadLine();

                using (TiendaContext tiendaContect = new TiendaContext())
                {
                    IQueryable<Venta> ventas = tiendaContect.Ventas.Where(p => p.Cliente.Contains(buscar));

                    if (ventas.Count() == 0)
                    {
                        Console.WriteLine("No se obtuvieron resultado en la busqueda");
                        Buscar();
                    }
                    else
                    {
                        foreach (Venta venta in ventas)
                        {
                            Console.WriteLine(venta);
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
                Console.WriteLine("Ocurrio un error en el proceso. Intente buscar la venta nuevamente");
                Buscar();
            }

        }

        public void Crear()
        {
            try
            {
                Console.WriteLine("Crear venta");
                Venta venta = new Venta();
                venta = LlenarVenta(venta);


                using (TiendaContext tiendaContext = new TiendaContext())
                {
                    tiendaContext.Add(venta);
                    tiendaContext.SaveChanges();
                    Console.WriteLine("Venta creada");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                Crear();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear la venta nuevamente");
                Crear();
            }
        }

        public void Editar()
        {
            try
            {
                Console.WriteLine("Actualizar venta");
                Venta venta = SeleccionarVenta();
                venta = LlenarVenta(venta);

                using (TiendaContext tiendaContext = new TiendaContext())
                {
                    tiendaContext.Update(venta);
                    tiendaContext.SaveChanges();
                    Console.WriteLine("Venta actualizada exitosamente");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                Buscar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente actualizar la venta nuevamente");
                Editar();
            }
        }

        public void Eliminar()
        {
            try
            {
                Console.WriteLine("Eliminar venta");
                Venta venta = SeleccionarVenta();

                using (TiendaContext tiendaContext = new TiendaContext())
                {
                    tiendaContext.Remove(venta);
                    tiendaContext.SaveChanges();
                    Console.WriteLine("Venta eliminada exitosamente :)");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                Eliminar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente eliminar el producto nuevamente");
                Eliminar();
            }

        }

        public Venta LlenarVenta(Venta venta)
        {
            try
            {
                Console.WriteLine("Total");
                venta.Total = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Fecha");
                venta.Fecha = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Cliente");
                venta.Cliente = Console.ReadLine();

            }
            catch (FormatException e)
            {
                Console.WriteLine("No es el formato correcto:( Intente crear la venta nuevamente");
                Crear();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Intente crear el producto nuevamente");
                Crear();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear la venta nuevamente");
                Crear();
            }
            return venta;
        }

        public Venta SeleccionarVenta()
        {
            Buscar();
            Console.Write("Selecciona el código de la venta: ");
            uint id = uint.Parse(Console.ReadLine());

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                Venta venta = tiendaContext.Ventas.Find(id);
                if (venta == null)
                {
                    Console.WriteLine ("Selecciona el código correcto");
                    SeleccionarVenta();
                }
                return venta;
            }
        }
    }
}
