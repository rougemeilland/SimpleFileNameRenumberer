using System.Text;
using Palmtree;
using Palmtree.IO.Console;

namespace SimpleFileNameRenumberer.CUI
{
    public partial class Program
    {
        private static int Main(string[] args)
        {
            TinyConsole.DefaultTextWriter = ConsoleTextWriterType.StandardError;
            var application = new SimpleFileNameRenumbererApplication(typeof(Program).Assembly.GetAssemblyFileNameWithoutExtension(), Encoding.UTF8);
            return application.Run(args);
        }
    }
}
