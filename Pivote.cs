using System;
namespace simplx
{
	public class Pivote
	{
		public float BuscarValor(float[,] sistemaEc, char opcion, int desicion)
		{
            float valor = 0;
            float valX = sistemaEc[1, 1];
            float varY = sistemaEc[1, 2];
            String coordinate = "X";
            switch (opcion){
                    case 'M':
                        foreach (float number in sistemaEc)
                        {
                            if (coordinate == "X")//Encontrar el valor más chico de la coordenada X
                            {
                                if (number < valX)
                                valX = number;

                                coordinate = "Y";
                            }
                            else if (coordinate == "Y")//Encontrar el valor más chico de la coordenada Y
                            {
                                if (number < varY)
                                varY = number;

                                coordinate = "X";
                            }
                        }
                        if (valX < varY)//Definir cuál es el valor más chico de todo el arreglo
                        {
                            valor = valX;
                        }
                        else
                        {
                            valor = varY;
                        }
                        Console.WriteLine($"El elemento más chico de la matriz es: {valor}");
                    break;

                case 'm':
                    float max = sistemaEc[1,1];
                    for( int f = 1; f<sistemaEc.GetLength(0);f++)
                    {
                        for(int c =0;c<desicion+2;c++)
                        {
                            if(max < sistemaEc[f,c])
                            {
                            max = sistemaEc[f,c];
                            valor = max;
                            }
                        }
                    }
                    Console.WriteLine($"El elemento más grande de la matriz es: {valor}");
                    break;
            }
            return valor;
		}

        public int DefinirColumnaPivote(float valor, float[,]sistemaEc)
        {
            int columnaPivote = 0;
            int contador = 0;
            float b = 0;
            float a = 0;
            for(int i = 1; i < sistemaEc.GetLength(0); i++)
            {
                for(int j = 1; j < sistemaEc.GetLength(1); j++)
                {
                    if (sistemaEc[i, j] == valor)
                    {
                        contador++;
                        if (contador > 1)
                        {
                            Console.WriteLine($"El numero {valor} fue encontrado en más de una ocación...");
                            b = resultdoMasChico(j, sistemaEc);
                            if (b < a)
                            {
                                columnaPivote = j;
                            }
                        }
                        else
                        {
                            columnaPivote = j;//Aún falta considerar que pasa si un mismo valor se encuentra en diferente posición de la misma columna
                            a = resultdoMasChico(j, sistemaEc);
                        }
                    }
                }
            }
            Console.WriteLine($"Por lo tanto, la columna pivote es X{columnaPivote-1}");
            return columnaPivote;
        }

        public float DefinirElementoPivote(int columPivote, float[,]sistemaEc, string[,] matriz)
        {
            float elemPivote = 0;
            float min;
            int fila;
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Definimos cuál será nuestro elemento pivote...");
            min = resultdoMasChico(columPivote, sistemaEc);
            fila = definirFila(columPivote, min, sistemaEc);

            //Encontrar el elemento pivote
            Ecuacion e = new Ecuacion();
            elemPivote = sistemaEc[fila, columPivote];
            if (elemPivote > 1)
            {
                Console.WriteLine($"Entonces, el elemento pivote es: {elemPivote}");
                Console.WriteLine($"Pero, {elemPivote} es mayor que '1' por lo tanto, se divide el elemento pivote entre sí mismo.");
                Console.WriteLine($"Ésto afectará a toda la fila {fila}");
                Console.WriteLine("");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("");
                //Aplicar GaussJordan
                establecerPivote(elemPivote, fila, sistemaEc, matriz);
                Console.WriteLine($"Por lo tanto, el elemento pivote se encuentra en h{fila-1}, x{columPivote-1}");
                Console.WriteLine($"matriz después de la operacion 'R{fila - 1} / {elemPivote}'");
                e.ImprimirMatriz(matriz);
            }
            else
            {
                Console.WriteLine($"La columna pivote es X{columPivote - 1}");
                Console.WriteLine($"Entonces, el elemento pivote es: {elemPivote}");
                e.ImprimirMatriz(matriz);
            }

            return elemPivote;
        }

        public int definirFila(int columnPivote, float min, float[,] sistemaEc)
        {
            int f = 0;
            int sol = sistemaEc.GetLength(1) - 1;
            for (int fila = 1; fila < sistemaEc.GetLength(0); fila++)
            {
                if (sistemaEc[fila, sol] / sistemaEc[fila,columnPivote] == min)
                {
                    f = fila;
                    break;
                }
            }
            return f;
        }

        public float resultdoMasChico(int columPivote, float[,] sistemaEc)
        {
            int sol = sistemaEc.GetLength(1) - 1;
            int filas = sistemaEc.GetLength(0);
            float[] resultados = new float[sistemaEc.GetLength(0)];
            Console.WriteLine("");
            Console.WriteLine("Divir los elementos de la columna pivote entre la columna solución");
            for (int i = 1; i < filas; i++)
            {
                if (sistemaEc[i, columPivote] < 0 || sistemaEc[i, columPivote] > 0)
                {
                    if (sistemaEc[i, sol] == 0)
                    {
                        resultados[i] = 0;
                    }
                    else
                    {
                        float dato = (sistemaEc[i, sol] / sistemaEc[i, columPivote]);
                        Console.WriteLine($"{sistemaEc[i, sol]} / {sistemaEc[i, columPivote]} = {dato}");
                        resultados[i] = dato;//Almacenar el resultado de las divisiones 
                    }
                }
            }
            Console.WriteLine();
            //Definir el resultado más chico del arreglo de resultados
            float min = resultados.Where(x => x > 0).Min();
            Console.WriteLine($"El resultados más chico y mayor que cero de las divisiones es: {min}");

            return min;
        }

        public void establecerPivote(float pivote, int fila, float[,] sistemaEc, string[,] matriz)
        {

            for (int columna = 1; columna < sistemaEc.GetLength(1); columna++)
            {
                if (sistemaEc[fila, columna] % pivote == 0)
                {
                    float resultado = sistemaEc[fila, columna] / pivote;
                    sistemaEc[fila, columna] = resultado;
                    matriz[fila, columna] = $"[ {resultado.ToString()} ]";
                }
                else
                {
                    /*matriz[fila, columna] = $"[{sistemaEc[fila, columna]}/{pivote}]";
                    sistemaEc[fila, columna] = sistemaEc[fila, columna] / pivote;*/
                    float resultado = sistemaEc[fila, columna] / pivote;
                    sistemaEc[fila, columna] = resultado;
                    matriz[fila, columna] = $"[ {resultado.ToString()} ]";
                }
            }
        }
    }
}

