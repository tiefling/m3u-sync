
namespace M3uSync
{
    public class Mp3PlaylistFormatRewriter : PlaylistRewriterBase
    {
        private const string Mp3FileExtension = "mp3";

        #region | Construction |

        public Mp3PlaylistFormatRewriter(PlaylistCollection playlistsToRewrite, string destinationPath)
            : base(playlistsToRewrite, destinationPath)
        {
            ElementsToReplace.Add(new ReplacementPair("m4a", Mp3FileExtension));
            ElementsToReplace.Add(new ReplacementPair("M4A", Mp3FileExtension));
            ElementsToReplace.Add(new ReplacementPair("flac", Mp3FileExtension));
            ElementsToReplace.Add(new ReplacementPair("FLAC", Mp3FileExtension));
            ElementsToReplace.Add(new ReplacementPair("Flac", Mp3FileExtension));
        }

        #endregion

    }
}
