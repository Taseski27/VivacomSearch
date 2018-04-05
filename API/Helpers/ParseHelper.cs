using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessObjects = API.Entities;

namespace API.Helpers
{
    public static class ParseHelper
    {
        public static IEnumerable<BusinessObjects.File> FindMatches(string folderPath, string keyword)
        {
            List<BusinessObjects.File> files = new List<BusinessObjects.File>();
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.log"))
            {
                List<BusinessObjects.MatchDetails> matches = ParseHelper.GetTextLocation(file, keyword).ToList();
                if (matches.Any())
                {
                    files.Add(new BusinessObjects.File
                    {
                        Name = Path.GetFileNameWithoutExtension(file),
                        Matches = matches
                    });
                }
            }

            return files;
        }

        public static IEnumerable<BusinessObjects.MatchDetails> GetTextLocation(string path, string keyword)
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
    }
}
