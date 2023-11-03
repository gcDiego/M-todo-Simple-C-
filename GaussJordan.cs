using System;
namespace simplx
{
	public class GaussJordan
	{
		Pivote p = new Pivote();
		Ecuacion e = new Ecuacion();
		public void GaussJ(int columPivote, float[,] sistemaEc, string[,]matriz)
		{
            float min;
            int fila;
			float []d;
            min = p.resultdoMasChico(columPivote, sistemaEc);
            fila = p.definirFila(columPivote, min, sistemaEc);
			d = valores(columPivote, fila, sistemaEc);
			operacion(fila, d, sistemaEc, matriz);
			Console.WriteLine("Matriz después de las operaciones");
            Console.WriteLine();
            e.ImprimirMatriz(matriz);
		}

		public float[] valores(int columPivote, int fila, float [,]sistemaEc)
		{
			float[] d = new float[sistemaEc.GetLength(0)];
			for(int i = 1; i < sistemaEc.GetLength(0); i++)
			{
				d[i] = sistemaEc[i, columPivote];
			}

			return d;
		}

		public float[,] operacion(int fila, float[] elemColPivote, float[,] sistemaEc, string[,] matriz)
		{
			float a;
			Fracciones f = new Fracciones();
			for(int i = 0; i < elemColPivote.Length; i++)
			{
                a = elemColPivote[i];
                if (i != fila && a != 0)
				{
					if (a < 0)
						{
                            Console.WriteLine($"OPERACION A REALIZAR: R{i-1} + {-1*a}(R{fila-1})");
                        }
						else
						{
                            Console.WriteLine($"OPERACION A REALIZAR: R{i-1} - {a}(R{fila-1})");
                        }

                    for (int j = 1; j < sistemaEc.GetLength(1); j++)
                    {
                        float operacion = (sistemaEc[i, j] - a * sistemaEc[fila, j]);
                        sistemaEc[i, j] = operacion;
                        matriz[i, j] = $"[ {operacion} ]";
                        /*if (operacion % 1 == 0)
						{
							int[] fraccion = f.DecimalaFraccion(operacion);
							matriz[i, j] = $"[{fraccion[0]}/{fraccion[1]}";
						}
						else
						{
							matriz[i, j] = $"[{operacion}]";
						}*/
                        
                    }
                }
			}
			return sistemaEc;
		}
	}
}

