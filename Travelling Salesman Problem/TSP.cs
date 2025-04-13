namespace TravellingSalesmanProblem;

public class TSP
{
    public static (List<int>, int) TSPGreedy(Graph graph)
    {
        int n = graph.vertices;
        bool[] visited = new bool[n];
        List<int> path = new List<int>();
        int Cost = 0;

        int current = 0;
        path.Add(current);
        visited[current] = true;

        for (int i = 1; i < n; i++)
        {
            int next = -1;
            int minCost = int.MaxValue;

            for (int v = 0; v < n; v++)
            {
                if (!visited[v] && graph.adjMatrix[current, v] != 0 && graph.adjMatrix[current, v] < minCost)
                {
                    minCost = graph.adjMatrix[current, v];
                    next = v;
                }
            }

            if (next == -1)
                throw new Exception("Граф не повний, тому неможливо знайти шлях");

            visited[next] = true;
            path.Add(next);
            Cost += minCost;
            current = next;
        }

        Cost += graph.adjMatrix[current, path[0]]; 
        path.Add(path[0]);

        return (path, Cost);
    }
    public static (List<int>, int) TSPList(Graph graph)
    {
        int n = graph.vertices;
        bool[] visited = new bool[n];
        List<int> path = new List<int>();
        int totalCost = 0;

        int current = 0;
        path.Add(current);
        visited[current] = true;

        for (int i = 1; i < n; i++)
        {
            int next = -1;
            int minCost = int.MaxValue;

            foreach (var (v, weight) in graph.adjList[current])
            {
                if (!visited[v] && weight < minCost)
                {
                    minCost = weight;
                    next = v;
                }
            }

            if (next == -1)
                throw new Exception("Граф не повний, тому неможливо знайти шлях");

            visited[next] = true;
            path.Add(next);
            totalCost += minCost;
            current = next;
        }

        foreach (var (v, weight) in graph.adjList[current])
        {
            if (v == path[0])
            {
                totalCost += weight;
                path.Add(v);
                break;
            }
        }

        return (path, totalCost);
    }
}