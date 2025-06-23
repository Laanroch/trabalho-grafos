namespace Grafos.Interfaces
{
    public interface IMatrizAdjacencia
    {
        public int[,] ExecutarFloydWarshall();
        void GerarTabelaDistancias(int[,] distancias);
    }
}
