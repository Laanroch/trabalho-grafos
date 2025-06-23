using Grafos.Interfaces;

public class InstanciaSnipers
{
    public IGrafo Grafo
    {
        get; set;
    }
    public int Origem
    {
        get; set;
    }
    public int Destino
    {
        get; set;
    }
    public int Balas
    {
        get; set;
    }
    public double Probabilidade
    {
        get; set;
    }
    public Dictionary<int, int> SnipersPorVertice
    {
        get; set;
    }
}