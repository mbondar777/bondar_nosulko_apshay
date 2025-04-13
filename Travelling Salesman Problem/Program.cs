using System.Diagnostics;

namespace TravellingSalesmanProblem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int top = 200;
            double density = 1;
            int runs = 100;

            double MatrixTime = 0;
            double ListTime = 0;
            double FrequencyTicks = 1000000.0/Stopwatch.Frequency;
            int successRun = 0;


            Console.WriteLine($"Початок експерименту для {top} вершин, щільність {density}");

            while (successRun < runs)
            {
                Graph graph = Graph.RandomGraph(top, density);
                graph.MatrixToList();

                if (!graph.FullyConnected())
                    continue; 

                try
                {
                    var sw1 = Stopwatch.StartNew();
                    TSP.TSPGreedy(graph);
                    sw1.Stop();
                    double matrixTimeMicro = sw1.ElapsedTicks * FrequencyTicks;
                    MatrixTime += matrixTimeMicro/1000.0; 


                    var sw2 = Stopwatch.StartNew();
                    TSP.TSPList(graph);
                    sw2.Stop();
                    double listTimeMicro = sw2.ElapsedTicks * FrequencyTicks;
                    ListTime += listTimeMicro/1000.0; 

                    successRun++;
                    Console.WriteLine($"Успішний запуск #{successRun}");
                }
                catch
                {
                    Console.WriteLine($"Неповний маршрут. Перегенерація");
                    continue;
                }
            }

            double avgMatrix = MatrixTime/runs;
            double avgList = ListTime/runs;

            Console.WriteLine($"Середній час для матриці: {avgMatrix:F4} мс");
            Console.WriteLine($"Середній час для списку:  {avgList:F4} мс");
            Console.WriteLine("кінець експерименту");
            Console.WriteLine($"Вершин: {top}");
            Console.WriteLine($"Щільність: {density}");
            

        }
    }
}