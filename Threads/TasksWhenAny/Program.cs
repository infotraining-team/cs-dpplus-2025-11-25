namespace ThreadsFun
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Task<string>> tasks = new List<Task<string>>();
            Task<string> task1 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("From slow task");
                return "Value from slow task";
            });
            tasks.Add(task1);

            Task<string> task2 = Task.Run(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine("From fast task");
                return "Value from fast task";
            });
            tasks.Add(task2);

            Console.WriteLine("After starting tasks");
            int Index = Task.WaitAny(tasks.ToArray());
            //foreach (Task<string> task in tasks)
            //{
            //    Console.WriteLine($"{task.Result}");
            //}
            Console.WriteLine($"Winner: {tasks[Index].Result}");

            Console.WriteLine("After task has finished");
        }
    }
}