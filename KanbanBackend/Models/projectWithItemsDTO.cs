using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KanbanBackend.Models
{
    public class projectWithItemsDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string acronym { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public IQueryable<itemDTO> items { get; set; }

    }
}