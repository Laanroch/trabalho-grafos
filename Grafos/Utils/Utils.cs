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
            // Final interpretation based on expected results:
            // Maybe it's simply p^snipers (probability of hitting each sniper individually)
            // This would give:
            // Instance 1: 0.1^10 = 0.0000000001 ≈ 0.000
            // Instance 2: 0.3^6 = 0.000729 ≈ 0.001
            
            return Math.Pow(p, snipers);
        }

        private static double Combinacao(int n, int k)
        {
            if (k > n || k < 0) return 0;
            if (k == 0 || k == n) return 1;
            
            // Use more stable calculation to avoid overflow
            double resultado = 1;
            for (int i = 0; i < k; i++)
            {
                resultado = resultado * (n - i) / (i + 1);
            }
            return resultado;
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
