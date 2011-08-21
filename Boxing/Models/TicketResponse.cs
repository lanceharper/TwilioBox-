using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boxing.Models
{
    public class TicketResponse
    {
        public virtual string Status { get; set; }
        public virtual string Ticket { get; set; }
    }
}