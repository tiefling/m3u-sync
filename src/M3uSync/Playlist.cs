using System;
using System.IO;
using System.Text;

namespace M3uSync
{
    public class Playlist
    {
        public const string FilePattern = "*.m3u";
        private readonly string _fullFileName;
        

        #region | Construction |

        public Playlist(string sourceFile)
        {
            _fullFileName = sourceFile;
            FileName = Path.GetFileName(sourceFile);
        }

        #endregion


        public string FileName { get; set; }


        public void Delete()
        {
            File.Delete(_fullFileName);
        }

        public Playlist CopyTo(string sourcePath)
        {
            var destination = Path.Combine(sourcePath, FileName);
            File.Copy(_fullFileName, destination);

            return new Playlist(destination);
        }

        public override string ToString()
        {
            return FileName;
        }
        
        public Playlist WriteToNewPlaylist(string toPath, Func<string, string> rewriteOperation)
        {
            var output = new StringBuilder();

            using (var streamReader = new StreamReader(_fullFileName))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var newLine = rewriteOperation(line);

                    if (!String.IsNullOrEmpty(newLine))
                        output.AppendLine(newLine);
                }
            }

            var newFilePath = Path.Combine(toPath, FileName);
            using (var inputStream = new FileStream(newFilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                var streamWriter = new StreamWriter(inputStream);
                streamWriter.Write(output);
                streamWriter.Close();
                inputStream.Close();
            }

            return new Playlist(newFilePath);
        }

    }
}
