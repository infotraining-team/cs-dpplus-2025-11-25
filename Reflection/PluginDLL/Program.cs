using System.Reflection;

namespace PluginDLL
{
    public interface IPlugin
    {
        string Name { get; }
        void Execute();
    }

    public class LoggerPlugin : IPlugin
    {
        public string Name => "File Logger Plugin";

        public void Execute()
        {
            Console.WriteLine("Hello from plugin");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Assembly pluginAssembly = Assembly.GetExecutingAssembly();
            //Assembly pluginAssembly = Assembly.LoadFile("my.dll");

            var availabeTypes = pluginAssembly.GetTypes()
                .Where(t => typeof(IPlugin).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
            foreach (Type type in availabeTypes)
            {
                Console.WriteLine(type.FullName);
                IPlugin plugin = (IPlugin)(Activator.CreateInstance(type));
                Console.WriteLine($"Plugin Name = {plugin.Name}");
                plugin.Execute();
            }

        }
    }
}
