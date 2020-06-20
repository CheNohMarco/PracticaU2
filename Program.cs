using System;
using System.Linq;
using Tienda.Models;

namespace Tienda
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1) Buscar producto");
            Console.WriteLine("2) Crear producto");
            Console.WriteLine("3) Eliminar producto");
            Console.WriteLine("4) Actualizar producto");
            Console.WriteLine("0) Salir");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    BuscarProductos();
                    break;
                case "2":
                    CrearProductos();
                    break;
                case "3":
                    ActualizarProducto();
                    break;
                case "4":
                    EliminarProducto();
                    break;
                case "5":
                    CrearVenta();
                    break;
                case "0": return;
            }
            Menu();
        }
        //venta
        public static void BuscarVenta()
        {
            Console.WriteLine("Buscar venta");
            Console.Write("Buscar: ");
            string buscar = Console.ReadLine();

            using (TiendaContext tiendaContect = new TiendaContext())
            {
                IQueryable<Venta> ventas = tiendaContect.Ventas.Where(p => p.Cliente.Contains(buscar));
                foreach(Venta venta in ventas)
                {
                    Console.WriteLine(venta);
                }
            }
        }

        public static void CrearVenta()
        {
            Console.WriteLine("Crear producto");
            Venta venta = new Venta();
            venta = LlenarVenta(venta);
           

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                tiendaContext.Add(venta);
                tiendaContext.SaveChanges();
                Console.WriteLine("Venta creada");
            }
        }

     

        public static Venta LlenarVenta(Venta venta)
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
            catch(FormatException e)
            {
                Console.WriteLine("No es el formato correcto:( Intente crear la venta  nuevamente");
                CrearVenta();

            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                CrearVenta();
            }

            return venta;
        }

        public static Venta  SeleccionarVenta()
        {

            BuscarVenta();
            Console.Write("Selecciona el código de la venta: ");
            uint id = uint.Parse(Console.ReadLine());

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                Venta venta = tiendaContext.Ventas.Find(id);
                if (venta == null)
                {
                    SeleccionarProducto();
                }
                return venta;
            }
        }

        public static void ActualizarVenta()
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

        public static void EliminarVenta()
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
        //productos
        public static void BuscarProductos()
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
                BuscarProductos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                BuscarProductos();
            }

        }

        public static void CrearProductos()
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
                CrearProductos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                CrearProductos();
            }

        }

        public static Producto LlenarProducto(Producto producto)
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
                CrearProductos();
            } catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                CrearProductos();
            }


            return producto;
        }



        public static Producto SeleccionarProducto()
        {

                BuscarProductos();
                Console.Write("Selecciona el código de producto: ");
                uint id = uint.Parse(Console.ReadLine());

                using (TiendaContext context = new TiendaContext())
                {
                    Producto producto = context.Productos.Find(id);
                    if (producto == null)
                    {
                        SeleccionarProducto();
                    }
                    return producto;
                }
            

        }

        public static void ActualizarProducto()
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
                BuscarProductos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente actualizar el producto nuevamente");
                ActualizarProducto();
            }

        }

        public static void EliminarProducto()
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
                EliminarProducto();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente eliminar el producto nuevamente");
                EliminarProducto();
            }

        }
    }
}
