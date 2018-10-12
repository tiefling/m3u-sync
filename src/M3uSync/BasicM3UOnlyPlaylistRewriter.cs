
namespace M3uSync
{
    /// <summary>
    /// Removes EXTINFO tags
    /// </summary>
    public class BasicM3UOnlyPlaylistRewriter : PlaylistRewriterBase
    {

        #region | Construction |

        public BasicM3UOnlyPlaylistRewriter(PlaylistCollection playlistsToRewrite, string destinationPath)
            : base(playlistsToRewrite, destinationPath)
        { }

        #endregion

        protected override string RewriteOperation(string line)
        {
            return line.StartsWith("#EXT") ? string.Empty : base.RewriteOperation(line);
        }
    }
}
