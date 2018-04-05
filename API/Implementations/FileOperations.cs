using API.Entities;
using API.Helpers;
using API.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Implementations
{
    public class FileOperations : IFileOperations
    {
        public IEnumerable<Folder> GetLocations(string path)
        {
            List<Folder> logFolders = new List<Folder>();

            DirectoryInfo directory = new DirectoryInfo(path);
            DirectoryInfo[] directories = directory.GetDirectories();

            foreach (DirectoryInfo folder in directories)
                logFolders.Add(new Folder
                {
                    Name = folder.Name,
                    Path = $"{path}\\{folder.Name}"
                });

            return logFolders;
        }

        public Folder GetMatchedObjects(string folderPath, string folderName, string keyword)
        {
            return new Folder
            {
                Name = folderName,
                Files = ParseHelper.FindMatches(folderPath, keyword)
            };
        }
    }
}
