using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KanbanBackend.Models
{
    using System;
    using System.Collections.Generic;

    public partial class acronymedItem
    {
        public int id { get; set; }
        public string type { get; set; }
        public int priority { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string acronym { get; set; }

        public virtual project project { get; set; }
    }
}