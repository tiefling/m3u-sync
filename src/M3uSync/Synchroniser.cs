using System;

namespace M3uSync
{
    public static class Synchroniser
    {
        internal static void Execute(string sourcePath, string destinationPath)
        {
            Console.WriteLine("Synchronising Playlists from '{0}' to '{1}'...", sourcePath, destinationPath);
            var rewriter = new Mp3PlaylistFormatRewriter(new PlaylistCollection(sourcePath), destinationPath);
            var output = rewriter.Rewrite();

            foreach (var playlist in output)
            {
                Console.WriteLine("Created Playlist: " + playlist);
            }

            Console.WriteLine("Sync completed");
        }
    }
}
