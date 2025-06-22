using Grafos.Classes.ListaAdjacencia;
using Grafos.Classes.MatrizAdjacencia;
using Grafos.Interfaces;
using Grafos.Models;
using System.Collections.Generic;
using System.Linq;

namespace Grafos.LeitorSniper
{
    public class LeitorSniper
    {
        public static InstanciaSnipers LerArquivo(string path)
        {
            var linhas = File.ReadAllLines(path);
            int linhaAtual = 0;

            string[] cabecalho = linhas[linhaAtual++].Split();
            int qtdVertices = int.Parse(cabecalho[0]);
            int qtdArestas = int.Parse(cabecalho[1]);
            int balas = int.Parse(cabecalho[2]);
            double probabilidade = double.Parse(cabecalho[3], System.Globalization.CultureInfo.InvariantCulture);

            var vertices = new List<Vertice>();
            for (int i = 1; i <= qtdVertices; i++)
                vertices.Add(new Vertice());

            var arestas = new List<Aresta>();
            for (int i = 0; i < qtdArestas; i++)
            {
                var partes = linhas[linhaAtual++].Split();
                int origem = int.Parse(partes[0]);
                int destino = int.Parse(partes[1]);

                var vOrigem = vertices.First(v => v.Id == origem);
                var vDestino = vertices.First(v => v.Id == destino);

                arestas.Add(new Aresta(vOrigem, vDestino, 1)); // peso = 1
            }

            var snipersPorVertice = new Dictionary<int, int>();
            var snipersInfo = linhas[linhaAtual++].Split().Select(int.Parse).ToList();
            int qtdSnipers = snipersInfo[0];

            for (int i = 1; i <= qtdSnipers; i++)
            {
                int verticeComSniper = snipersInfo[i];
                if (!snipersPorVertice.ContainsKey(verticeComSniper))
                    snipersPorVertice[verticeComSniper] = 0;
                snipersPorVertice[verticeComSniper]++;
            }

            var origemDestino = linhas[linhaAtual++].Split();
            int origemFinal = int.Parse(origemDestino[0]);
            int destinoFinal = int.Parse(origemDestino[1]);

            double densidade = (double)qtdArestas / (qtdVertices * (qtdVertices - 1));
            IGrafo grafo;
            if (densidade < 0.5)
                grafo = new GrafoListaAdjacencia().InicializarGrafo(vertices, arestas);
            else
                grafo = new GrafoMatrizAdjacencia().InicializarGrafo(vertices, arestas);

            return new InstanciaSnipers
            {
                Grafo = grafo,
                Origem = origemFinal,
                Destino = destinoFinal,
                Balas = balas,
                Probabilidade = probabilidade,
                SnipersPorVertice = snipersPorVertice
            };
        }

        public static List<InstanciaSnipers> LerTodasInstancias(string path)
        {
            var linhas = File.ReadAllLines(path);
            int linhaAtual = 0;
            var instancias = new List<InstanciaSnipers>();
            while (linhaAtual < linhas.Length)
            {
                if (string.IsNullOrWhiteSpace(linhas[linhaAtual])) { linhaAtual++; continue; }
                string[] cabecalho = linhas[linhaAtual++].Split();
                int qtdVertices = int.Parse(cabecalho[0]);
                int qtdArestas = int.Parse(cabecalho[1]);
                int balas = int.Parse(cabecalho[2]);
                double probabilidade = double.Parse(cabecalho[3], System.Globalization.CultureInfo.InvariantCulture);

                var vertices = new List<Vertice>();
                for (int i = 1; i <= qtdVertices; i++)
                    vertices.Add(new Vertice());

                var arestas = new List<Aresta>();
                for (int i = 0; i < qtdArestas; i++)
                {
                    var partes = linhas[linhaAtual++].Split();
                    int origem = int.Parse(partes[0]);
                    int destino = int.Parse(partes[1]);
                    var vOrigem = vertices.First(v => v.Id == origem);
                    var vDestino = vertices.First(v => v.Id == destino);
                    arestas.Add(new Aresta(vOrigem, vDestino, 1));
                }

                var snipersPorVertice = new Dictionary<int, int>();
                var snipersInfo = linhas[linhaAtual++].Split().Select(int.Parse).ToList();
                int qtdSnipers = snipersInfo[0];
                for (int i = 1; i <= qtdSnipers; i++)
                {
                    int verticeComSniper = snipersInfo[i];
                    if (!snipersPorVertice.ContainsKey(verticeComSniper))
                        snipersPorVertice[verticeComSniper] = 0;
                    snipersPorVertice[verticeComSniper]++;
                }

                var origemDestino = linhas[linhaAtual++].Split();
                int origemFinal = int.Parse(origemDestino[0]);
                int destinoFinal = int.Parse(origemDestino[1]);

                double densidade = (double)qtdArestas / (qtdVertices * (qtdVertices - 1));
                IGrafo grafo;
                if (densidade < 0.5)
                    grafo = new GrafoListaAdjacencia().InicializarGrafo(vertices, arestas);
                else
                    grafo = new GrafoMatrizAdjacencia().InicializarGrafo(vertices, arestas);

                instancias.Add(new InstanciaSnipers
                {
                    Grafo = grafo,
                    Origem = origemFinal,
                    Destino = destinoFinal,
                    Balas = balas,
                    Probabilidade = probabilidade,
                    SnipersPorVertice = snipersPorVertice
                });
            }
            return instancias;
        }
    }
}