using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boxing.Models
{
    public class Folder
    {
        public List<File> Files { get; set; }
        public string Name { get; set; }

    }
}