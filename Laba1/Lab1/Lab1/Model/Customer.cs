using System;
using System.Collections.Generic;

namespace Lab1.Model
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Company { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string FAX { get; set; }
        public double TaxRate { get; set; }
        public string Contact { get; set; }
        public DateTime LastInvoiceDate { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}