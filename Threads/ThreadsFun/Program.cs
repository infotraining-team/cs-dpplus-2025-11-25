namespace ThreadsFun
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Task<string>> tasks = new List<Task<string>>();
            for (int i = 0; i < 10; ++i)
            {
                Task<string> task = Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("From task");
                    return "Value from task";
                });
                tasks.Add(task);
            }

            Console.WriteLine("After starting tasks");
            Task.WaitAll(tasks.ToArray());
            foreach (Task<string> task in tasks)
            {
                Console.WriteLine($"{task.Result}");
            }

            Console.WriteLine("After task has finished");
        }
    }
}
