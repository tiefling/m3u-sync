using System.Collections.Generic;

namespace M3uSync
{
    public abstract class PlaylistRewriterBase
    {

        #region | Construction |

        protected PlaylistRewriterBase(PlaylistCollection playlistsToRewrite, string destinationPath)
        {
            Playlists = playlistsToRewrite;
            Destination = destinationPath;

            ElementsToRemove = new List<string>();
            ElementsToReplace = new List<ReplacementPair>();
        }

        #endregion

        protected PlaylistCollection Playlists { get; private set; }
        protected string Destination { get; private set; }

        public List<string> ElementsToRemove { get; private set; }
        public List<ReplacementPair> ElementsToReplace { get; private set; }
        public string PrependedEntryPath { get; set; }


        public PlaylistCollection Rewrite()
        {
            return  Playlists.WriteToDestination(Destination, RewriteOperation);
        }


        protected virtual string RewriteOperation(string line)
        {
            var outputLine = PrependedEntryPath + line;

            if (ElementsToRemove.Count > 0)
            {
                foreach (var element in ElementsToRemove)
                {
                    outputLine = outputLine.Replace(element, string.Empty);
                }
            }

            if (ElementsToReplace.Count > 0)
            {
                foreach (var element in ElementsToReplace )
                {
                    outputLine = outputLine.Replace(element.Original, element.Replacement);
                }
            }

            return outputLine;
        }

    }
}
