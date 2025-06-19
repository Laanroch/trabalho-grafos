namespace Grafos.Models
{
    public enum ClassificacaoAresta
    {
        Nenhuma,
        ArestaArvore,
        ArestaDeRetorno,
        ArestaDeAvanco,
        ArestaDeCruzamento,
        ArestaTio,
        ArestaPrimo,
        ArestaIrmao
    }

    public class Aresta
    {
        private static int _id = 1;
        public int Id { get; set; }
        public Vertice? Origem { get; set; }
        public Vertice? Destino { get; set; }
        public int Peso { get; set; }
        public ClassificacaoAresta Tipo { get; set; }

        public Aresta(Vertice? origem, Vertice? destino, int peso)
        {
            Origem = origem;
            Destino = destino;
            Peso = peso;
            Id = _id;
            _id++;
        }
    }
}
