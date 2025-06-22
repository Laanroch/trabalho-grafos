namespace Grafos.Utils
{
    public static class Utils
    {
        public static double CalcularDensidade(int numVertices, int numArestas)
        {
            return numVertices <= 1 ? 0 : (double)numArestas / (numVertices * (numVertices - 1));
        }

        public static double CalcularProbabilidade(int snipers, int balas, double p)
        {
            double total = 0.00;
            if (snipers <= balas)
            {
                for (int i = snipers; i <= balas; i++)
                {
                    double combinacao = Combinacao(balas, i);
                    double probAcertos = Math.Pow(p, i);
                    double probErros = Math.Pow(1 - p, balas - i);
                    total += combinacao * probAcertos * probErros;
                }
            }  
            return total;
        }

        private static double Combinacao(int n, int k)
        {
            return Fatorial(n) / (Fatorial(k) * Fatorial(n - k));
        }

        private static double Fatorial(int n)
        {
            double resultado = 1;
            for (int i = 2; i <= n; i++)
                resultado *= i;
            return resultado;
        }
    }

}
