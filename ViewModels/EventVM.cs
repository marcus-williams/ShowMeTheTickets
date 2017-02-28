using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShowMeTheTickets.ViewModels
{
    public class EventVM
    {
        // Event
        public string Title { get; set; }
        public int EventId { get; set; }
        public string EventLink { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public string MinimumPrice { get; set; }

        // Venue
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // Ticket Listings
        public List<TicketInfo> Tickets { get; set; }
    }

    public class TicketInfo
    {
        public int Id { get; set; }
        public string SeatSection { get; set; }
        public int NumberOfTickets { get; set; }
        public string Price { get; set; }
        public string TicketType { get; set; }
    }
}