namespace TravellingSalesmanProblem;

public class TSP
{
    public static (List<int>, int) GreedyTSP(Graph graph)
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

            for (int v = 0; v < n; v++)
            {
                if (!visited[v] && graph.adjacencyMatrix[current, v] != 0 && graph.adjacencyMatrix[current, v] < minCost)
                {
                    minCost = graph.adjacencyMatrix[current, v];
                    next = v;
                }
            }

            if (next == -1)
                throw new Exception("Граф не є повним, неможливо знайти шлях");

            visited[next] = true;
            path.Add(next);
            totalCost += minCost;
            current = next;
        }

        totalCost += graph.adjacencyMatrix[current, path[0]]; 
        path.Add(path[0]);

        return (path, totalCost);
    }
    public static (List<int>, int) GreedyTSP_List(Graph graph)
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

            foreach (var (v, weight) in graph.adjacencyList[current])
            {
                if (!visited[v] && weight < minCost)
                {
                    minCost = weight;
                    next = v;
                }
            }

            if (next == -1)
                throw new Exception("Граф не є повним, неможливо знайти шлях");

            visited[next] = true;
            path.Add(next);
            totalCost += minCost;
            current = next;
        }

        foreach (var (v, weight) in graph.adjacencyList[current])
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