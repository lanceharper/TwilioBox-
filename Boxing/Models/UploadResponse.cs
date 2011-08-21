using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boxing.Models
{
    public class UploadResponse
    {
        public virtual string Status { get; set; }
        public List<File> Files { get; set; }
    }
}