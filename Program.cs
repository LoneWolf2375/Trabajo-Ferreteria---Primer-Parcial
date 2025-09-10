using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Administracion
{
    class Program
    {
        static string GetName()
        {
            Console.WriteLine("Nombre del producto: ");
            Console.Write("- ");
            string nombre = Console.ReadLine();

            return nombre;
        }

        static bool GetAvailability()
        {
            bool disponible = false;
            
            Console.WriteLine("Esta disponibile?");

            bool confirmacion = ValidarConfirmacion(); 

            if (confirmacion)
            {
                disponible = true;
            }
            else
            {
                if (!confirmacion)
                {
                    disponible = false;
                }
            }

            return disponible;
        }

        static int ValidarEntero(int minValor = 0, int maxValor = 999999999, string Error = "", string NomVariable = "")
        {
            if (NomVariable != "")
            {
                Console.WriteLine(NomVariable + " del producto:");
            }
            Console.Write("- ");
            string ingreso = Console.ReadLine();
            int resultado;

            while (true)
            {
                while (!int.TryParse(ingreso, out resultado))
                {
                    Console.WriteLine("Ingreso invalido! " + Error);
                    Console.Write("- ");
                    ingreso = Console.ReadLine();
                }

                if (minValor <= resultado & resultado <= maxValor)
                {
                    break;
                }
                else
                {
                    ingreso = "";
                }
            }

            return resultado;
        }

        static bool ValidarConfirmacion()
        {
            bool confirmacion;

            Console.WriteLine("Si/No");

            while (true)
            {
                Console.Write("- ");
                string respuesta = Console.ReadLine();

                if (respuesta == "Si")
                {
                    confirmacion = true;
                    break;
                }
                else
                {
                    if (respuesta == "No")
                    {
                        confirmacion= false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ingreso invalido! Por favor ingesar 'Si' o 'No'");
                    }
                }
            }
            
            return confirmacion;
        }

        static void Main(string[] strings)
        {
            List<Producto> productos = new List<Producto>();

            bool exitLoop = false;
            while (!exitLoop)
            {
                Console.WriteLine("Hello! Choose one of the following options:");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Add Stock");
                Console.WriteLine("3. Change Availability");
                Console.WriteLine("4. Check Item");

                Console.Write("- ");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (0 <= choice & choice < 5)
                {
                    Console.WriteLine("Elegiste: " + choice);

                    switch (choice)
                    {
                        case 0:
                            exitLoop = true;

                            break;

                        case 1:
                            string nombre = GetName();

                            int precio = ValidarEntero(1000, 999999999, "Debe ser un entero mayor o igual a 1.000 Gs", "Precio");

                            bool disponible = GetAvailability();

                            Console.WriteLine("Los datos ingresados son:");
                            Console.WriteLine("Nombre: " + nombre);
                            Console.WriteLine("Precio: " + precio);
                            Console.WriteLine("Disponible: " + disponible);

                            Console.WriteLine("Por favor, confirmar para guardar: ");
                            bool confirmacionGuardar = ValidarConfirmacion();

                            if (confirmacionGuardar)
                            {
                                Producto newProducto = new Producto(nombre);

                                newProducto.Nombre = nombre;
                                newProducto.Precio = precio;
                                newProducto.Disponible = disponible;
                                newProducto.CantStock = 0;

                                productos.Add(newProducto);

                                Console.WriteLine("Elements in the list:");
                                foreach (var item in productos) Console.WriteLine(item);

                            }
                            else
                            {
                                if (!confirmacionGuardar)
                                {
                                    Console.WriteLine("Elegir una opcion:");
                                    Console.WriteLine("1. Cambiar valor");
                                    Console.WriteLine("2. Cancelar");

                                    int opcionNoGuardar = ValidarEntero(1, 2, "Debe ser uno de los opciones disponibles", "Opcion");

                                    switch (opcionNoGuardar)
                                    {
                                        case 1:
                                            Console.WriteLine("Elegir un valor a cambiar:");
                                            Console.WriteLine("1. Nombre");
                                            Console.WriteLine("2. Precio");
                                            Console.WriteLine("3. Disponibilidad");

                                            int opcionCambiar = ValidarEntero(1, 3, "Debe ser uno de los opciones disponibles", "Opcion");
                                            break;

                                        case 2:
                                            break;
                                    }


                                }
                            }

                                break;
                    }
                }
            }
        }
    }
}