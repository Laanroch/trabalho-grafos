using Grafos.Interfaces;
using Grafos.Models;

namespace Grafos.Classes.ListaAdjacencia
{
    public class GrafoListaAdjacencia : IGrafo
    {
        #region Propriedades
        public List<Aresta> Arestas { get; set; }
        public List<Vertice>[] Vertices { get; set; }
        #endregion

        #region Construtores
        public GrafoListaAdjacencia()
        {

        }

        public GrafoListaAdjacencia(List<Vertice>[] vertices, List<Aresta> arestas)
        {
            Vertices = vertices;
            Arestas = arestas;
        }

        public IGrafo InicializarGrafo(List<Vertice> vertices, List<Aresta> arestas)
        {
            Vertices = new List<Vertice>[vertices.Count + 1];

            foreach (var aresta in arestas)
            {
                for (int i = 1; i < Vertices.Length; i++)
                {
                    if (Vertices[i] == null)
                    {
                        Vertices[i] = new List<Vertice>();
                    }

                    if (aresta.Origem?.Id == i)
                    {
                        Vertices[i].Add(aresta.Destino);
                    }
                }
            }

            Arestas = arestas;
            return this;
        }
        #endregion

        #region Métodos de consulta simples
        /// <summary>
        /// Retorna um vértice a partir do seu ID.
        /// </summary>
        /// <param name="id">ID do vértice.</param>
        /// <returns>O vértice correspondente ao ID.</returns>
        /// <exception cref="ArgumentException">Se o vértice não for encontrado.</exception>
        public Vertice ObterVertice(int id)
        {
            var vertice = Vertices[id].FirstOrDefault();

            if (vertice == null)
                throw new ArgumentException($"Vértice com ID {id} não encontrado.");

            return vertice;
        }

        /// <summary>
        /// Retorna uma aresta a partir dos IDs dos vértices de origem e destino.
        /// </summary>
        /// <param name="idOrigem">ID do vértice de origem.</param>
        /// <param name="idDestino">ID do vértice de destino.</param>
        /// <returns>A aresta correspondente.</returns>
        /// <exception cref="ArgumentException">Se a aresta não for encontrada.</exception>
        public Aresta ObterAresta(int idOrigem, int idDestino)
        {
            var aresta = Arestas.FirstOrDefault(a => a.Origem?.Id == idOrigem && a.Destino?.Id == idDestino);

            if (aresta == null)
                throw new ArgumentException($"Aresta ({idOrigem}, {idDestino}) não encontrada.");

            return aresta;
        }

        /// <summary>
        /// Retorna todos os vértices do grafo.
        /// </summary>
        /// <returns>Lista com todos os vértices.</returns>
        public List<Vertice> ObterTodosVertices()
        {
            return Vertices.SelectMany(v => v).ToList();
        }

        /// <summary>
        /// Retorna todas as arestas do grafo.
        /// </summary>
        /// <returns>Lista com todos as arestas.</returns>
        public List<Aresta> ObterTodasArestas()
        {
            return Arestas;
        }
        #endregion

        #region Métodos de consulta complexos

        /// <summary>
        /// Retorna as arestas adjacentes a uma determinada aresta.
        /// Arestas adjacentes são duas arestas com um destino em comum
        /// </summary>
        /// <param name="idOrigem">ID do vértice de origem da aresta.</param>
        /// <param name="idDestino">ID do vértice de destino da aresta.</param>
        /// <returns>Uma lista de arestas adjacentes.</returns>
        public List<Aresta> ObterAdjacenciasDaAresta(int idOrigem, int idDestino)
        {
            var aresta = ObterAresta(idOrigem, idDestino);
            var adjacencias = new List<Aresta>();

            foreach (var a in Arestas)
            {
                if (a.Destino?.Id == aresta.Destino?.Id)
                {
                    adjacencias.Add(a);
                }
            }

            return adjacencias;
        }

        /// <summary>
        /// Retorna os vértices adjacentes a um determinado vértice.
        /// Vértices adjacentes são aqueles que são conectados pelo vértice de referência.
        /// São obtidos a partir da linha.
        /// É a vizinhança do vértice. //IMPORTANTE!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="idVertice">ID do vértice de referência.</param>
        /// <returns>Uma lista de vértices adjacentes.</returns>
        public List<Vertice> ObterVizinhanca(int idVertice)
        {
            var vertice = ObterVertice(idVertice);
            var vizinhanca = new List<Vertice>();

            foreach (var vizinho in Vertices[vertice.Id])
            {
                vizinhanca.Add(vizinho);
            }

            return vizinhanca;
        }

        /// <summary>
        /// Retorna as arestas incidentes a um determinado vértice.
        /// As arestas incidentes a um vértice são aquelas que CHEGAM ao vértice de referência.
        /// </summary>
        /// <param name="idVertice">ID do vértice de referência.</param>
        /// <returns>Uma lista de arestas incidentes.</returns>
        public List<Aresta> ObterArestasIncidentes(int idVertice)
        {
            var vertice = ObterVertice(idVertice);
            var arestasIncidentes = new List<Aresta>();

            foreach (var aresta in Arestas)
            {
                if (aresta.Destino?.Id == vertice.Id)
                {
                    arestasIncidentes.Add(aresta);
                }
            }

            return arestasIncidentes;
        }

        /// <summary>
        /// Retorna as arestas adjacentes a um determinado vértice.
        /// As arestas adjacentes a um vértice são aquelas que SAEM do vértice de referência.
        ///  É a vizinhança do vértice. 
        /// </summary>
        /// <param name="idVertice">ID do vértice de referência.</param>
        /// <returns>Uma lista de arestas adjacentes.</returns>
        public List<Aresta> ObterArestasAdjacentes(int idVertice)
        {
            var vertice = ObterVertice(idVertice);
            var arestasAdjacentes = new List<Aresta>();

            foreach (var aresta in Arestas)
            {
                if (aresta.Origem?.Id == vertice.Id)
                {
                    arestasAdjacentes.Add(aresta);
                }
            }

            return arestasAdjacentes;
        }

        /// <summary>
        /// Retorna os IDs dos vértices que são origem e destino de uma aresta.
        /// </summary>
        /// <param name="idOrigem">ID do vértice de origem da aresta.</param>
        /// <param name="idDestino">ID do vértice de destino da aresta.</param>
        /// <returns>Uma tupla contendo os IDs dos vértices de origem e destino.</returns>
        public (int origem, int destino) ObterVerticesIncidentes(int idOrigem, int idDestino)
        {
            var aresta = ObterAresta(idOrigem, idDestino);

            return (aresta.Origem.Id, aresta.Destino.Id);
        }

        /// <summary>
        /// Calcula o grau de um vértice (número de arestas que saem dele).
        /// </summary>
        /// <param name="idVertice">ID do vértice a ser analisado.</param>
        /// <returns>O número de arestas que saem do vértice.</returns>
        public int ObterGrauDoVertice(int idVertice)
        {
            var vertice = ObterVertice(idVertice);

            return ObterArestasAdjacentes(vertice.Id).Count;
        }

        /// <summary>
        /// Verifica se existe uma aresta saindo do primeiro vértice para o segundo.
        /// </summary>
        /// <param name="idV1">ID do vértice de origem.</param>
        /// <param name="idV2">ID do vértice de destino.</param>
        /// <returns>True se existe uma aresta de v1 para v2, False caso contrário.</returns>
        public bool VerificarAdjacenciaEntreVertices(int idV1, int idV2)
        {
            var vertice1 = ObterVertice(idV1);
            var vertice2 = ObterVertice(idV2);

            return Arestas.Any(a => a.Origem?.Id == vertice1.Id && a.Destino?.Id == vertice2.Id);
        }
        #endregion

        #region Métodos de modificação
        public void SubstituirPesoAresta(int idOrigem, int idDestino, int novoPeso)
        {
            var aresta = ObterAresta(idOrigem, idDestino);
            aresta.Peso = novoPeso;
        }

        public void TrocarVertices(int idV1, int idV2)
        {
            var vertice1 = ObterVertice(idV1);
            var vertice2 = ObterVertice(idV2);

            foreach (var aresta in Arestas)
            {
                if (aresta.Origem?.Id == vertice1.Id)
                {
                    aresta.Origem = vertice2;
                }
                if (aresta.Destino?.Id == vertice1.Id)
                {
                    aresta.Destino = vertice2;
                }
                if (aresta.Origem?.Id == vertice2.Id)
                {
                    aresta.Origem = vertice1;
                }
                if (aresta.Destino?.Id == vertice2.Id)
                {
                    aresta.Destino = vertice1;
                }
            }

            // var verticesAux = Vertices[vertice1.Id];

            // Vertices[vertice1.Id] = Vertices[vertice2.Id];
            // Vertices[vertice2.Id] = verticesAux;

            (Vertices[vertice2.Id], Vertices[vertice1.Id]) = (Vertices[vertice1.Id], Vertices[vertice2.Id]);
        }
        #endregion

        #region Métodos de exibição
        public void ExibirRepresentacao()
        {
            if (Vertices == null || Arestas == null)
                throw new InvalidOperationException("Grafo não inicializado");

            Console.WriteLine("\nLista de Adjacências:");

            for (int i = 1; i < Vertices.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{i} -> ");
                Console.ResetColor();

                for (int j = 1; j < Vertices.Length; j++)
                {
                    if (Vertices[i] != null && Vertices[i].Any(v => v.Id == j))
                    {
                        Console.Write(j + " -> ");
                    }
                }
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine("\u23DA");
            }
        }
        #endregion
    }
}