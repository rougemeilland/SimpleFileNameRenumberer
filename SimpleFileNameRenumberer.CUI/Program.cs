using System.Text;
using Palmtree;

namespace SimpleFileNameRenumberer.CUI
{
    public partial class Program
    {
        private static int Main(string[] args)
        {
            var application = new SimpleFileNameRenumbererApplication(typeof(Program).Assembly.GetAssemblyFileNameWithoutExtension(), Encoding.UTF8);
            return application.Run(args);
        }
    }
}
