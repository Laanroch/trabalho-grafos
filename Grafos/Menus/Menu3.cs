// =============================
// Arquivo: Menu3.cs
// =============================

using Grafos.LeitorSniper;
using Grafos.Algoritmos;
using Grafos.Utils;
using System.Globalization;

namespace Grafos.Menus
{
    public class Menu3
    {
        public void ExecutarMenu()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Menu 3: Desafio dos Snipers ===");
                Console.WriteLine("1 - Carregar instância do desafio");
                Console.WriteLine("0 - Voltar ao menu principal");
                Console.Write("\nEscolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            ExecutarDesafio();
                            break;
                        case 0:
                            Console.WriteLine("Voltando...");
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

        private void ExecutarDesafio()
        {
            try
            {
                Console.Write("\nDigite o caminho do arquivo da instância: ");
                string path = Console.ReadLine() ?? string.Empty;

                var instancia = LeitorSniper.LerArquivo(path);

                int snipersNoCaminho = SniperDijkstra.CalcularSnipersNoCaminho(instancia);

                double resultado;
                if (snipersNoCaminho > instancia.Balas)
                {
                    resultado = 0.0;
                }
                else
                {
                    resultado = Probabilidade.CalcularProbabilidadeSucesso(snipersNoCaminho, instancia.Balas, instancia.Probabilidade);
                }

                Console.WriteLine($"\nProbabilidade de sucesso: {resultado.ToString("F3", CultureInfo.InvariantCulture)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao executar desafio: {ex.Message}");
            }
        }
    }
}
