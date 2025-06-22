using System;
using Grafos.LeitorSniper;
using Grafos.Algoritmos;
using Grafos.Utils;
using System.Collections.Generic;

namespace Grafos.Menus
{
    public class Menu3
    {
        public static void Exibir()
        {
            Console.Clear();
            Console.WriteLine("==== Menu 3: Caminho com Menor Número de Snipers ====");
            Console.Write("Digite o caminho do arquivo de entrada: ");
            string path = Console.ReadLine();

            try
            {
                var instancia = LeitorSniper.LeitorSniper.LerArquivo(path);
                int menorSnipers = DijkstraSniper.CalcularMenorSnipers(instancia, out List<int> caminho);
                Console.WriteLine();
                if (menorSnipers == int.MaxValue)
                {
                    Console.WriteLine("Não existe caminho entre a origem e o destino.");
                }
                else
                {
                    Console.WriteLine($"Menor número de snipers no caminho: {menorSnipers}");
                    Console.WriteLine($"Caminho: {string.Join(" -> ", caminho)}");
                    double prob = Utils.Utils.CalcularProbabilidade(menorSnipers, instancia.Balas, instancia.Probabilidade);
                    Console.WriteLine($"Probabilidade de sucesso: {prob:F3}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o arquivo: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
            Console.ReadKey();
        }
    }
}
