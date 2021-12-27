using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickApp.Helpers
{
    public class FileResult
    {
        public FileResult(string name, string type, byte[] bytes)
        {
            Name = name;
            Type = type;
            Bytes = bytes;
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public byte[] Bytes { get; }
    }
}
