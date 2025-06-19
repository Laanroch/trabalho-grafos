using Grafos.Interfaces;
using Grafos.Models;

namespace Grafos.Algoritmos
{
    public class HelperDijkstra
    {
        public Aresta? Aresta { get; set; }
        public decimal Distancia { get; set; }
    }

    public static class Dijkstra
    {
        public static void ExecutarDijkstra(this IGrafo Grafo, int IdInicio)
        {
            var vertices = Grafo.ObterTodosVertices().ToList();
            vertices.ForEach(x => x.ResetCaminhoMinimo());

            var verticeDeInicio = vertices.FirstOrDefault(x => x.Id == IdInicio);

            var verticesSelecionados = new List<Vertice>
                {
                    verticeDeInicio
                };

            verticeDeInicio.DefinirDistanciaCM(0);

            while (verticesSelecionados.Count < vertices.Count)
            {
                var arestasPossiveis = new List<HelperDijkstra>();

                // Primeiro: coletar todas as arestas possíveis
                foreach (var vertice in verticesSelecionados)
                {
                    foreach (var aresta in Grafo.ObterArestasAdjacentes(vertice.Id))
                    {
                        // Se destino está fora do conjunto de S
                        if (aresta.Origem != null && !verticesSelecionados.Contains(aresta.Destino))
                        {
                            arestasPossiveis.Add(new HelperDijkstra
                            {
                                Aresta = aresta,
                                Distancia = aresta.Origem.ObterDistanciaCM() + aresta.Peso
                            });

                        }
                    }
                }

                //Pega o menor caminho
                var menorCaminho = arestasPossiveis.MinBy(x => x.Distancia);

                if (menorCaminho == null)
                    break;

                menorCaminho.Aresta.Destino
                    .DefinirPaiCM(menorCaminho.Aresta.Origem)
                    .DefinirDistanciaCM(menorCaminho.Distancia);
                verticesSelecionados.Add(menorCaminho.Aresta.Destino);
            }
        }

        public static void ImprimirTabelaCaminhoMinimo(this IGrafo Grafo)
        {
            Console.WriteLine("Tabela de Distâncias e Predecessores:");
            Console.WriteLine("Vértice\t\tDistância\tPredecessor");

            foreach (var vertice in Grafo.ObterTodosVertices())
            {
                var CaminhoMinimo = vertice.CaminhoMinimo;
                var distancia = CaminhoMinimo.Distancia == int.MaxValue ? "∞" : CaminhoMinimo.Distancia.ToString();
                var predecessor = CaminhoMinimo.Pai?.Id.ToString() ?? "-";

                Console.WriteLine($"{vertice.Id}\t\t{distancia}\t\t{predecessor}");
            }
        }
    }
}
