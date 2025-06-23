namespace Grafos.Models
{
    public class Vertice
    {
        private static int _id = 1;
        public int Id { get; set; }
        public string? Nome { get; set; }
        public BuscaProfundidade? BuscaProfundidade { get; set; }
        public CaminhoMinimo? CaminhoMinimo { get; set; }
        public BuscaLargura? BuscaLargura { get; set; }

        public Vertice(string name = "")
        {
            Id = _id;
            Nome = name;
            _id++;
        }

        public static void ResetarId()
        {
            _id = 1;
        }

        #region Busca em Profundidade

        public void ResetBuscaEmProfundidade()
        {
            BuscaProfundidade = new BuscaProfundidade();
        }

        public int ObterTempoDescoberta()
        {
            return BuscaProfundidade.TempoDescoberta;
        }

        public void DefinirTempoDescoberta(int tempo)
        {
            BuscaProfundidade.TempoDescoberta = tempo;
        }

        public int ObterTempoTermino()
        {
            return BuscaProfundidade.TempoTermino;
        }

        public void DefinirTempoTermino(int tempo)
        {
            BuscaProfundidade.TempoTermino = tempo;
        }

        public Vertice? ObterPai()
        {
            return BuscaProfundidade.Pai;
        }

        public void DefinirPai(Vertice pai)
        {
            BuscaProfundidade.Pai = pai;
        }

        #endregion

        #region Busca em Largura
        public void ResetBuscaEmLargura()
        {
            BuscaLargura = new BuscaLargura();
        }

        public Vertice? ObterPaiBuscaEmLargura()
        {
            return BuscaLargura.Pai;
        }

        public void DefinirPaiBuscaEmLargura(Vertice pai)
        {
            BuscaLargura.Pai = pai;
        }

        public int ObterIndiceBuscaEmLargura()
        {
            return BuscaLargura.Indice;
        }

        public void DefinirIndiceBuscaEmLargura(int indice)
        {
            BuscaLargura.Indice = indice;
        }

        public int ObterNivelBuscaEmLargura()
        {
            return BuscaLargura.Nivel;
        }

        public void DefinirNivelBuscaEmLargura(int nivel)
        {
            BuscaLargura.Nivel = nivel;
        }

        #endregion 

        #region Caminho Mínimo

        public void ResetCaminhoMinimo()
        {
            CaminhoMinimo = new CaminhoMinimo();
        }

        public decimal ObterDistanciaCM()
        {
            return CaminhoMinimo.Distancia;
        }

        public void DefinirDistanciaCM(decimal distancia)
        {
            CaminhoMinimo.Distancia = distancia;
        }

        public Vertice? ObterPaiCM()
        {
            return CaminhoMinimo.Pai;
        }

        public Vertice DefinirPaiCM(Vertice pai)
        {
            CaminhoMinimo.Pai = pai;
            return this;
        }
        #endregion Caminho Mínimo

    }

    public class BuscaProfundidade
    {
        public int TempoDescoberta { get; set; }
        public int TempoTermino { get; set; }
        public Vertice? Pai { get; set; }

        public BuscaProfundidade()
        {
            TempoDescoberta = 0;
            TempoTermino = 0;
            Pai = null;
        }
    }

    public class BuscaLargura
    {
        public int Indice { get; set; }
        public int Nivel { get; set; }
        public Vertice? Pai { get; set; }

        public BuscaLargura()
        {
            Indice = 0;
            Nivel = 0;
            Pai = null;
        }
    }

    public class CaminhoMinimo
    {
        public decimal Distancia { get; set; }
        public Vertice? Pai { get; set; }

        public CaminhoMinimo()
        {
            Distancia = int.MaxValue;
            Pai = null;
        }
    }
}
