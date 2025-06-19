using Grafos.Interfaces;
using Grafos.Models;
using System.Text;

namespace Grafos.Algoritmos
{
    public static class BuscaEmLargura
    {
        private static int Indice = 0;
        private static IGrafo? Grafo;

        public static IGrafo ExecutarBuscaEmLargura(this IGrafo GrafoEntrada, int IdVertice)
        {
            Indice = 0;
            var fila = new Queue<Vertice>();
            Grafo = GrafoEntrada;

            var vertices = Grafo.ObterTodosVertices().ToList();

            var verticeDeInicio = vertices.FirstOrDefault(x => x.Id == IdVertice);

            vertices.ForEach(x => x.ResetBuscaEmLargura());

            verticeDeInicio?.DefinirIndiceBuscaEmLargura(1);

            while (vertices.Any(v => v.ObterNivelBuscaEmLargura() == 0))
            {
                Busque(verticeDeInicio, fila);
            }
            return Grafo;
        }

        private static void Busque(Vertice vertice, Queue<Vertice> fila)
        {
            Indice++;
            vertice.DefinirNivelBuscaEmLargura(Indice);
            fila.Enqueue(vertice);

            while (fila.Count > 0)
            {
                var verticeAtual = fila.Dequeue();

                foreach (var aresta in Grafo.ObterArestasAdjacentes(verticeAtual.Id))
                {
                    var vizinho = aresta.Destino;

                    if (vizinho != null && vizinho.ObterNivelBuscaEmLargura() == 0)
                    {
                        aresta.Tipo = ClassificacaoAresta.ArestaArvore;
                        vizinho.DefinirPaiBuscaEmLargura(verticeAtual);
                        vizinho.DefinirNivelBuscaEmLargura(verticeAtual.ObterNivelBuscaEmLargura() + 1);

                        Indice++;
                        vizinho.DefinirIndiceBuscaEmLargura(Indice);
                        fila.Enqueue(vizinho);
                    }
                    else if (vizinho?.ObterNivelBuscaEmLargura() == verticeAtual?.ObterNivelBuscaEmLargura() + 1)
                    {
                        aresta.Tipo = ClassificacaoAresta.ArestaTio;
                    }
                    else if (vizinho?.ObterNivelBuscaEmLargura() == verticeAtual?.ObterNivelBuscaEmLargura()
                        && verticeAtual?.ObterPaiBuscaEmLargura().Id == vizinho?.ObterPaiBuscaEmLargura().Id
                        && vizinho?.ObterIndiceBuscaEmLargura() > verticeAtual?.ObterIndiceBuscaEmLargura())
                    {
                        aresta.Tipo = ClassificacaoAresta.ArestaIrmao;
                    }
                    else if (vizinho?.ObterNivelBuscaEmLargura() == verticeAtual?.ObterNivelBuscaEmLargura()
                        && verticeAtual?.ObterPaiBuscaEmLargura().Id != vizinho?.ObterPaiBuscaEmLargura().Id
                        && vizinho?.ObterIndiceBuscaEmLargura() > verticeAtual?.ObterIndiceBuscaEmLargura())
                    {
                        aresta.Tipo = ClassificacaoAresta.ArestaPrimo;
                    }
                }
            }
        }

        public static void GerarTabelaBuscaEmLargura(this IGrafo grafo)
        {
            var vertices = grafo.ObterTodosVertices();

            if (vertices == null || vertices.Count == 0)
            {
                throw new InvalidOperationException("Grafo não possui vértices.");
            }

            var sb = new StringBuilder();
            sb.AppendLine("Resultados da Busca em Largura:");
            sb.AppendLine(new string('-', 60)); // Separador
            sb.AppendLine($"{"Vértice",-10} {"Índice",-10} {"Nível",-10} {"Pai",-10}"); // Cabeçalho
            sb.AppendLine(new string('-', 60)); // Separador

            foreach (var vertice in vertices.OrderBy(v => v.Id)) // Ordenação para melhor visualização
            {
                sb.AppendLine($"{vertice.Id,-10} {vertice.ObterIndiceBuscaEmLargura(),-10} {vertice.ObterNivelBuscaEmLargura(),-10} {(vertice.ObterPaiBuscaEmLargura()?.Id.ToString() ?? "-"),-10} ");
            }

            Console.WriteLine(sb.ToString());
        }

        public static void GerarTabelaBuscaEmLargura(this IGrafo grafo, int nivelMaximo)
        {
            var vertices = grafo.ObterTodosVertices();

            if (vertices == null || vertices.Count == 0)
            {
                throw new InvalidOperationException("Grafo não possui vértices.");
            }

            // Filtra os vértices pelo nível
            var verticesFiltrados = vertices.Where(v => v.ObterNivelBuscaEmLargura() <= nivelMaximo && v.ObterNivelBuscaEmLargura() > 1);

            if (!verticesFiltrados.Any())
            {
                throw new InvalidOperationException($"Não foram encontrados vértices até o nível {nivelMaximo}.");
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Resultados da Busca em Largura (Até nível {nivelMaximo}):");
            sb.AppendLine(new string('-', 60)); // Separador
            sb.AppendLine($"{"Vértice",-10} {"Índice",-10} {"Nível",-10} {"Pai",-10} {"Nome",-10}"); // Cabeçalho
            sb.AppendLine(new string('-', 60)); // Separador

            // Ordena primeiro por nível e depois por ID
            foreach (var vertice in verticesFiltrados.OrderBy(v => v.ObterNivelBuscaEmLargura()).ThenBy(v => v.Id))
            {
                sb.AppendLine($"{vertice.Id,-10} {vertice.ObterIndiceBuscaEmLargura(),-10} {vertice.ObterNivelBuscaEmLargura(),-10} {(vertice.ObterPaiBuscaEmLargura()?.Id.ToString() ?? "-"),-10} {vertice.Nome,-10} ");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
