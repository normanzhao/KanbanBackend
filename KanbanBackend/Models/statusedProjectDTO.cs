using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KanbanBackend.Models
{
    public partial class statusedProjectDTO
    {
        public int id { get; set; }
        public string status { get; set; }
    }
}