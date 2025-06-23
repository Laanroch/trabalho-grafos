using Grafos.Interfaces;
using Grafos.Models;
using System.Collections.Generic;

namespace Grafos.Algoritmos
{
    public static class DijkstraSniper
    {
        public static int CalcularMenorSnipers(InstanciaSnipers instancia, out List<int> outCaminho)
        {
            var grafo = instancia.Grafo;
            int origem = instancia.Origem;
            int destino = instancia.Destino;
            var snipersPorVertice = instancia.SnipersPorVertice;

            int n = grafo.ObterTodosVertices().Count;
            var dist = new int[n + 1];
            var prev = new int[n + 1];
            var visitado = new bool[n + 1];
            for (int i = 0; i <= n; i++)
            {
                dist[i] = int.MaxValue;
                prev[i] = -1;
            }
            dist[origem] = snipersPorVertice.ContainsKey(origem) ? snipersPorVertice[origem] : 0;

            var pq = new SortedSet<(int, int)>(Comparer<(int, int)>.Create((a, b) => a.Item1 != b.Item1 ? a.Item1 - b.Item1 : a.Item2 - b.Item2));
            pq.Add((dist[origem], origem));

            while (pq.Count > 0)
            {
                var (custoAtual, u) = pq.Min;
                pq.Remove(pq.Min);
                if (visitado[u]) continue;
                visitado[u] = true;
                if (u == destino) break;
                
                foreach (var verticeVizinho in grafo.ObterVizinhanca(u))
                {
                    int v = verticeVizinho.Id;
                    int custoNo = snipersPorVertice.ContainsKey(v) ? snipersPorVertice[v] : 0;
                    if (dist[v] > dist[u] + custoNo)
                    {
                        dist[v] = dist[u] + custoNo;
                        prev[v] = u;
                        pq.Add((dist[v], v));
                    }
                }
            }
            
            outCaminho = new List<int>();
            if (dist[destino] == int.MaxValue)
                return int.MaxValue;
            for (int at = destino; at != -1; at = prev[at])
                outCaminho.Add(at);
            outCaminho.Reverse();
            return dist[destino];
        }
    }
}
