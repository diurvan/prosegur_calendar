using System;

namespace e_prosegur
{
    public class EventModel
    {
        public int id { get; set; }

        public String title { get; set; }

        public String start { get; set; }

        public String end { get; set; }

        public bool allDay { get; set; }
        public string email { get; set; }
        public int evaluacion { get; set; }
        public string nombre { get; set; }
    }
}
