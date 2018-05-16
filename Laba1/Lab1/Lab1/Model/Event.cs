using System;
using System.Collections.Generic;

namespace Lab1.Model
{
    public class Event
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Photo { get; set; }
        public Venue Venue { get; set; }
        public List<Reservation> Reservations { get; set; }

        public virtual int VenueID { get; set; }
    }
}
