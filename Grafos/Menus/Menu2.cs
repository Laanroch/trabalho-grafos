using Grafos.Algoritmos;
using Grafos.Interfaces;
using Grafos.LeitorDimac;

namespace Grafos.Menus
{
    public class Menu2
    {
        private IGrafo? grafo;
        private const string SolicitaVertice = "Digite o vértice: ";
        private const string SolicitaOrigem = "Digite o vértice de origem: ";
        private const string SolicitaDestino = "Digite o vértice de destino: ";

        public void ExecutarMenu()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Menu 2 ===");
                Console.WriteLine("1 - Obter grafo Dimac de arquivo");
                Console.WriteLine("2 - Interagir com grafo obtido");
                Console.WriteLine("0 - Sair");
                Console.Write("\nEscolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            LerArquivoDimac();
                            break;
                        case 2:
                            if (grafo == null)
                            {
                                Console.WriteLine("Grafo não foi carregado!");
                                break;
                            }

                            InteragirComGrafo();
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


        private void LerArquivoDimac()
        {
            Console.Write("Digite o caminho do arquivo: ");
            var caminho = Console.ReadLine();

            try
            {
                grafo = DimacReader.LerArquivo(caminho);
                Console.WriteLine("Grafo criado com sucesso!\n");
                grafo.ExibirRepresentacao();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao ler o grafo: {ex.Message}");
            }
        }

        private void InteragirComGrafo()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Interações com o Grafo ===");
                Console.WriteLine("1 - Imprimir todas as arestas adjacentes a uma aresta");
                Console.WriteLine("2 - Imprimir todos os vértices adjacentes a um vértice");
                Console.WriteLine("3 - Imprimir todas as arestas incidentes a um vértice");
                Console.WriteLine("4 - Imprimir todos os vértices incidentes a uma aresta");
                Console.WriteLine("5 - Imprimir o grau do vértice");
                Console.WriteLine("6 - Determinar se dois vértices são adjacentes");
                Console.WriteLine("7 - Substituir o peso de uma aresta");
                Console.WriteLine("8 - Trocar dois vértices");
                Console.WriteLine("9 - Busca em Largura");
                Console.WriteLine("10 - Busca em Profundidade");
                Console.WriteLine("11 - Dijkstra");
                Console.WriteLine("12 - Floyd-Warshall");
                Console.WriteLine("0 - Voltar");
                Console.Write("\nEscolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            ImprimirArestasAdjacentes();
                            break;
                        case 2:
                            ImprimirVerticesAdjacentes();
                            break;
                        case 3:
                            ImprimirArestasIncidentes();
                            break;
                        case 4:
                            ImprimirVerticesIncidentais();
                            break;
                        case 5:
                            ImprimirGrauVertice();
                            break;
                        case 6:
                            DeterminarAdjacencia();
                            break;
                        case 7:
                            SubstituirPesoAresta();
                            break;
                        case 8:
                            TrocarVertices();
                            break;
                        case 9:
                            BuscaLargura();
                            break;
                        case 10:
                            BuscaProfundidade();
                            break;
                        case 11:
                            Dijkstra();
                            break;
                        case 12:
                            FloydWarshall();
                            break;
                        case 13:
                            grafo.ExibirRepresentacao();
                            break;
                        case 0:
                            return;
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

        private void ImprimirArestasAdjacentes()
        {
            Console.Write(SolicitaOrigem);
            var origem = int.Parse(Console.ReadLine());
            Console.Write(SolicitaDestino);
            var destino = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nArestas adjacentes à aresta ({origem}, {destino}): {grafo.ObterAdjacenciasDaAresta(origem, destino)}");
        }

        private void ImprimirVerticesAdjacentes()
        {
            Console.Write(SolicitaVertice);
            var vertice = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nVértices adjacentes ao vértice {vertice}: {grafo.ObterVizinhanca(vertice)}");
        }

        private void ImprimirArestasIncidentes()
        {
            Console.Write(SolicitaVertice);
            var vertice = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nArestas incidentes ao vértice {vertice}: {grafo.ObterArestasIncidentes(vertice)}");
        }

        private void ImprimirVerticesIncidentais()
        {
            Console.Write(SolicitaOrigem);
            var origem = int.Parse(Console.ReadLine());
            Console.Write(SolicitaDestino);
            var destino = int.Parse(Console.ReadLine());

            var (v1, v2) = grafo.ObterVerticesIncidentes(origem, destino);
            Console.WriteLine($"\nVértices incidentes à aresta ({origem}, {destino}): {v1}, {v2}");
        }

        private void ImprimirGrauVertice()
        {
            Console.Write(SolicitaVertice);
            var vertice = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nGrau do vértice {vertice}: {grafo.ObterGrauDoVertice(vertice)}");
        }

        private void DeterminarAdjacencia()
        {
            Console.Write(SolicitaOrigem);
            var v1 = int.Parse(Console.ReadLine());
            Console.Write(SolicitaDestino);
            var v2 = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nOs vértices {v1} e {v2} são adjacentes? {grafo.VerificarAdjacenciaEntreVertices(v1, v2)}");
        }

        private void SubstituirPesoAresta()
        {
            Console.Write(SolicitaOrigem);
            var origem = int.Parse(Console.ReadLine());
            Console.Write(SolicitaDestino);
            var destino = int.Parse(Console.ReadLine());
            Console.Write("Digite o novo peso: ");
            var peso = int.Parse(Console.ReadLine());

            grafo.SubstituirPesoAresta(origem, destino, peso);
            Console.WriteLine("\nPeso substituído com sucesso!");
        }

        private void TrocarVertices()
        {
            Console.Write(SolicitaOrigem);
            var v1 = int.Parse(Console.ReadLine());
            Console.Write(SolicitaDestino);
            var v2 = int.Parse(Console.ReadLine());

            grafo.TrocarVertices(v1, v2);
            Console.WriteLine("\nVértices trocados com sucesso!");
        }

        private void BuscaLargura()
        {
            Console.Write(SolicitaOrigem);
            var origem = int.Parse(Console.ReadLine());

            grafo.ExecutarBuscaEmLargura(origem).GerarTabelaBuscaEmLargura();
        }

        private void BuscaProfundidade()
        {
            Console.Write(SolicitaOrigem);
            var origem = int.Parse(Console.ReadLine());

            grafo.ExecutarBuscaEmProfundidade(origem);
        }

        private void Dijkstra()
        {
            Console.Write(SolicitaOrigem);
            var origem = int.Parse(Console.ReadLine());

            grafo.ExecutarDijkstra(origem);
            grafo.ImprimirTabelaCaminhoMinimo();
        }

        private void FloydWarshall()
        {

        }
    }
}
