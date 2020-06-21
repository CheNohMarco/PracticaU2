using System;
using System.Collections.Generic;
using System.Linq;
using Tienda.Interfaces;
using Tienda.Models;

namespace Tienda.Clases
{
    class Productos : Icrud, IProducto
    {
        public void Menu()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||      Menú producto     ||");
            Console.WriteLine("============================");
            Console.WriteLine("||   1) Buscar producto   ||");
            Console.WriteLine("||   2) Crear producto    ||");
            Console.WriteLine("||  3) Eliminar producto  ||");
            Console.WriteLine("|| 4) Actualizar producto ||");
            Console.WriteLine("|| 5) Regresar al menu    ||");
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
                    Eliminar();
                    break;
                case "4":
                    Editar();
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
        //productos
        public void Buscar()
        {
            try
            {
                Console.WriteLine("Buscar prodcutos");
                Console.Write("Buscar: ");
                string buscar = Console.ReadLine();

                using (TiendaContext context = new TiendaContext())
                {
                    IQueryable<Producto> productos = context.Productos.Where(p => p.Nombre.Contains(buscar));

                    if (productos.Count() == 0)
                    {
                        Console.WriteLine("No se obtuvieron resultado en la busqueda");
                        Buscar();
                    }
                    else
                    {
                        foreach (Producto producto in productos)
                        {
                            Console.WriteLine(producto);
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
                Console.WriteLine("Ocurrio un error en el proceso. Intente buscar el producto nuevamente");
                Buscar();
            }
        }

        public void Crear()
        {
            try
            {
                Console.WriteLine("Crear producto");
                Producto producto = new Producto();
                producto = LlenarProducto(producto);

                using (TiendaContext context = new TiendaContext())
                {
                    context.Add(producto);
                    context.SaveChanges();
                    Console.WriteLine("Producto creado exitosamente");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                Crear();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                Crear();
            }
        }

        public void Editar()
        {
            try
            {
                Console.WriteLine("Actualizar producto");
                Producto producto = SeleccionarProducto();
                producto = LlenarProducto(producto);

                using (TiendaContext context = new TiendaContext())
                {
                    context.Update(producto);
                    context.SaveChanges();
                    Console.WriteLine("Producto actualizado exitosamente");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                Buscar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente actualizar el producto nuevamente");
                Editar();
            }
        }

        public void Eliminar()
        {
            try
            {
                Console.WriteLine("Eliminar producto");
                Producto producto = SeleccionarProducto();

                using (TiendaContext context = new TiendaContext())
                {
                    context.Remove(producto);
                    context.SaveChanges();
                    Console.WriteLine("Producto eliminado exitosamente");
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

        public Producto SeleccionarProducto()
        {
            Buscar();
            Console.Write("Selecciona el código de producto: ");
            uint id = uint.Parse(Console.ReadLine());

            using (TiendaContext context = new TiendaContext())
            {
                Producto producto = context.Productos.Find(id);
                if (producto == null)
                {
                    Console.WriteLine("Selecciona el código correcto");
                    SeleccionarProducto();
                }
                return producto;
            }
        }

        public Producto LlenarProducto(Producto producto)
        {
            try
            {
                Console.Write("Nombre: ");
                producto.Nombre = Console.ReadLine();
                Console.Write("Descripcion: ");
                producto.Descripcion = Console.ReadLine();
                Console.Write("Precio: ");
                producto.Precio = decimal.Parse(Console.ReadLine());
                Console.Write("Costo: ");
                producto.Costo = decimal.Parse(Console.ReadLine());
                Console.Write("Cantidad: ");
                producto.Cantidad = decimal.Parse(Console.ReadLine());
                Console.Write("Tamaño: ");
                producto.Tamano = Console.ReadLine();
            }
            
            catch (FormatException e)
            {
                Console.WriteLine("No es el formato correcto:( Intente crear el producto nuevamente");
                Crear();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Intente crear el producto nuevamente");
                Crear();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                Crear();
            }
            return producto;
        }
    }
}
