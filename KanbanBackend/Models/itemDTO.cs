namespace KanbanBackend.Models
{
    public class itemDTO
    {
        public int id { get; set; }
        public int p_id { get; set; }
        public string type { get; set; }
        public int priority { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}