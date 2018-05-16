using System.Collections.Generic;

namespace Lab1.Model
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public byte[] Map { get; set; }
        public string Remarks { get; set; }

        public List<Event> Events { get; set; }
    }
}
