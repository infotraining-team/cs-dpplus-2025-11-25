namespace SimpleRaceCondition
{
    internal class Program
    {
        private static int counter;
        private static object _lock = new object();
        static void Main(string[] args)
        {
            int nOfTasks = 10;
            int increment = 100_000;

            Task[] tasks = new Task[nOfTasks];

            for (int i = 0; i < nOfTasks; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < increment; j++)
                    {
                        var a = 3;
                        //lock (_lock)
                        //{
                        //    counter++;
                        //}
                        Interlocked.Increment(ref counter);
                        var b = 4;
                        //return a + b;
                    }
                });
            }

            Task.WaitAll(tasks);
            Console.WriteLine($"Counter = {counter}");
        }
    }
}
