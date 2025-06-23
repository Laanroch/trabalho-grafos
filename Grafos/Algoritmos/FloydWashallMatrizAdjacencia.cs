using Grafos.Interfaces;
using System.Text;

namespace Grafos.Classes.MatrizAdjacencia
{
    public partial class GrafoMatrizAdjacencia : IMatrizAdjacencia
    {
        public int[,] ExecutarFloydWarshall()
        {
            var dist = new int[Vertices.Count + 1, Vertices.Count + 1];

            for (int i = 1; i <= Vertices.Count; i++)
            {
                for (int j = 1; j <= Vertices.Count; j++)
                {
                    dist[i, j] = i == j ? 0 : int.MaxValue / 200; //Esse trem quebra se tirar o 200
                }
            }

            for (int i = 1; i <= Vertices.Count; i++)
            {
                for (int j = 1; j <= Vertices.Count; j++)
                {
                    if (MatrizDeAdjacencia[i, j] != null) { dist[i, j] = MatrizDeAdjacencia[i, j].Peso; }
                }
            }

            for (int k = 1; k <= Vertices.Count; k++)
            {
                for (int i = 1; i <= Vertices.Count; i++)
                {
                    for (int j = 1; j <= Vertices.Count; j++)
                    {
                        if (dist[i, j] > dist[i, k] + dist[k, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }

            return dist;
        }

        public void GerarTabelaDistancias(int[,] distancias)
        {
            var sb = new StringBuilder();
            int numVertices = distancias.GetLength(0) - 1;

            int maxLength = 3;
            for (int i = 1; i <= numVertices; i++)
            {
                for (int j = 1; j <= numVertices; j++)
                {
                    int length = distancias[i, j] == int.MaxValue / 200 ? "∞".Length : distancias[i, j].ToString().Length;
                    maxLength = Math.Max(maxLength, length);
                }
            }

            sb.Append("   ");
            for (int i = 1; i <= numVertices; i++)
            {
                sb.Append($"{i,3}");
            }
            sb.AppendLine();

            sb.Append("   ");
            sb.AppendLine(new string('-', maxLength * numVertices));

            for (int i = 1; i <= numVertices; i++)
            {
                sb.Append($"{i,2}|");
                for (int j = 1; j <= numVertices; j++)
                {
                    string valor = distancias[i, j] == int.MaxValue ? "∞" : distancias[i, j].ToString();
                    sb.Append($"{valor,3}");
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
