using GogoKit;
using GogoKit.Models.Request;
using GogoKit.Models.Response;
using HalKit.Models.Response;
using Newtonsoft.Json;
using ShowMeTheTickets.Infastructure;
using ShowMeTheTickets.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShowMeTheTickets.Controllers
{

    public class HomeController : Controller
    {
        private Auth AuthSession = new Auth();
        
        public async Task<ViewResult> Index()
        {
            OAuth2Token token = await AuthSession.InitializeToken();
            return View("Index");
        }

        // Currently Not In Use
        public async Task<ActionResult> Category()
        {
            var category = await AuthSession._viagogoClient.Categories.GetAllGenresAsync();
            ViewBag.category = category;
            return View(category);
        }

        public async Task<ActionResult> Search(string SearchTerm = "FC Barcelona tickets", string SortBy = "Alphabetical")
        {
            ViewBag.NoEvents = "";
            // Query API for search term
            var searchResults = await AuthSession._viagogoClient.Search.GetAsync(SearchTerm);
            
            // Create a new instance of the ViewModel
            List<SearchVM> ViewModel = new List<SearchVM>();
            int counter = 0;
            
            // Loop through each of the search results
            foreach (var eventx in searchResults.Items)
            {
                if (eventx.Type == "Event")
                {
                    var evnt = await AuthSession._viagogoClient.Hypermedia.GetAsync<Event>(eventx.EventLink);
                    ViewModel.Add(new SearchVM { SearchTerm = SearchTerm,
                        Title = eventx.Title,
                        EventLink = JsonConvert.SerializeObject(eventx.EventLink),
                        SearchIndex = counter,
                        Date = (DateTimeOffset)evnt.StartDate,
                        MinimumPrice = evnt.MinTicketPrice.Display,
                        PriceValue = (decimal)evnt.MinTicketPrice.Amount,
                        City = evnt.Venue.City,
                        Country = evnt.Venue.Country.Name,
                        Cheapest = false
                    });
                    counter++;
                }
            }

            // Sort List of Events
            List<SearchVM> SortedViewModel = Sorter.SortBy(ViewModel, SortBy);
            List<SearchVM> SortCheapViewModel = FindCheapest.CheapestTickets(SortedViewModel);

            if (counter == 0) { ViewBag.NoEvents = "There are no events listed for " + SearchTerm + "."; }
            return View(SortCheapViewModel);
        } // END OF SEARCH METHOD



        public async Task<ActionResult> FindEvent(string EventLink, string SortBy)
        {
            // Convert to Link Object
            Link EventLinkObj = JsonConvert.DeserializeObject<Link>(EventLink);
            
            // Query API for Event Link
            var evnt = await AuthSession._viagogoClient.Hypermedia.GetAsync<Event>(EventLinkObj);
            var TicketList = await AuthSession._viagogoClient.Listings.GetAllByEventAsync((int)evnt.Id);

            // Construct a ViewModel
            EventVM ViewModel = new EventVM
            {
                EventId = (int)evnt.Id,
                EventLink = EventLink,
                Title = evnt.Name,
                StartDate = evnt.StartDate,
                MinimumPrice = evnt.MinTicketPrice.Display,
                VenueId = (int)evnt.Venue.Id,
                VenueName = evnt.Venue.Name,
                City = evnt.Venue.City,
                Country = evnt.Venue.Country.Name,
                Tickets = new List<TicketInfo>()
            };

            foreach (var item in TicketList)
            {
                TicketInfo NewInfo = new TicketInfo()
                {
                    Id = (int)item.Id,
                    NumberOfTickets = (int)item.NumberOfTickets,
                    SeatSection = item.Seating.Section,
                    TicketType = item.TicketType.Name,
                    Price = item.TicketPrice.Display
                };
                ViewModel.Tickets.Add(NewInfo);
            }

            // Sort Ticket Listings
            ViewModel.Tickets = Sorter.SortTicketsBy(ViewModel.Tickets, SortBy);

                return View(ViewModel);
        } // END OF FINDEVENT METHOD

    } // END OF CLASS
} // END OF NAMESPACE