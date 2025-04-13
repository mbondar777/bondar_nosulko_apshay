namespace TravellingSalesmanProblem;

public class Graph
{
        public int[,] adjMatrix;
        public List<List<(int, int)>> adjList;
        public int vertices;

        public Graph(int vertices)
        {
            this.vertices = vertices;
            adjMatrix = new int[vertices, vertices];
            adjList = new List<List<(int, int)>>();
            for (int i = 0; i < vertices; i++)
                adjList.Add(new List<(int, int)>());
        }

        public void AddEdge(int u, int v, int weight)
        {
            adjMatrix[u, v] = weight;
            adjMatrix[v, u] = weight; 

            adjList[u].Add((v, weight));
            adjList[v].Add((u, weight));
        }
        public bool FullyConnected()
        {
            bool[] visited = new bool[vertices];
            DFS(0, visited);

            return visited.All(v => v);

            void DFS(int u, bool[] vis)
            {
                vis[u] = true;
                foreach (var (v, _) in adjList[u])
                {
                    if (!vis[v]) DFS(v, vis);
                }
            }
        }


        public void MatrixToList()
        {
            adjList = new List<List<(int, int)>>();
            for (int i = 0; i < vertices; i++)
                adjList.Add(new List<(int, int)>());

            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    if (adjMatrix[i, j] != 0)
                        adjList[i].Add((j, adjMatrix[i, j]));
                }
            }
        }

        public static Graph RandomGraph(int vertices, double density)
        {
            var rand = new Random();
            Graph g = new Graph(vertices);

            int maxEdges = vertices * (vertices - 1) / 2;
            int targetEdges = (int)(density * maxEdges);

            HashSet<(int, int)> edges = new HashSet<(int, int)>();

            while (edges.Count < targetEdges)
            {
                int u = rand.Next(vertices);
                int v = rand.Next(vertices);
                if (u != v && !edges.Contains((Math.Min(u, v), Math.Max(u, v))))
                {
                    int weight = rand.Next(1, 100);
                    g.AddEdge(u, v, weight);
                    edges.Add((Math.Min(u, v), Math.Max(u, v)));
                }
            }

            return g;
        }
}