﻿using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IFileOperations
    {
        IEnumerable<Folder> GetLocations(string path);

        Folder GetMatchedObjects(string folderPath, string folderName, string keyword);

        string FindPID(string filePath, int lineNumber);
    }
}
