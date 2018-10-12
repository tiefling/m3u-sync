using System;

namespace M3uSync
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                Console.WriteLine("USAGE: M3uSync.exe {sourcePath} {destinationPath} [/C] - Optional switch to clean unwanted inner path parameters from the playlists instead of change file formats");

            if (args.Length > 2 && (args[2] == "/C" || args[2] =="/c"))
                Cleaner.Execute(args[0], args[1]);
            else
                Synchroniser.Execute(args[0], args[1]);

            #if DEBUG
                Console.ReadLine();
            #endif
        }
    }
}
