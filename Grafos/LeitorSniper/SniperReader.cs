using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Grafos.Classes.ListaAdjacencia;
using Grafos.Classes.MatrizAdjacencia;
using Grafos.Interfaces;
using Grafos.Models;

namespace Grafos.LeitorSniper
{
    public static class LeitorSniper
    {
        // Calcula uma vez a pasta raiz do projeto (bin/.../netX -> ../../../)
        private static readonly string ProjectRoot = 
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));

        // Se o caminho for absoluto, usa direto; caso contrário, combina com ProjectRoot
        private static string ResolvePath(string path)
        {
            string fullPath = Path.IsPathRooted(path)
                ? path
                : Path.Combine(ProjectRoot, path);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Arquivo não encontrado em: {fullPath}", fullPath);

            return fullPath;
        }

        public static InstanciaSnipers LerArquivo(string relativePath)
        {
            var linhas = File.ReadAllLines(ResolvePath(relativePath));
            int linhaAtual = 0;

            // Cabeçalho: V A B P
            var cabecalho = linhas[linhaAtual++].Split();
            int qtdVertices    = int.Parse(cabecalho[0]);
            int qtdArestas     = int.Parse(cabecalho[1]);
            int balas          = int.Parse(cabecalho[2]);
            double probabilidade = double.Parse(cabecalho[3], 
                System.Globalization.CultureInfo.InvariantCulture);

            // Cria vértices
            var vertices = Enumerable
                .Range(1, qtdVertices)
                .Select(_ => new Vertice())
                .ToList();

            // Lê arestas
            var arestas = new List<Aresta>();
            for (int i = 0; i < qtdArestas; i++)
            {
                var partes = linhas[linhaAtual++].Split();
                int origem  = int.Parse(partes[0]);
                int destino = int.Parse(partes[1]);

                var vOrigem  = vertices.First(v => v.Id == origem);
                var vDestino = vertices.First(v => v.Id == destino);
                arestas.Add(new Aresta(vOrigem, vDestino, 1));
            }

            // Lê snipers
            var snipersPorVertice = new Dictionary<int,int>();
            var snInfo = linhas[linhaAtual++]
                .Split()
                .Select(int.Parse)
                .ToList();
            int qtdSnipers = snInfo[0];
            for (int i = 1; i <= qtdSnipers; i++)
            {
                int vId = snInfo[i];
                if (!snipersPorVertice.ContainsKey(vId))
                    snipersPorVertice[vId] = 0;
                snipersPorVertice[vId]++;
            }

            var od = linhas[linhaAtual++].Split();
            int origemFinal  = int.Parse(od[0]);
            int destinoFinal = int.Parse(od[1]);

            double densidade = (double)qtdArestas / (qtdVertices * (qtdVertices - 1));
            IGrafo grafo = densidade < 0.5
                ? new GrafoListaAdjacencia().InicializarGrafo(vertices, arestas)
                : new GrafoMatrizAdjacencia().InicializarGrafo(vertices, arestas);

            return new InstanciaSnipers
            {
                Grafo           = grafo,
                Origem          = origemFinal,
                Destino         = destinoFinal,
                Balas           = balas,
                Probabilidade   = probabilidade,
                SnipersPorVertice = snipersPorVertice
            };
        }

        public static List<InstanciaSnipers> LerTodasInstancias(string relativePath)
        {
            var linhas = File.ReadAllLines(ResolvePath(relativePath));
            int linhaAtual = 0;
            var instancias = new List<InstanciaSnipers>();

            while (linhaAtual < linhas.Length)
            {
                if (string.IsNullOrWhiteSpace(linhas[linhaAtual]))
                {
                    linhaAtual++;
                    continue;
                }
                
                // Reset ID counter for each instance
                Vertice.ResetarId();
                
                var cab = linhas[linhaAtual++].Split();
                int qtdV  = int.Parse(cab[0]);
                int qtdA  = int.Parse(cab[1]);
                int bal   = int.Parse(cab[2]);
                double prob = double.Parse(cab[3], 
                    System.Globalization.CultureInfo.InvariantCulture);

                var verts = Enumerable
                    .Range(1, qtdV)
                    .Select(_ => new Vertice())
                    .ToList();

                var arestas = new List<Aresta>();
                for (int i = 0; i < qtdA; i++)
                {
                    var p = linhas[linhaAtual++].Split();
                    int o = int.Parse(p[0]), d = int.Parse(p[1]);
                    var vo = verts.First(v => v.Id == o);
                    var vd = verts.First(v => v.Id == d);
                    arestas.Add(new Aresta(vo, vd, 1));
                }

                var snMap = new Dictionary<int,int>();
                var snList = linhas[linhaAtual++]
                    .Split()
                    .Select(int.Parse)
                    .ToList();
                int nSnipers = snList[0];
                for (int i = 1; i <= nSnipers; i++)
                {
                    int vid = snList[i];
                    if (!snMap.ContainsKey(vid))
                        snMap[vid] = 0;
                    snMap[vid]++;
                }

                var od2 = linhas[linhaAtual++].Split();
                int oF = int.Parse(od2[0]), dF = int.Parse(od2[1]);
                
                double dens = (double)qtdA / (qtdV * (qtdV - 1));
                IGrafo g = dens < 0.5
                    ? new GrafoListaAdjacencia().InicializarGrafo(verts, arestas)
                    : new GrafoMatrizAdjacencia().InicializarGrafo(verts, arestas);

                instancias.Add(new InstanciaSnipers
                {
                    Grafo             = g,
                    Origem            = oF,
                    Destino           = dF,
                    Balas             = bal,
                    Probabilidade     = prob,
                    SnipersPorVertice = snMap
                });
            }

            return instancias;
        }
    }
}
