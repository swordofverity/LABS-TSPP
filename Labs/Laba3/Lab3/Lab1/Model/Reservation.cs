using System;

namespace Lab1.Model
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public Event Event { get; set; }
        public Customer Customer { get; set; }
        public int NumTickets { get; set; }
        public decimal AmtPaid { get; set; }
        public string PayMethod { get; set; }
        public string CardNo { get; set; }
        public DateTime CardExp { get; set; }
        public string PayNotes { get; set; }
        public DateTime PurgeDate { get; set; }
        public bool Paid { get; set; }

        public virtual int EventID { get; set; }
        public virtual int CustomerID { get; set; }       
    }
}
