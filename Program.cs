using System;
using System.Collections.Generic;

namespace Administracion
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Producto> ListaProductos = new List<Producto>();

            bool exitMainLoop = false;
            while (!exitMainLoop)
            {
                Console.WriteLine("=======================================");
                Console.WriteLine("   SISTEMA DE INVENTARIO - FERRETERÍA  ");
                Console.WriteLine("=======================================");
                Console.WriteLine("1. Añadir producto al sistema");
                Console.WriteLine("2. Agregar producto al stock");
                Console.WriteLine("3. Modificar producto existente");
                Console.WriteLine("4. Buscar producto por ID o nombre");
                Console.WriteLine("5. Listar productos existentes");
                Console.WriteLine("0. Salir");
                Console.WriteLine("=======================================");

                int choice = ProductoService.ValidarEntero(0, 5, "Debe ser una de las opciones disponibles", "Opción");

                switch (choice)
                {
                    case 0: // Salir del programa
                        exitMainLoop = true;
                        Console.WriteLine("\n-Saliendo del programa...-\n");
                        break;

                    case 1: // Añadir un producto
                        string nombre = ProductoService.GetName(true);
                        int precio = ProductoService.ValidarEntero(1000, 999999999, "Debe ser un entero mayor o igual a 1.000 Gs", "Precio");
                        bool disponible = ProductoService.GetAvailability(false);

                        bool exitSaveLoop = false;
                        while (!exitSaveLoop)
                        {
                            Console.WriteLine("\nLos datos ingresados son:");
                            Console.WriteLine($" Nombre: {nombre}");
                            Console.WriteLine($" Precio: {precio}");
                            Console.WriteLine($" Disponible: {disponible}");

                            Console.WriteLine("\n¿Confirmar para guardar?");
                            bool confirmacionGuardar = ProductoService.ValidarConfirmacion();

                            if (confirmacionGuardar)
                            {
                                Producto newProducto = ProductoService.GuardarProducto(nombre, precio, disponible);
                                ListaProductos.Add(newProducto);

                                Console.WriteLine("\n-Producto guardado con éxito-\n");
                                exitSaveLoop = true;
                            }
                            else
                            {
                                Console.WriteLine("\nElegir una opción:");
                                Console.WriteLine(" 1. Cambiar valor");
                                Console.WriteLine(" 2. Cancelar");

                                int opcionNoGuardar = ProductoService.ValidarEntero(1, 2, "Debe ser una de las opciones disponibles", "");

                                switch (opcionNoGuardar)
                                {
                                    case 1:
                                        Console.WriteLine("\nElegir un valor a cambiar:");
                                        Console.WriteLine("1. Nombre");
                                        Console.WriteLine("2. Precio");
                                        Console.WriteLine("3. Disponibilidad");

                                        int opcionCambiar = ProductoService.ValidarEntero(1, 3, "Debe ser una de las opciones disponibles", "");

                                        switch (opcionCambiar)
                                        {
                                            case 1:
                                                nombre = (string)ProductoService.ModificarAtributo(nombre, precio, disponible, 0, opcionCambiar);
                                                break;
                                            case 2:
                                                precio = (int)ProductoService.ModificarAtributo(nombre, precio, disponible, 0, opcionCambiar);
                                                break;
                                            case 3:
                                                disponible = (bool)ProductoService.ModificarAtributo(nombre, precio, disponible, 0, opcionCambiar);
                                                break;
                                        }
                                        break;

                                    case 2:
                                        Console.WriteLine("\n-Guardado cancelado-\n");
                                        exitSaveLoop = true;
                                        break;
                                }
                            }
                        }
                        break;

                    case 2: // Agregar stock
                        var find = ProductoService.BuscarProducto(ListaProductos, true, true);

                        if (find != null)
                        {
                            Console.WriteLine();
                            int nuevoStock = ProductoService.ValidarEntero(0, 999999999, "Debe ser un entero positivo", "Cantidad a agregar");

                            find.CantStock += nuevoStock;

                            Console.WriteLine($"\n-Stock actualizado con éxito: {find.CantStock}-\n");

                            if (find.CantStock > 0 && !find.Disponible)
                            {
                                Console.WriteLine("El producto no estaba disponible, ¿desea cambiar su estado a disponible?");
                                bool cambiarEstado = ProductoService.ValidarConfirmacion();

                                if (cambiarEstado)
                                {
                                    find.Disponible = true;
                                    Console.WriteLine("\n-El producto ahora está disponible-\n");
                                }
                                else
                                {
                                    Console.WriteLine("\n-El producto se mantiene como no disponible-\n");
                                }
                            }
                        }
                        break;

                    case 3: // Modificar atributos
                        var findModificar = ProductoService.BuscarProducto(ListaProductos, true);

                        if (findModificar != null)
                        {
                            Console.WriteLine("\nElegir un atributo a cambiar:");
                            Console.WriteLine(" 1. Nombre");
                            Console.WriteLine(" 2. Precio");
                            Console.WriteLine(" 3. Disponibilidad");
                            Console.WriteLine(" 4. Stock");

                            int opcionCambiarAtributo = ProductoService.ValidarEntero(1, 4, "Debe ser una de las opciones disponibles", "");
                            string nuevoValor = "";

                            switch (opcionCambiarAtributo)
                            {
                                case 1:
                                    nuevoValor = (string)ProductoService.ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo);
                                    break;
                                case 2:
                                    nuevoValor = ProductoService.ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo).ToString();
                                    break;
                                case 3:
                                    nuevoValor = ProductoService.ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo).ToString();
                                    break;
                                case 4:
                                    nuevoValor = ProductoService.ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo).ToString();
                                    break;
                            }

                            ProductoService.ModificarProducto(ListaProductos, findModificar, opcionCambiarAtributo, nuevoValor);
                            Console.WriteLine("\n-Atributo actualizado con éxito-\n");
                        }
                        break;

                    case 4: // Buscar producto
                        ProductoService.BuscarProducto(ListaProductos, false, true);
                        Console.WriteLine();
                        break;

                    case 5: // Listar productos
                        Console.WriteLine();
                        ProductoService.ImprimirProductos(ListaProductos);
                        break;
                }
            }
        }
    }
}