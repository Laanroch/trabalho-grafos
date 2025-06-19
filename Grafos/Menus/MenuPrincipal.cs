namespace Grafos.Menus
{
    public static class MenuPrincipal
    {
        public static void ExecutarMenu()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Menu Principal ===");
                Console.WriteLine("1 - Criar novo grafo");
                Console.WriteLine("2 - Obter grafo Dimac");
                Console.WriteLine("0 - Sair");
                Console.Write("\nEscolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            var menu1 = new Menu1();
                            menu1.ExecutarMenu();
                            Console.Clear();
                            break;
                        case 2:
                            var menu2 = new Menu2();
                            menu2.ExecutarMenu();
                            Console.Clear();
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
    }
}
