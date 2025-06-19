using Grafos.Interfaces;
using Grafos.Models;

namespace Grafos.Classes.MatrizAdjacencia
{
    public partial class GrafoMatrizAdjacencia : IGrafo, IMatrizAdjacencia
    {
        #region Propriedades
        public Aresta[,] MatrizDeAdjacencia { get; set; }
        public List<Vertice> Vertices { get; set; }
        #endregion

        #region Construtores
        public GrafoMatrizAdjacencia()
        {
        }

        public GrafoMatrizAdjacencia(List<Aresta> arestas, List<Vertice> vertices)
        {
            Vertices = vertices;
            var matriz = new Aresta[vertices.Count + 1, vertices.Count + 1];

            foreach (var aresta in arestas)
            {
                matriz[aresta.Origem.Id, aresta.Destino.Id] = aresta;
            }

            MatrizDeAdjacencia = matriz;
        }

        public IGrafo InicializarGrafo(List<Vertice> vertices, List<Aresta> arestas)
        {
            Vertices = vertices;

            // Matriz é criada com tamanho + 1 para permitir uso direto dos IDs como índices
            // Índice 0 não é utilizado, simplificando o acesso à matriz
            var matriz = new Aresta[vertices.Count + 1, vertices.Count + 1];

            foreach (var aresta in arestas)
            {
                matriz[aresta.Origem.Id, aresta.Destino.Id] = aresta;
            }

            MatrizDeAdjacencia = matriz;

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
            var vertice = Vertices.FirstOrDefault(v => v.Id == id);

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
            var aresta = MatrizDeAdjacencia[idOrigem, idDestino];

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
            return Vertices;
        }

        /// <summary>
        /// Retorna todas as arestas do grafo.
        /// </summary>
        /// <returns>Lista com todas as arestas.</returns>
        public List<Aresta> ObterTodasArestas()
        {
            var arestas = new List<Aresta>();
            for (int i = 1; i <= Vertices.Count; i++)
            {
                for (int j = 1; j <= Vertices.Count; j++)
                {
                    if (MatrizDeAdjacencia[i, j] != null)
                        arestas.Add(MatrizDeAdjacencia[i, j]);
                }
            }
            return arestas;
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
            if (MatrizDeAdjacencia[idOrigem, idDestino] == null)
                throw new ArgumentException("Aresta não encontrada.");

            var arestasAdjacentes = new List<Aresta>();

            for (int i = 1; i <= Vertices.Count; i++)
            {
                if (MatrizDeAdjacencia[i, idDestino] != null)
                    arestasAdjacentes.Add(MatrizDeAdjacencia[i, idDestino]);
            }
            return arestasAdjacentes;
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
            var adjacentes = new List<Vertice>();

            for (int i = 1; i <= Vertices.Count; i++)
            {
                if (MatrizDeAdjacencia[idVertice, i] != null)
                    adjacentes.Add(Vertices[i - 1]);
            }

            return adjacentes;
        }

        /// <summary>
        /// Retorna as arestas incidentes a um determinado vértice.
        /// As arestas incidentes a um vértice são aquelas que CHEGAM ao vértice de referência.
        /// </summary>
        /// <param name="idVertice">ID do vértice de referência.</param>
        /// <returns>Uma lista de arestas incidentes.</returns>
        public List<Aresta> ObterArestasIncidentes(int idVertice)
        {
            var arestasIncidentes = new List<Aresta>();

            for (int i = 1; i <= Vertices.Count; i++)
            {
                if (MatrizDeAdjacencia[i, idVertice] != null)
                    arestasIncidentes.Add(MatrizDeAdjacencia[i, idVertice]);
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
            var arestasAdjacentes = new List<Aresta>();

            for (int i = 1; i <= Vertices.Count; i++)
            {
                if (MatrizDeAdjacencia[idVertice, i] != null)
                    arestasAdjacentes.Add(MatrizDeAdjacencia[idVertice, i]);
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
            if (MatrizDeAdjacencia[idOrigem, idDestino] == null)
                throw new ArgumentException("Aresta não encontrada.");

            return (idOrigem, idDestino);
        }

        /// <summary>
        /// Calcula o grau de um vértice (número de arestas que saem dele).
        /// </summary>
        /// <param name="idVertice">ID do vértice a ser analisado.</param>
        /// <returns>O número de arestas que saem do vértice.</returns>
        public int ObterGrauDoVertice(int idVertice)
        {
            int grau = 0;

            for (int j = 1; j <= Vertices.Count; j++)
            {
                if (MatrizDeAdjacencia[idVertice, j] != null)
                {
                    grau++;
                }
            }

            return grau;
        }

        /// <summary>
        /// Verifica se existe uma aresta saindo do primeiro vértice para o segundo.
        /// </summary>
        /// <param name="idV1">ID do vértice de origem.</param>
        /// <param name="idV2">ID do vértice de destino.</param>
        /// <returns>True se existe uma aresta de v1 para v2, False caso contrário.</returns>
        public bool VerificarAdjacenciaEntreVertices(int idV1, int idV2)
        {
            return MatrizDeAdjacencia[idV1, idV2] != null;
        }
        #endregion 

        #region Métodos de modificação

        /// <summary>
        /// Altera o peso de uma aresta existente.
        /// </summary>
        /// <param name="idOrigem">ID do vértice de origem da aresta.</param>
        /// <param name="idDestino">ID do vértice de destino da aresta.</param>
        /// <param name="novoPeso">O novo peso da aresta.</param>
        /// <exception cref="ArgumentException">Se a aresta não existir no grafo.</exception>
        public void SubstituirPesoAresta(int idOrigem, int idDestino, int novoPeso)
        {
            if (MatrizDeAdjacencia[idOrigem, idDestino] != null)
            {
                MatrizDeAdjacencia[idOrigem, idDestino].Peso = novoPeso;
            }
            else
            {
                throw new ArgumentException($"Aresta ({idOrigem}, {idDestino}) não encontrada no grafo.");
            }
        }

        public void TrocarVertices(int idV1, int idV2)
        {
            //Adicionar verficações: Id igual
            //Fazer esse trem
            var arestasV1 = ObterArestasIncidentes(idV1);
            var arestasV2 = ObterArestasIncidentes(idV2);

            for (int i = 1; i <= Vertices.Count; i++)
            {

            }
        }
        #endregion 

        #region Métodos de exibição
        public void ExibirRepresentacao()
        {
            if (Vertices == null || MatrizDeAdjacencia == null)
                throw new InvalidOperationException("Grafo não inicializado");

            Console.WriteLine("\nMatriz de Adjacências:");

            Console.Write("   ");
            for (int i = 1; i <= Vertices.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{i,3}");
                Console.ResetColor();
            }
            Console.WriteLine();

            Console.Write("   ");
            for (int i = 1; i <= Vertices.Count; i++)
                Console.Write("---");
            Console.WriteLine();

            for (int i = 1; i <= Vertices.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{i,2}");
                Console.ResetColor();
                Console.Write("|");

                for (int j = 1; j <= Vertices.Count; j++)
                {
                    var aresta = MatrizDeAdjacencia[i, j];
                    if (aresta == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" - ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{aresta.Peso,3}");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}


