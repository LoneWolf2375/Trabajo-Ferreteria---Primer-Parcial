using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Administracion
{
    class Program
    {
        static void Main(string[] strings)
        {
            bool exitMainLoop = false;
            while (!exitMainLoop)
            {
                Console.WriteLine("Hello! Choose one of the following options:");
                Console.WriteLine("1. Anadir producto");
                Console.WriteLine("2. Agregar Stock");
                Console.WriteLine("3. Cambiar atributos");
                Console.WriteLine("4. Buscar producto");
                Console.WriteLine("5. Listar productos");

                int choice = ProductoMethods.ValidarEntero(0, 6, "Debe ser uno de los opciones disponibles", "");

                switch (choice)
                {
                    case 0: // Salir del programa
                        exitMainLoop = true;

                        break; // Salir del programa

                    case 1: // Anadir un producto
                        string nombre = ProductoMethods.GetName(true);

                        int precio = ProductoMethods.ValidarEntero(1000, 999999999, "Debe ser un entero mayor o igual a 1.000 Gs", "Precio");

                        bool disponible = ProductoMethods.GetAvailability(false);

                        bool exitSaveLoop = false;

                        while (!exitSaveLoop)
                        {
                            Console.WriteLine(System.Environment.NewLine + "Los datos ingresados son:");
                            Console.WriteLine(" Nombre: " + nombre);
                            Console.WriteLine(" Precio: " + precio);
                            Console.WriteLine(" Disponible: " + disponible);

                            Console.WriteLine(System.Environment.NewLine + "Por favor, confirmar para guardar: ");
                            bool confirmacionGuardar = ProductoMethods.ValidarConfirmacion();

                            if (confirmacionGuardar)
                            {
                                Producto newProducto = ProductoMethods.GuardarProducto(nombre, precio, disponible);

                                using (var db = new ProductoContext())
                                {
                                    db.Productos.Add(newProducto);
                                    db.SaveChanges();
                                }

                                Console.WriteLine(System.Environment.NewLine + "-Producto guardado con exito-" + System.Environment.NewLine);

                                exitSaveLoop = true;

                            }
                            else
                            {
                                if (!confirmacionGuardar)
                                {
                                    Console.WriteLine(System.Environment.NewLine + "Elegir una opcion:");
                                    Console.WriteLine(" 1. Cambiar valor");
                                    Console.WriteLine(" 2. Cancelar");

                                    int opcionNoGuardar = ProductoMethods.ValidarEntero(1, 2, "Debe ser uno de los opciones disponibles", "");

                                    switch (opcionNoGuardar)
                                    {
                                        case 1:
                                            Console.WriteLine(System.Environment.NewLine + "Elegir un valor a cambiar:");
                                            Console.WriteLine("1. Nombre");
                                            Console.WriteLine("2. Precio");
                                            Console.WriteLine("3. Disponibilidad");

                                            int opcionCambiar = ProductoMethods.ValidarEntero(1, 3, "Debe ser uno de los opciones disponibles", "");

                                            switch (opcionCambiar)
                                            {
                                                case 1:
                                                    nombre = (string)ProductoMethods.ModificarAtributo(nombre, precio, disponible, 0, opcionCambiar);
                                                    break;
                                                case 2:
                                                    precio = (int)ProductoMethods.ModificarAtributo(nombre, precio, disponible, 0, opcionCambiar);
                                                    break;
                                                case 3:
                                                    disponible = (bool)ProductoMethods.ModificarAtributo(nombre, precio, disponible, 0, opcionCambiar);
                                                    break;
                                            }

                                            break;

                                        case 2:
                                            Console.WriteLine(System.Environment.NewLine + "-Guardado cancelado-" + System.Environment.NewLine);

                                            exitSaveLoop = true;
                                            break;
                                    }
                                }
                            }
                        }

                        break; // Anadir un producto

                    case 2: // Agregar stock
                        var find = ProductoMethods.BuscarProducto(true, true);

                        if (find != null)
                        {
                            Console.WriteLine();
                            int nuevoStock = ProductoMethods.ValidarEntero(0, 999999999, "Debe ser un entero positivo", "Cantidad a agregar");

                            find.CantStock += nuevoStock;

                            Console.WriteLine(System.Environment.NewLine + $"-Stock actualizado con exito: {find.CantStock}-" + System.Environment.NewLine);

                            if (find.CantStock > 0 & !find.Disponible)
                            {
                                Console.WriteLine("El producto no estaba disponible, desea cambiar su estado a disponible?");
                                bool cambiarEstado = ProductoMethods.ValidarConfirmacion();

                                if (cambiarEstado)
                                {
                                    find.Disponible = true;
                                    Console.WriteLine(System.Environment.NewLine + "-El producto ahora esta disponible-" + System.Environment.NewLine);
                                }
                                else
                                {
                                    Console.WriteLine(System.Environment.NewLine + "-El producto se queda indisponible-" + System.Environment.NewLine);
                                }
                            }
                        }

                        break;// Agregar stock

                    case 3: // Cambiar atributos
                        var findModificar = ProductoMethods.BuscarProducto(true);

                        if (findModificar != null)
                        {
                            Console.WriteLine(System.Environment.NewLine + "Elegir un atributo a cambiar:");
                            Console.WriteLine(" 1. Nombre");
                            Console.WriteLine(" 2. Precio");
                            Console.WriteLine(" 3. Disponibilidad");
                            Console.WriteLine(" 4. Stock");

                            int opcionCambiarAtributo = ProductoMethods.ValidarEntero(1, 4, "Debe ser uno de los opciones disponibles", "");
                            string nuevoValor = "";

                            switch (opcionCambiarAtributo)
                            {
                                case 1: // Cambiar nombre
                                    nuevoValor = (string)ProductoMethods.ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo);
                                    break;
                                case 2: // Cambiar precio
                                    nuevoValor = ProductoMethods.ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo).ToString();
                                    break;
                                case 3: // Cambiar disponibilidad
                                    nuevoValor = ProductoMethods.ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo).ToString();
                                    break;
                                case 4: // Cambiar stock
                                    nuevoValor = ProductoMethods.ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo).ToString();
                                    break;
                            }

                            ProductoMethods.ModificarProducto(findModificar, opcionCambiarAtributo, nuevoValor);
                            Console.WriteLine(System.Environment.NewLine + "-Atributo actualizado con exito-" + System.Environment.NewLine);
                        }
                        break;// Cambiar atributos

                    case 4: // Buscar producto
                        ProductoMethods.BuscarProducto(false, true);
                        Console.WriteLine();
                        break; // Buscar producto

                    case 5: // Listar productos
                        Console.WriteLine();
                        using (var db = new ProductoContext())
                        {
                            var productos = db.Productos.ToList();
                            ProductoMethods.ImprimirProductos(productos);
                        }

                        break; // Listar productos
                }
            }
        }
    }
}