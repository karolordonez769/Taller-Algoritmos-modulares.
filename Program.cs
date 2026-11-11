using System;
internal class Program
{
    // declarar vectores para cada uno de los sistemas --- ips, cafeteria, academico
    static string[] ipsDocumentos = new string[30];
    static string[] ipsNombres = new string[30];
    static int[] ipsEdades = new int[30];
    static int[] ipsTipos = new int[30];
    static int[] ipsPrioridades = new int[30];
    static string[] ipsAtenciones = { "Urgencias", "Consulta general", "Prioritaria" };
    static int ipsCantidad = 0;

    static string[] invCodigos = new string[20];
    static string[] invNombres = new string[20];
    static double[] invPrecios = new double[20];
    static int[] invExistencias = new int[20];
    static int[] invCategorias = new int[20];
    static string[] invNombresCategoria = { "Bebidas", "Panaderia", "Snacks" };
    static double invTotalVendido = 0;
    static int invCantidad = 0;

    static string[] acaCodigos = new string[25];
    static string[] acaNombres = new string[25];
    static double[,] acaNotas = new double[25, 3];  
    static double[] acaAsistencias = new double[25];
    static int acaCantidad = 0;

    static void Main()
    {
        int opcion = 0;

        while (opcion != 4)
        {
            Console.WriteLine("\n===== MENU PRINCIPAL =====\n1. Sistema de turnos de una IPS\n2. Inventario de cafeteria\n3. Control academico\n4. Salir");
            opcion = LeerEntero("Seleccione una opcion: ", 1, 4);

            if (opcion == 1) MenuIPS();
            else if (opcion == 2) MenuInventario();
            else if (opcion == 3) MenuAcademico();
            else Console.WriteLine("Gracias por utilizar el programa.");
        }
    }
    // SISTEMA DE TURNOS DE UNA IPS
    static void MenuIPS()
    {
        int opcion = 0;

        while (opcion != 6)
        {
            Console.WriteLine("\n===== SISTEMA IPS =====\n1. Registrar paciente\n2. Mostrar pacientes\n3. Buscar paciente\n4. Estadisticas de atencion\n5. Paciente con mayor prioridad\n6. Volver al menu principal");
            opcion = LeerEntero("Seleccione: ", 1, 6);

            if (opcion == 1) RegistrarPaciente();
            else if (opcion == 2) MostrarPacientes();
            else if (opcion == 3) BuscarPaciente();
            else if (opcion == 4) EstadisticasAtencion();
            else if (opcion == 5) MostrarMayorPrioridad();
        }
    }


    static void RegistrarPaciente()
    {
        if (ipsCantidad == 30)
        {
            Console.WriteLine("No hay cupos disponibles.");
            return;
        }

        string documento = LeerTexto("Documento: ");

        if (Buscar(ipsDocumentos, documento, ipsCantidad) != -1)
        {
            Console.WriteLine("Ese documento ya esta registrado.");
            return;
        }

        ipsDocumentos[ipsCantidad] = documento;
        ipsNombres[ipsCantidad] = LeerTexto("Nombre completo: ");
        ipsEdades[ipsCantidad] = LeerEntero("Edad: ", 1, 100);
        Console.WriteLine("Tipo de atencion: 1. Urgencias  2. Consulta general  3. Prioritaria");
        ipsTipos[ipsCantidad] = LeerEntero("Seleccione: ", 1, 3);
        ipsPrioridades[ipsCantidad] = LeerEntero("Prioridad (1 a 5): ", 1, 5);
        ipsCantidad++;

        Console.WriteLine("Paciente registrado. Cupos disponibles: " + (30 - ipsCantidad));
    }

    static void MostrarPaciente(int i)
    {
        Console.WriteLine("Documento: " + ipsDocumentos[i]);
        Console.WriteLine("Nombre: " + ipsNombres[i]);
        Console.WriteLine("Edad: " + ipsEdades[i]);
        Console.WriteLine("Atencion: " + ipsAtenciones[ipsTipos[i] - 1]);
        Console.WriteLine("Prioridad: " + ipsPrioridades[i]);
    }

    static void MostrarPacientes()
    {
        if (ipsCantidad == 0)
            Console.WriteLine("No hay pacientes registrados.");

        for (int i = 0; i < ipsCantidad; i++)
        {
            Console.WriteLine("\nPaciente " + (i + 1));
            MostrarPaciente(i);
        }
    }

    static void BuscarPaciente()
    {
        int p = Buscar(ipsDocumentos, LeerTexto("Documento que desea buscar: "), ipsCantidad);

        if (p == -1)
            Console.WriteLine("Paciente no encontrado.");
        else
            MostrarPaciente(p);
    }

    static int ContarPorTipo(int tipo)
    {
        int total = 0;

        for (int i = 0; i < ipsCantidad; i++)
            if (ipsTipos[i] == tipo)
                total++;

        return total;
    }

    static void EstadisticasAtencion()
    {
        Console.WriteLine("\n===== ESTADISTICAS =====");

        for (int tipo = 1; tipo <= 3; tipo++)
            Console.WriteLine(ipsAtenciones[tipo - 1] + ": " + ContarPorTipo(tipo) + " paciente(s)");

        Console.WriteLine("Total de pacientes: " + ipsCantidad);
    }

    static int MayorPrioridad()
    {
        int mayor = 0;

        for (int i = 0; i < ipsCantidad; i++)
            if (ipsPrioridades[i] > mayor)
                mayor = ipsPrioridades[i];

        return mayor;
    }

    static void MostrarMayorPrioridad()
    {
        if (ipsCantidad == 0)
        {
            Console.WriteLine("No hay pacientes registrados.");
            return;
        }

        int mayor = MayorPrioridad();
        Console.WriteLine("Paciente(s) con mayor prioridad (" + mayor + "):");

        for (int i = 0; i < ipsCantidad; i++)
            if (ipsPrioridades[i] == mayor)
                Console.WriteLine(ipsNombres[i] + " - Documento: " + ipsDocumentos[i]);
    }

    // CASO 2: INVENTARIO DE CAFETERIA
    static void MenuInventario()
    {
        int opcion = 0;

        while (opcion != 7)
        {
            Console.WriteLine("\n===== INVENTARIO DE CAFETERIA =====\n1. Registrar producto\n2. Realizar venta\n3. Actualizar inventario\n4. Mostrar productos agotados\n5. Total vendido del dia\n6. Mostrar productos\n7. Volver al menu principal");
            opcion = LeerEntero("Seleccione: ", 1, 7);

            if (opcion == 1) RegistrarProducto();
            else if (opcion == 2) RealizarVenta();
            else if (opcion == 3) ActualizarInventario();
            else if (opcion == 4) MostrarAgotados();
            else if (opcion == 5) MostrarTotalVendido();
            else if (opcion == 6) MostrarProductos();
        }
    }

    static void RegistrarProducto()
    {
        if (invCantidad == 20)
        {
            Console.WriteLine("El inventario esta lleno.");
            return;
        }

        string codigo = LeerTexto("Codigo: ");

        if (Buscar(invCodigos, codigo, invCantidad) != -1)
        {
            Console.WriteLine("Ese codigo ya existe.");
            return;
        }

        invCodigos[invCantidad] = codigo;
        invNombres[invCantidad] = LeerTexto("Nombre: ");
        invPrecios[invCantidad] = LeerDecimal("Precio unitario: ", 0.01, 1000000000);
        invExistencias[invCantidad] = LeerEntero("Cantidad disponible: ", 0, 1000000);
        Console.WriteLine("Categoria: 1. Bebidas  2. Panaderia  3. Snacks");
        invCategorias[invCantidad] = LeerEntero("Seleccione: ", 1, 3);
        invCantidad++;

        Console.WriteLine("Producto registrado.");
    }

    static bool HayStock(int p, int unidades)
    {
        if (unidades <= invExistencias[p])
            return true;

        return false;
    }

    static void RealizarVenta()
    {
        int p = Buscar(invCodigos, LeerTexto("Codigo del producto: "), invCantidad);

        if (p == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        Console.WriteLine("Producto: " + invNombres[p]);
        Console.WriteLine("Existencias: " + invExistencias[p]);

        if (invExistencias[p] == 0)
        {
            Console.WriteLine("Producto agotado. No se puede vender.");
            return;
        }

        int unidades = LeerEntero("Cantidad a vender: ", 1, 1000000);

        if (HayStock(p, unidades) == false)
        {
            Console.WriteLine("No hay suficientes unidades.");
            return;
        }

        double venta = invPrecios[p] * unidades;
        invExistencias[p] = invExistencias[p] - unidades;
        invTotalVendido = invTotalVendido + venta;

        Console.WriteLine("Venta realizada. Total: $" + venta.ToString("F0"));
    }

    static void ActualizarInventario()
    {
        int p = Buscar(invCodigos, LeerTexto("Codigo del producto: "), invCantidad);

        if (p == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        invExistencias[p] = invExistencias[p] + LeerEntero("Unidades que desea agregar: ", 1, 1000000);
        Console.WriteLine("Nuevo inventario: " + invExistencias[p]);
    }

    static void MostrarAgotados()
    {
        bool hayAgotados = false;

        for (int i = 0; i < invCantidad; i++)
            if (invExistencias[i] == 0)
            {
                Console.WriteLine(invNombres[i]);
                hayAgotados = true;
            }

        if (hayAgotados == false)
            Console.WriteLine("No hay productos agotados.");
    }

    static void MostrarTotalVendido()
    {
        Console.WriteLine("Total vendido: $" + invTotalVendido.ToString("F0"));
    }

    static void MostrarProductos()
    {
        if (invCantidad == 0)
            Console.WriteLine("No hay productos registrados.");

        for (int i = 0; i < invCantidad; i++)
        {
            Console.WriteLine("\nCodigo: " + invCodigos[i]);
            Console.WriteLine("Nombre: " + invNombres[i]);
            Console.WriteLine("Precio: $" + invPrecios[i].ToString("F2"));
            Console.WriteLine("Existencias: " + invExistencias[i]);
            Console.WriteLine("Categoria: " + invNombresCategoria[invCategorias[i] - 1]);
        }
    }

    // CASO 3: CONTROL ACADEMICO
    static void MenuAcademico()
    {
        int opcion = 0;

        while (opcion != 6)
        {
            Console.WriteLine("\n===== CONTROL ACADEMICO =====\n1. Registrar estudiante\n2. Mostrar definitivas\n3. Consultar estado de un estudiante\n4. Promedio general del grupo\n5. Mejor y peor promedio\n6. Volver al menu principal");
            opcion = LeerEntero("Seleccione: ", 1, 6);

            if (opcion == 1) RegistrarEstudiante();
            else if (opcion == 2) MostrarEstudiantes();
            else if (opcion == 3) ConsultarEstudiante();
            else if (opcion == 4) MostrarPromedioGrupo();
            else if (opcion == 5) MostrarMejorYPeor();
        }
    }

    static void RegistrarEstudiante()
    {
        if (acaCantidad == 25)
        {
            Console.WriteLine("Se alcanzo el limite de estudiantes.");
            return;
        }

        string codigo = LeerTexto("Codigo del estudiante: ");
        acaCodigos[acaCantidad] = codigo;
        acaNombres[acaCantidad] = LeerTexto("Nombre: ");

        for (int j = 0; j < 3; j++)
            acaNotas[acaCantidad, j] = LeerDecimal("Nota " + (j + 1) + " (0.0 a 5.0): ", 0, 5);

        acaAsistencias[acaCantidad] = LeerDecimal("Porcentaje de asistencia (0 a 100): ", 0, 100);
        acaCantidad++;

        Console.WriteLine("Estudiante registrado.");
    }

    static double CalcularDefinitiva(int i)
    {
        double suma = 0;

        for (int j = 0; j < 3; j++)
            suma = suma + acaNotas[i, j];

        return suma / 3;
    }

    // FUNCION: devuelve el estado segun la nota definitiva
    static string Estado(double nota)
    {
        if (nota >= 3) return "Aprobado";
        if (nota >= 2) return "Habilita";
        return "Reprueba";
    }

    static void MostrarEstudiante(int i)
    {
        double definitiva = CalcularDefinitiva(i);

        Console.WriteLine("Codigo: " + acaCodigos[i]);
        Console.WriteLine("Nombre: " + acaNombres[i]);
        Console.WriteLine("Asistencia: " + acaAsistencias[i] + "%");
        Console.WriteLine("Definitiva: " + definitiva.ToString("F2"));
        Console.WriteLine("Estado: " + Estado(definitiva));
    }

    static void MostrarEstudiantes()
    {
        if (acaCantidad == 0)
            Console.WriteLine("No hay estudiantes registrados.");

        for (int i = 0; i < acaCantidad; i++)
        {
            Console.WriteLine();
            MostrarEstudiante(i);
        }
    }

    static void ConsultarEstudiante()
    {
        int p = Buscar(acaCodigos, LeerTexto("Codigo del estudiante: "), acaCantidad);

        if (p == -1)
            Console.WriteLine("Estudiante no encontrado.");
        else
            MostrarEstudiante(p);
    }

    static double PromedioGrupo()
    {
        double suma = 0;

        for (int i = 0; i < acaCantidad; i++)
            suma = suma + CalcularDefinitiva(i);

        return suma / acaCantidad;
    }

    static void MostrarPromedioGrupo()
    {
        if (acaCantidad == 0)
            Console.WriteLine("No hay estudiantes registrados.");
        else
            Console.WriteLine("Promedio del grupo: " + PromedioGrupo().ToString("F2"));
    }

    static void MostrarMejorYPeor()
    {
        if (acaCantidad == 0)
        {
            Console.WriteLine("No hay estudiantes registrados.");
            return;
        }

        int mejor = 0;   
        int peor = 0;    

        for (int i = 1; i < acaCantidad; i++)
        {
            if (CalcularDefinitiva(i) > CalcularDefinitiva(mejor)) mejor = i;
            if (CalcularDefinitiva(i) < CalcularDefinitiva(peor)) peor = i;
        }

        Console.WriteLine("Mejor promedio: " + acaNombres[mejor] + " - " + CalcularDefinitiva(mejor).ToString("F2"));
        Console.WriteLine("Peor promedio: " + acaNombres[peor] + " - " + CalcularDefinitiva(peor).ToString("F2"));
    }

    // FUNCIONES/METODOS DE AYUDA 

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
            texto = (Console.ReadLine() ?? "").Trim();   
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