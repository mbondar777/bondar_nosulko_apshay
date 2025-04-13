namespace TravellingSalesmanProblem;

public class Graph
{
        public int[,] adjacencyMatrix;
        public List<List<(int, int)>> adjacencyList;
        public int vertices;

        public Graph(int vertices)
        {
            this.vertices = vertices;
            adjacencyMatrix = new int[vertices, vertices];
            adjacencyList = new List<List<(int, int)>>();
            for (int i = 0; i < vertices; i++)
                adjacencyList.Add(new List<(int, int)>());
        }

        public void AddEdge(int u, int v, int weight)
        {
            adjacencyMatrix[u, v] = weight;
            adjacencyMatrix[v, u] = weight; 

            adjacencyList[u].Add((v, weight));
            adjacencyList[v].Add((u, weight));
        }
        public bool IsFullyConnected()
        {
            bool[] visited = new bool[vertices];
            DFS(0, visited);

            return visited.All(v => v);

            void DFS(int u, bool[] vis)
            {
                vis[u] = true;
                foreach (var (v, _) in adjacencyList[u])
                {
                    if (!vis[v]) DFS(v, vis);
                }
            }
        }


        public void MatrixToList()
        {
            adjacencyList = new List<List<(int, int)>>();
            for (int i = 0; i < vertices; i++)
                adjacencyList.Add(new List<(int, int)>());

            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    if (adjacencyMatrix[i, j] != 0)
                        adjacencyList[i].Add((j, adjacencyMatrix[i, j]));
                }
            }
        }

        public void ListToMatrix()
        {
            adjacencyMatrix = new int[vertices, vertices];

            foreach (var u in adjacencyList)
            {
                foreach (var (v, weight) in u)
                {
                    adjacencyMatrix[adjacencyList.IndexOf(u), v] = weight;
                }
            }
        }

        public static Graph GenerateRandomGraph(int vertices, double density)
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