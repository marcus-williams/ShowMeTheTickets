using HalKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShowMeTheTickets.ViewModels
{
    public class SearchVM
    {
        public string SearchTerm { get; set; }
        public string Title { get; set; }
        public string EventLink { get; set; }
        public int SearchIndex { get; set; }
        public DateTimeOffset Date { get; set; }
        public string MinimumPrice { get; set; }
        public decimal PriceValue { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Boolean Cheapest { get; set; }
    }
}