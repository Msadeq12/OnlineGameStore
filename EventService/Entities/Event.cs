﻿namespace EventService.Entities
{
    public class Event
    {
        public int eventID { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
