namespace TravellingSalesmanProblem;

public class Program
{
    static void Main(string[] args)
    {
        int vertices = 5;
        double density = 1.0;

        Graph g = Graph.GenerateRandomGraph(vertices, density);

        Console.WriteLine("Матриця суміжності:");
        for (int i = 0; i < vertices; i++)
        {
            for (int j = 0; j < vertices; j++)
            {
                Console.Write(g.adjacencyMatrix[i, j].ToString().PadLeft(4));
            }
            Console.WriteLine();
        }

        var (path, cost) = TSP.GreedyTSP(g);

        Console.WriteLine("Знайдений шлях:");
        foreach (var v in path)
            Console.Write(v + " ");

        Console.WriteLine("Вартість шляху: " + cost);
    }
}