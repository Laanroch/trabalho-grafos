using Grafos.Models;

namespace Grafos.Interfaces
{
    public interface IGrafo
    {
        IGrafo InicializarGrafo(List<Vertice> vertices, List<Aresta> arestas);
        void ExibirRepresentacao();
        Vertice ObterVertice(int id);
        Aresta ObterAresta(int idOrigem, int idDestino);
        List<Vertice> ObterTodosVertices();
        List<Aresta> ObterTodasArestas();
        List<Aresta> ObterAdjacenciasDaAresta(int idOrigem, int idDestino);
        List<Vertice> ObterVizinhanca(int idVertice);
        List<Aresta> ObterArestasIncidentes(int idVertice);
        List<Aresta> ObterArestasAdjacentes(int idVertice);
        (int origem, int destino) ObterVerticesIncidentes(int idOrigem, int idDestino);
        int ObterGrauDoVertice(int idVertice);
        bool VerificarAdjacenciaEntreVertices(int idV1, int idV2);
        void SubstituirPesoAresta(int idOrigem, int idDestino, int novoPeso);
        void TrocarVertices(int idV1, int idV2);
    }
}
