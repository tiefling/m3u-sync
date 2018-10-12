using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace M3uSync
{
    public class PlaylistCollection : ReadOnlyCollection<Playlist>
    {

        #region | Construction |

        public PlaylistCollection(string directoryPath)
            : this(directoryPath, LoadPlaylists(directoryPath))
        { }

        private PlaylistCollection(string directoryPath, IList<Playlist> playlists)
            : base(playlists)
        {
            DirectoryPath = directoryPath;
        }

        #endregion


        public string DirectoryPath { get; private set; }


        public PlaylistCollection WriteToDestination(string tempPath, Func<string, string> rewriteOperation)
        {
            var tempPlaylists = this.Select(file => file.WriteToNewPlaylist(tempPath, rewriteOperation)).ToList();
            return new PlaylistCollection(tempPath, tempPlaylists);
        }


        #region | Private Methods |

        private static IList<Playlist> LoadPlaylists(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath, Playlist.FilePattern);

            return files.Select(file => new Playlist(file)).ToList();
        }

        #endregion


        internal void ReplaceWith(PlaylistCollection newPlaylists)
        {
            Delete();

            foreach (var playlist in newPlaylists)
            {
                playlist.CopyTo(DirectoryPath);
            }
        }

        public void Delete()
        {
            var failedPlaylists = new StringBuilder();

            foreach (var playlist in this)
            {
                try
                {
                    playlist.Delete();
                }
                catch (Exception ex)
                {
                    failedPlaylists.AppendLine("    " + playlist.FileName);
                }
            }

            if (failedPlaylists.Length > 0)
            {
                throw new IOException(string.Format("Unable to delete the following playlists from {0}...\n{1}\n\nThese will have to be removed manually.", DirectoryPath, failedPlaylists));
            }
        }
    }
}
