using System;
using System.Linq;
using Tienda.Models;
using System.Security.Cryptography;
using System.Text;
using Tienda.Clases;

namespace Tienda
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Bienvenido();
        }

        //Login
        public  void Bienvenido()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||        Bienvenido      ||");
            Console.WriteLine("============================");
            Console.WriteLine("||                        ||");
            Console.WriteLine("||        1) Entrar       ||");
            Console.WriteLine("||    2) Crear usuario    ||");
            Console.WriteLine("||        0) Salir        ||");
            Console.WriteLine("============================");

            Usuarios u = new Usuarios();
            string opcion = Console.ReadLine();
            switch(opcion)
            {
                case "1":
                    u.Login();
                    break;
                case "2":
                    u.CrearUsuario();
                    break;
                case "0": return;
                default:
                    Console.WriteLine("Introduzca una opción valida");
                    Bienvenido();
                    break;
            }
        }

        public void menu()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||           Menú         ||");
            Console.WriteLine("============================");
            Console.WriteLine("||                        ||");
            Console.WriteLine("||      1) Productos      ||");
            Console.WriteLine("||        2) Ventas       ||");
            Console.WriteLine("||      3) Detalles       ||");
            Console.WriteLine("||  4) Regresar al menú   ||");
            Console.WriteLine("||        0) Salir        ||");
            Console.WriteLine("============================");

           
            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    Productos p = new Productos();
                    p.Menu();
                    break;
                case "2":
                    Ventas v = new Ventas();
                    v.Menu();
                    break;
                case "3":
                    Detalles d = new Detalles();
                    d.Menu();
                    break;
                case "4":
                    Bienvenido();
                    break;
                case "0": return;
                default:
                    Console.WriteLine("Introduzca una opción valida");
                    menu();
                    break;
            }
        }

    }
}
