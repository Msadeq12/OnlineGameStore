namespace EventService.Models
{
    public class Event
    {
        public int eventID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
