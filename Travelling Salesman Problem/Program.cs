using System;
using System.Diagnostics;
using System.Linq;

namespace TravellingSalesmanProblem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int vertices = 40;
            double density = 0.8;
            int runs = 100;

            double totalMatrixTime = 0;
            double totalListTime = 0;
            double tickFrequency = 1000000.0 / Stopwatch.Frequency;
            int successfulRuns = 0;


            Console.WriteLine($" Старт експерименту для {vertices} вершин, щільність: {density}");

            while (successfulRuns < runs)
            {
                Graph graph = Graph.GenerateRandomGraph(vertices, density);
                graph.MatrixToList();

                if (!graph.IsFullyConnected())
                    continue; 

                try
                {
                    var sw1 = Stopwatch.StartNew();
                    TSP.GreedyTSP(graph);
                    sw1.Stop();
                    double matrixTimeMicro = sw1.ElapsedTicks * tickFrequency;
                    totalMatrixTime += matrixTimeMicro / 1000.0; 


                    var sw2 = Stopwatch.StartNew();
                    TSP.GreedyTSP_List(graph);
                    sw2.Stop();
                    double listTimeMicro = sw2.ElapsedTicks * tickFrequency;
                    totalListTime += listTimeMicro / 1000.0; 

                    successfulRuns++;
                    Console.WriteLine($" Успішний запуск #{successfulRuns}");
                }
                catch
                {
                    Console.WriteLine($"  Неповний маршрут — перегенеровуємо...");
                    continue;
                }
            }

            double avgMatrix = totalMatrixTime / runs;
            double avgList = totalListTime / runs;

            Console.WriteLine($"Середній час (матриця): {avgMatrix:F4} мс");
            Console.WriteLine($"Середній час (список):  {avgList:F4} мс");



            Console.WriteLine($"\n ЕКСПЕРИМЕНТ ЗАВЕРШЕНО");
            Console.WriteLine($"Вершин: {vertices}");
            Console.WriteLine($"Щільність: {density}");
            

        }
    }
}
