using System;
using System.IO;
using System.Collections.Generic;
using Grafos.LeitorSniper;
using Grafos.Algoritmos;
using Grafos.Utils;

namespace Grafos.Menus
{
    public static class Menu3
    {
        // Calcula a pasta raiz do projeto (sobe 3 níveis a partir de bin/.../net7.0)
        private static readonly string ProjectRoot =
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));

        public static void Exibir()
        {
            Console.Clear();
            Console.WriteLine("==== Menu 3: Caminho com Menor Número de Snipers ====");
            
            // Buscar arquivos .txt na raiz do projeto
            var txtFiles = Directory.GetFiles(ProjectRoot, "*.txt");
            
            if (txtFiles.Length == 0)
            {
                Console.WriteLine("Nenhum arquivo .txt encontrado na raiz do projeto.");
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Arquivos disponíveis:");
            for (int i = 0; i < txtFiles.Length; i++)
            {
                string fileName = Path.GetFileName(txtFiles[i]);
                Console.WriteLine($"{i + 1}. {fileName}");
            }
            
            Console.Write("\nEscolha o número do arquivo (ou 0 para voltar): ");
            string input = Console.ReadLine()?.Trim() ?? "";
            
            if (!int.TryParse(input, out int escolha) || escolha < 0 || escolha > txtFiles.Length)
            {
                Console.WriteLine("Opção inválida!");
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
                Console.ReadKey();
                return;
            }
            
            if (escolha == 0)
            {
                return; // Volta ao menu principal
            }
            
            string resolvedPath = txtFiles[escolha - 1];

            try
            {
                // LerTodasInstancias já lança FileNotFoundException se não existir
                var instancias = LeitorSniper.LeitorSniper.LerTodasInstancias(resolvedPath);

                int caso = 1;
                foreach (var instancia in instancias)
                {
                    int menorSnipers = DijkstraSniper.CalcularMenorSnipers(instancia, out List<int> caminho);
                    Console.WriteLine($"\nInstância {caso++}:");
                    if (menorSnipers == int.MaxValue)
                    {
                        Console.WriteLine("Não existe caminho entre a origem e o destino.");
                    }
                    else
                    {
                        Console.WriteLine($"Menor número de snipers no caminho: {menorSnipers}");
                        Console.WriteLine($"Caminho: {string.Join(" -> ", caminho)}");
                        double prob = Utils.Utils.CalcularProbabilidade(
                            menorSnipers,
                            instancia.Balas,
                            instancia.Probabilidade
                        );
                        Console.WriteLine($"Probabilidade de sucesso: {prob:F3}");
                    }
                }
            }
            catch (FileNotFoundException fnf)
            {
                Console.WriteLine($"Arquivo não encontrado: {fnf.FileName}");
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
