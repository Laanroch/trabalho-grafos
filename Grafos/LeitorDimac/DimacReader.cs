using Grafos.Classes.ListaAdjacencia;
using Grafos.Classes.MatrizAdjacencia;
using Grafos.Interfaces;
using Grafos.Models;
using static Grafos.Utils.Utils;

namespace Grafos.LeitorDimac
{
    public class DimacReader
    {
        public static IGrafo LerArquivo(string path)
        {
            var vertices = new List<Vertice>();
            var arestas = new List<Aresta>();

            string[] linhas = File.ReadAllLines(path);
            for (int i = 0; i < linhas.Length; i++)
            {
                if (i == 0)
                {
                    LerPrimeiraLinha(linhas[i], vertices, arestas);
                }
                else
                {
                    LerCabecalho(linhas[i], vertices, arestas);
                }
            }

            var densidade = CalcularDensidade(vertices.Count, arestas.Count);

            if (densidade < 0.5)
            {
                return new GrafoListaAdjacencia().InicializarGrafo(vertices, arestas);
            }
            else
            {
                return new GrafoMatrizAdjacencia().InicializarGrafo(vertices, arestas);
            }
        }

        private static void LerPrimeiraLinha(string linha, List<Vertice> vertices, List<Aresta> arestas)
        {
            var valores = linha.Split(' ');

            if (valores.Length != 2)
                throw new InvalidOperationException("Formato da linha está incorreto");

            var qtdVertices = int.Parse(valores[0]);
            var qtdArestas = int.Parse(valores[1]);

            for (int i = 0; i < qtdVertices; i++)
                vertices.Add(new Vertice());

            for (int i = 0; i < qtdArestas; i++)
                arestas.Add(new Aresta(null, null, 0));
        }

        private static void LerCabecalho(string linha, List<Vertice> vertices, List<Aresta> arestas)
        {
            string[] partes = linha.Split(' ');

            if (partes.Length != 3)
                throw new ArgumentException("Erro ao ler cabeçalho do arquivo.");

            var origem = int.Parse(partes[0]);

            var destino = int.Parse(partes[1]);

            var peso = int.Parse(partes[2]);

            var verticesDaAresta = vertices.Where(v => v.Id == origem || v.Id == destino).ToList();

            if (verticesDaAresta.Count != 2)
                throw new InvalidOperationException("Erro ao encontrar vértices da aresta."); //Passou um vértice que não existe jumento

            var aresta = arestas.FirstOrDefault(a => a.Origem == null && a.Destino == null);

            if (aresta == null)
                throw new InvalidOperationException("Erro ao encontrar aresta."); //Por acaso, passou mais arestas do que o esperado?

            aresta.Origem = verticesDaAresta.FirstOrDefault(v => v.Id == origem);
            aresta.Destino = verticesDaAresta.FirstOrDefault(v => v.Id == destino);
            aresta.Peso = peso;
        }
    }
}
