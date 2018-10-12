using System;
using System.Linq;

namespace M3uSync
{
    public static class Cleaner
    {
        internal static void Execute(string sourcePath, string tempPath)
        {
            Console.WriteLine("Please enter all playlist path elements to clean out, seperated by commas as appropriate:");
            var pathsInput = Console.ReadLine();

            Console.WriteLine("Please enter all playlist path elements to replace, seperated by commas as appropriate with replacements seperated by pipes (original1|replace1,original2|replace2):");
            var replaceInput = Console.ReadLine();

            Console.WriteLine("If you would like to enter a new base path for your entries enter it now, else press enter:");
            var newBasePath = Console.ReadLine();

            var originalPlaylists = new PlaylistCollection(sourcePath);
            var rewriter = new BasicM3UOnlyPlaylistRewriter(originalPlaylists, tempPath)
                               {PrependedEntryPath = newBasePath};

            if (!String.IsNullOrEmpty(pathsInput))
            {
                rewriter.ElementsToRemove.AddRange(pathsInput.Split(','));
            }

            if (!String.IsNullOrEmpty(replaceInput))
            {
                var pairStrings = (replaceInput.Split(','));
                foreach (var items in pairStrings.Select(pairString => pairString.Split('|')))
                {
                    rewriter.ElementsToReplace.Add(new ReplacementPair(items[0], items[1]));
                }
            }

            Console.WriteLine("Cleaning Playlist files at '{0}' using '{1}' as temporary storage", sourcePath, tempPath);
            PlaylistCollection newPlaylists = rewriter.Rewrite();

            foreach (var playlist in newPlaylists)
            {
                Console.WriteLine("Cleaned Playlist: " + playlist);
            }

            Console.WriteLine("Check your playlists are OK in the temp location ('{0}') and enter 'Y' to replace the original playlists:", tempPath);
            var input = Console.ReadLine();

            if (input == "Y" || input == "y")
                originalPlaylists.ReplaceWith(newPlaylists);
            else
                Console.WriteLine("Clean aborted");

            try
            {
                newPlaylists.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deletion Failed: " + ex.Message);
            }

            Console.WriteLine("Clean completed");
        }
    }
}
