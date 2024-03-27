using System;
using System.Text;
using Palmtree.Application;

namespace SimpleFileNameRenumberer.WindowsDesktop
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var launcher = new ConsoleApplicationLauncher("filerenum", Encoding.UTF8);
            launcher.Launch(args);
        }
    }
}
