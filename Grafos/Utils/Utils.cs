namespace Grafos.Utils
{
    public static class Utils
    {
        public static double CalcularDensidade(int numVertices, int numArestas)
        {
            return numVertices <= 1 ? 0 : (double)numArestas / (numVertices * (numVertices - 1));
        }
    }
}
