using System;

internal class Program
{
    static void Main()
    {
        int opcion = 0;

        while (opcion != 4)
        {
            Console.WriteLine("\n===== MENU PRINCIPAL =====\n1. Sistema de turnos de una IPS\n2. Inventario de cafeteria\n3. Control academico\n4. Salir");
            opcion = LeerEntero("Seleccione una opcion: ", 1, 4);

            if (opcion == 1) SistemaIPS();
            else if (opcion == 2) SistemaInventario();
            else if (opcion == 3) SistemaAcademico();
            else Console.WriteLine("Gracias por utilizar el programa.");
        }
    }

    // SISTEMA DE TURNOS DE UNA IPS
    static void SistemaIPS()
    {
        string[] documentos = new string[30];
        string[] nombres = new string[30];
        int[] edades = new int[30];
        int[] tipos = new int[30];
        int[] prioridades = new int[30];
        string[] atenciones = { "Urgencias", "Consulta general", "Prioritaria" };
        int cantidad = 0;
        int opcion = 0;

        while (opcion != 6)
        {
            Console.WriteLine("\n===== SISTEMA IPS =====\n1. Registrar paciente\n2. Mostrar pacientes\n3. Buscar paciente\n4. Estadisticas de atencion\n5. Paciente con mayor prioridad\n6. Volver al menu principal");
            opcion = LeerEntero("Seleccione: ", 1, 6);

            if (opcion == 1)
            {
                if (cantidad == 30)
                    Console.WriteLine("No hay cupos disponibles.");
                else
                {
                    string documento = LeerTexto("Documento: ");

                    if (Buscar(documentos, documento, cantidad) != -1)
                        Console.WriteLine("Ese documento ya esta registrado.");
                    else
                    {
                        documentos[cantidad] = documento;
                        nombres[cantidad] = LeerTexto("Nombre completo: ");
                        edades[cantidad] = LeerEntero("Edad: ", 1, 100);
                        Console.WriteLine("Tipo de atencion: 1. Urgencias  2. Consulta general  3. Prioritaria");
                        tipos[cantidad] = LeerEntero("Seleccione: ", 1, 3);
                        prioridades[cantidad] = LeerEntero("Prioridad (1 a 5): ", 1, 5);
                        cantidad++;
                        Console.WriteLine("Paciente registrado.");
                    }
                }
            }
            else if (opcion == 2)
            {
                if (cantidad == 0)
                    Console.WriteLine("No hay pacientes registrados.");

                for (int i = 0; i < cantidad; i++)
                {
                    Console.WriteLine("\nPaciente " + (i + 1));
                    Console.WriteLine("Documento: " + documentos[i]);
                    Console.WriteLine("Nombre: " + nombres[i]);
                    Console.WriteLine("Edad: " + edades[i]);
                    Console.WriteLine("Atencion: " + atenciones[tipos[i] - 1]);
                    Console.WriteLine("Prioridad: " + prioridades[i]);
                }
            }
            else if (opcion == 3)
            {
                int p = Buscar(documentos, LeerTexto("Documento que desea buscar: "), cantidad);

                if (p == -1)
                    Console.WriteLine("Paciente no encontrado.");
                else
                {
                    Console.WriteLine("Nombre: " + nombres[p]);
                    Console.WriteLine("Edad: " + edades[p]);
                    Console.WriteLine("Atencion: " + atenciones[tipos[p] - 1]);
                    Console.WriteLine("Prioridad: " + prioridades[p]);
                }
            }
            else if (opcion == 4)
            {
                int[] conteo = new int[3];   

                for (int i = 0; i < cantidad; i++)
                    conteo[tipos[i] - 1]++;

                Console.WriteLine("\n===== ESTADISTICAS =====");
                for (int i = 0; i < 3; i++)
                    Console.WriteLine(atenciones[i] + ": " + conteo[i] + " paciente(s)");
                Console.WriteLine("Total de pacientes: " + cantidad);
            }
            else if (opcion == 5)
            {
                if (cantidad == 0)
                    Console.WriteLine("No hay pacientes registrados.");
                else
                {
                    int mayor = prioridades[0];

                    for (int i = 1; i < cantidad; i++)
                        if (prioridades[i] > mayor)
                            mayor = prioridades[i];

                    Console.WriteLine("Paciente(s) con mayor prioridad (" + mayor + "):");
                    for (int i = 0; i < cantidad; i++)
                        if (prioridades[i] == mayor)
                            Console.WriteLine(nombres[i] + " - Documento: " + documentos[i]);
                }
            }
        }
    }

    //  INVENTARIO DE CAFETERIA 
    static void SistemaInventario()
    {
        string[] codigos = new string[20];
        string[] nombres = new string[20];
        double[] precios = new double[20];
        int[] existencias = new int[20];
        int[] categorias = new int[20];
        string[] nombresCategoria = { "Bebidas", "Panaderia", "Snacks" };
        double totalVendido = 0;
        int cantidad = 0;
        int opcion = 0;

        while (opcion != 7)
        {
            Console.WriteLine("\n===== INVENTARIO DE CAFETERIA =====\n1. Registrar producto\n2. Realizar venta\n3. Actualizar inventario\n4. Mostrar productos agotados\n5. Total vendido del dia\n6. Mostrar productos\n7. Volver al menu principal");
            opcion = LeerEntero("Seleccione: ", 1, 7);

            if (opcion == 1)
            {
                if (cantidad == 20)
                    Console.WriteLine("El inventario esta lleno.");
                else
                {
                    string codigo = LeerTexto("Codigo: ");

                    if (Buscar(codigos, codigo, cantidad) != -1)
                        Console.WriteLine("Ese codigo ya existe.");
                    else
                    {
                        codigos[cantidad] = codigo;
                        nombres[cantidad] = LeerTexto("Nombre: ");
                        precios[cantidad] = LeerDecimal("Precio unitario: ", 0.01, 1000000000);
                        existencias[cantidad] = LeerEntero("Cantidad disponible: ", 0, 1000000);
                        Console.WriteLine("Categoria: 1. Bebidas  2. Panaderia  3. Snacks");
                        categorias[cantidad] = LeerEntero("Seleccione: ", 1, 3);
                        cantidad++;
                        Console.WriteLine("Producto registrado.");
                    }
                }
            }
            else if (opcion == 2)
            {
                int p = Buscar(codigos, LeerTexto("Codigo del producto: "), cantidad);

                if (p == -1)
                    Console.WriteLine("Producto no encontrado.");
                else
                {
                    Console.WriteLine("Producto: " + nombres[p]);
                    Console.WriteLine("Existencias: " + existencias[p]);
                    int unidades = LeerEntero("Cantidad a vender: ", 1, 1000000);

                    if (unidades > existencias[p])
                        Console.WriteLine("No hay suficientes unidades.");
                    else
                    {
                        double venta = precios[p] * unidades;
                        existencias[p] = existencias[p] - unidades;
                        totalVendido = totalVendido + venta;
                        Console.WriteLine("Venta realizada. Total: $" + venta.ToString("F0"));
                    }
                }
            }
            else if (opcion == 3)
            {
                int p = Buscar(codigos, LeerTexto("Codigo del producto: "), cantidad);

                if (p == -1)
                    Console.WriteLine("Producto no encontrado.");
                else
                {
                    existencias[p] = existencias[p] + LeerEntero("Unidades que desea agregar: ", 1, 1000000);
                    Console.WriteLine("Nuevo inventario: " + existencias[p]);
                }
            }
            else if (opcion == 4)
            {
                bool hayAgotados = false;

                for (int i = 0; i < cantidad; i++)
                    if (existencias[i] == 0)
                    {
                        Console.WriteLine(nombres[i]);
                        hayAgotados = true;
                    }

                if (hayAgotados == false)
                    Console.WriteLine("No hay productos agotados.");
            }
            else if (opcion == 5)
            {
                Console.WriteLine("Total vendido: $" + totalVendido.ToString("F0"));
            }
            else if (opcion == 6)
            {
                if (cantidad == 0)
                    Console.WriteLine("No hay productos registrados.");

                for (int i = 0; i < cantidad; i++)
                {
                    Console.WriteLine("\nCodigo: " + codigos[i]);
                    Console.WriteLine("Nombre: " + nombres[i]);
                    Console.WriteLine("Precio: $" + precios[i].ToString("F2"));
                    Console.WriteLine("Existencias: " + existencias[i]);
                    Console.WriteLine("Categoria: " + nombresCategoria[categorias[i] - 1]);
                }
            }
        }
    }

    //  CONTROL ACADEMICO
    static void SistemaAcademico()
    {
        string[] codigos = new string[25];
        string[] nombres = new string[25];
        double[] definitivas = new double[25];   
        int cantidad = 0;
        int opcion = 0;

        while (opcion != 6)
        {
            Console.WriteLine("\n===== CONTROL ACADEMICO =====\n1. Registrar estudiante\n2. Mostrar definitivas\n3. Consultar estado de un estudiante\n4. Promedio general del grupo\n5. Mejor y peor promedio\n6. Volver al menu principal");
            opcion = LeerEntero("Seleccione: ", 1, 6);

            if (opcion == 1)
            {
                if (cantidad == 25)
                    Console.WriteLine("Se alcanzo el limite de estudiantes.");
                else
                {
                    string codigo = LeerTexto("Codigo del estudiante: ");

                    if (Buscar(codigos, codigo, cantidad) != -1)
                        Console.WriteLine("Ese codigo ya esta registrado.");
                    else
                    {
                        codigos[cantidad] = codigo;
                        nombres[cantidad] = LeerTexto("Nombre: ");
                        double nota1 = LeerDecimal("Nota 1 (0 a 5): ", 0, 5);
                        double nota2 = LeerDecimal("Nota 2 (0 a 5): ", 0, 5);
                        double nota3 = LeerDecimal("Nota 3 (0 a 5): ", 0, 5);
                        definitivas[cantidad] = (nota1 + nota2 + nota3) / 3;
                        LeerDecimal("Asistencia (0 a 100): ", 0, 100);
                        cantidad++;
                        Console.WriteLine("Estudiante registrado.");
                    }
                }
            }
            else if (opcion == 2)
            {
                if (cantidad == 0)
                    Console.WriteLine("No hay estudiantes registrados.");

                for (int i = 0; i < cantidad; i++)
                {
                    Console.WriteLine("\nCodigo: " + codigos[i]);
                    Console.WriteLine("Nombre: " + nombres[i]);
                    Console.WriteLine("Definitiva: " + definitivas[i].ToString("F2"));
                    Console.WriteLine("Estado: " + Estado(definitivas[i]));
                }
            }
            else if (opcion == 3)
            {
                int p = Buscar(codigos, LeerTexto("Codigo del estudiante: "), cantidad);

                if (p == -1)
                    Console.WriteLine("Estudiante no encontrado.");
                else
                {
                    Console.WriteLine("Nombre: " + nombres[p]);
                    Console.WriteLine("Definitiva: " + definitivas[p].ToString("F2"));
                    Console.WriteLine("Estado: " + Estado(definitivas[p]));
                }
            }
            else if (opcion == 4)
            {
                if (cantidad == 0)
                    Console.WriteLine("No hay estudiantes registrados.");
                else
                {
                    double suma = 0;

                    for (int i = 0; i < cantidad; i++)
                        suma = suma + definitivas[i];

                    Console.WriteLine("Promedio del grupo: " + (suma / cantidad).ToString("F2"));
                }
            }
            else if (opcion == 5)
            {
                if (cantidad == 0)
                    Console.WriteLine("No hay estudiantes registrados.");
                else
                {
                    int mejor = 0;   
                    int peor = 0;    

                    for (int i = 1; i < cantidad; i++)
                    {
                        if (definitivas[i] > definitivas[mejor]) mejor = i;
                        if (definitivas[i] < definitivas[peor]) peor = i;
                    }

                    Console.WriteLine("Mejor promedio: " + nombres[mejor] + " - " + definitivas[mejor].ToString("F2"));
                    Console.WriteLine("Peor promedio: " + nombres[peor] + " - " + definitivas[peor].ToString("F2"));
                }
            }
        }
    }

    static string Estado(double nota)
    {
        if (nota >= 3) return "Aprobado";
        if (nota >= 2) return "Habilita";
        return "Reprueba";
    }

    // METODOS 
    static int Buscar(string[] lista, string valor, int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
            if (lista[i] == valor)
                return i;

        return -1;
    }

    static string LeerTexto(string mensaje)
    {
        string texto = "";

        while (texto == "")
        {
            Console.Write(mensaje);
            texto = Console.ReadLine().Trim();
        }

        return texto;
    }

    static int LeerEntero(string mensaje, int min, int max)
    {
        int numero;

        Console.Write(mensaje);
        while (int.TryParse(Console.ReadLine(), out numero) == false || numero < min || numero > max)
            Console.Write("Valor invalido. " + mensaje);

        return numero;
    }

    static double LeerDecimal(string mensaje, double min, double max)
    {
        double numero;

        Console.Write(mensaje);
        while (double.TryParse(Console.ReadLine(), out numero) == false || numero < min || numero > max)
            Console.Write("Valor invalido. " + mensaje);

        return numero;
    }
}