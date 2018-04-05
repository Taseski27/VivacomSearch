using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Entities
{
    public class File
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public IEnumerable<MatchDetails> Matches { get; set; }

        public File()
        {
            Matches = new List<MatchDetails>();
        }
    }
}
