using BusinessObjects = API.Entities;
using API.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace API.Implementations
{
    public class FileOperations : IFileOperations
    {
        public IEnumerable<BusinessObjects.Folder> GetLocations(string path)
        {
            List<BusinessObjects.Folder> logFolders = new List<BusinessObjects.Folder>();

            DirectoryInfo directory = new DirectoryInfo(path);
            DirectoryInfo[] directories = directory.GetDirectories();

            foreach (DirectoryInfo folder in directories)
                logFolders.Add(new BusinessObjects.Folder
                {
                    Name = folder.Name,
                    Path = $"{path}\\{folder.Name}"
                });

            return logFolders;
        }

        public BusinessObjects.Folder GetMatchedObjects(string folderPath, string folderName, string keyword)
        {
            return new BusinessObjects.Folder
            {
                Name = folderName,
                Files = FindMatches(folderPath, keyword)
            };
        }

        public string FindPID(string filePath, int lineNumber)
        {
            string result = string.Empty;

            string[] lines = File.ReadAllLines(HttpUtility.UrlDecode(filePath));

            int counter = lineNumber;
            while (result == string.Empty && counter > -1)
            {
                if (lines[counter].Contains("PID="))
                {
                    string line = lines[counter];
                    int start = line.IndexOf("PID=") + 4;
                    int end = line.IndexOf(" ", start);
                    result = line.Substring(start, end - start);
                    break;
                }

                counter--;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(lines[counter++]);

            while (!lines[counter].Contains("PID=") && counter < lines.Length)
            {
                sb.AppendLine(lines[counter++]);
            }

            return sb.ToString();
        }


        #region helpers
        private IEnumerable<BusinessObjects.File> FindMatches(string folderPath, string keyword)
        {
            List<BusinessObjects.File> files = new List<BusinessObjects.File>();
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.log"))
            {
                List<BusinessObjects.MatchDetails> matches = GetTextLocation(file, keyword).ToList();
                if (matches.Any())
                {
                    files.Add(new BusinessObjects.File
                    {
                        Name = Path.GetFileNameWithoutExtension(file),
                        Path = file,
                        Matches = matches
                    });
                }
            }

            return files;
        }

        private IEnumerable<BusinessObjects.MatchDetails> GetTextLocation(string path, string keyword)
        {
            List<BusinessObjects.MatchDetails> result = new List<BusinessObjects.MatchDetails>();

            int counter = 0;
            string line;

            StreamReader fileDoc = new StreamReader(path);
            while ((line = fileDoc.ReadLine()) != null)
            {
                if (line.Contains(keyword))
                {
                    result.Add(new BusinessObjects.MatchDetails
                    {
                        PositionX = line.IndexOf(keyword),
                        PositionY = counter
                    });
                }

                counter++;
            }

            fileDoc.Close();

            return result;
        }

        #endregion
    }
}
