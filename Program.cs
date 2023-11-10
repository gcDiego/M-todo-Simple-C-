namespace simplx;

class Program
{
    static void Main(string[] args)
    {
        int columnas = 1;//De este dato van a depender las columnas de la matriz
        int desicion;
        int filas = 1;//De este dato van a depender las filas de la matriz
        int restriccion;
        int clmnSol;
        bool finalizado;
        char opcion;

        float[,] sistemaEc;
        string[,] matriz;
        float valor;
        int columPivote;

        Console.WriteLine("¿Cuántas variables de desición hay?");
        desicion = int.Parse(Console.ReadLine());
        clmnSol = desicion + 2;
        columnas += desicion;
        Console.WriteLine("¿Cuántas restriciones hay?");
        restriccion = int.Parse(Console.ReadLine());
        filas += restriccion;
        columnas += filas + 1;
        filas++;
        Console.WriteLine("¿Maximizarás (M) ó Minimizarás (m) tu función objeto?");
        opcion = char.Parse(Console.ReadLine());


        Ecuacion e = new Ecuacion();
        Pivote p = new Pivote();
        GaussJordan g = new GaussJordan();
        
        
        matriz = e.CrearMatriz(desicion,filas, columnas);
        sistemaEc = e.EstablecerMatriz(desicion, filas, columnas, matriz, opcion);
        finalizado = e.Solucion(filas, clmnSol, restriccion, sistemaEc);
        Console.Clear();

        
        
        //Repetir este proceso hasta que exista la matriz unidad
        while (finalizado == false)
        {
            valor = p.BuscarValor(sistemaEc, opcion, desicion);
            columPivote = p.DefinirColumnaPivote(valor, sistemaEc);
            p.DefinirElementoPivote(columPivote, sistemaEc, matriz);
            g.GaussJ(columPivote, sistemaEc, matriz);
            finalizado = e.Solucion(filas, clmnSol, restriccion, sistemaEc);
        }
        e.MostrarSolucion(desicion, sistemaEc, matriz);
        Console.ReadKey();
    }
}

