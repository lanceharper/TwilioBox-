using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boxing.Models
{
    public class TreeResponse
    {
        public virtual string Status { get; set; }
        public virtual Tree Tree { get; set; }
    }
}