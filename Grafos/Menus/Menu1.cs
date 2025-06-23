using Grafos.Classes.MatrizAdjacencia;
using Grafos.Classes.ListaAdjacencia;
using Grafos.Interfaces;
using Grafos.Models;
using static Grafos.Utils.Utils;

namespace Grafos.Menus
{
    public class Menu1
    {
        private IGrafo? grafo;

        public void ExecutarMenu()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Menu 1 ===");
                Console.WriteLine("1 - Criar novo grafo");
                Console.WriteLine("2 - Exibir representação do grafo");
                Console.WriteLine("0 - Sair");
                Console.Write("\nEscolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            CriarGrafo();
                            break;
                        case 2:
                            RepresentarGrafo();
                            break;
                        case 0:
                            Console.WriteLine("Saindo...");
                            break;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        private void CriarGrafo()
        {
            try
            {
                Console.WriteLine("\n=== Criação do Grafo ===");

                Console.Write("Digite a quantidade de vértices: ");
                int numVertices = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Digite a quantidade de arestas: ");
                int numArestas = int.Parse(Console.ReadLine() ?? "0");

                double densidade = CalcularDensidade(numVertices, numArestas);
                Console.WriteLine($"\nDensidade do grafo: {densidade:F2}");

                ObterRepresentacaoPorDensidade(densidade);

                var vertices = CriarVertices(numVertices);
                var arestas = CriarArestas(numVertices, numArestas, vertices);

                grafo?.InicializarGrafo(vertices, arestas);

                Console.WriteLine("\nGrafo criado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao criar grafo: {ex.Message}");
            }
        }

        private void ObterRepresentacaoPorDensidade(double densidade)
        {
            if (densidade > 0.5)
            {
                Console.WriteLine("Usando Matriz de Adjacência (densidade > 0,5)");
                grafo = new GrafoMatrizAdjacencia();
            }
            else
            {
                Console.WriteLine("Usando Lista de Adjacência (densidade <= 0,5)");
                grafo = new GrafoListaAdjacencia();
            }
        }

        private static List<Vertice> CriarVertices(int numVertices)
        {
            var vertices = new List<Vertice>();

            for (int i = 1; i <= numVertices; i++)
            {
                vertices.Add(new Vertice());
            }

            return vertices;
        }

        private static List<Aresta> CriarArestas(int numVertices, int numArestas, List<Vertice> vertices)
        {
            var arestas = new List<Aresta>();

            Console.WriteLine("\nDigite as informações das arestas");

            for (int i = 0; i < numArestas; i++)
            {
                Console.WriteLine($"\nAresta {i + 1}");

                Console.Write("Vértice de origem (1 a {0}): ", numVertices);
                int origem = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Vértice de destino (1 a {0}): ", numVertices);
                int destino = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Peso da aresta: ");
                int peso = int.Parse(Console.ReadLine() ?? "0");

                if (origem < 1 || origem > numVertices ||
                    destino < 1 || destino > numVertices)
                {
                    throw new ArgumentException("Vértices inválidos!");
                }

                var arestaOrigem = vertices.Find(v => v.Id == origem);
                var arestaDestino = vertices.Find(v => v.Id == destino);

                if (arestaOrigem != null && arestaDestino != null)
                {
                    arestas.Add(new Aresta(arestaOrigem, arestaDestino, peso));
                }
            }

            return arestas;
        }

        private void RepresentarGrafo()
        {
            if (grafo == null)
            {
                Console.WriteLine("\nGrafo não foi criado ainda!");
                return;
            }

            Console.WriteLine("\n=== Representação do Grafo ===");
            grafo.ExibirRepresentacao();
        }
    }
}
