using ShowMeTheTickets.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShowMeTheTickets.Infastructure
{
    public class Sorter
    {
        public static List<SearchVM> SortBy(List<SearchVM> Model, string SortKey)
        {
            switch (SortKey)
            {
                case "Alphabetical":
                    Model.Sort((x, y) => x.Title.CompareTo(y.Title));
                    return Model;
                case "City":
                    Model.Sort((x, y) => x.City.CompareTo(y.City));
                    return Model;
                case "Country":
                    Model.Sort((x, y) => x.PriceValue.CompareTo(y.PriceValue));
                    Model.Sort((x, y) => x.Country.CompareTo(y.Country));
                    return Model;
                case "Date":
                    Model.Sort((x, y) => x.Date.CompareTo(y.Date));
                    return Model;
                case "Price":
                    Model.Sort((x, y) => x.PriceValue.CompareTo(y.PriceValue));
                    return Model;
                default:
                    return Model;
            }
        }

        public static List<TicketInfo> SortTicketsBy(List<TicketInfo> Model, string SortKey)
        {
            switch (SortKey)
            {
                case "Alphabetical":
                    Model.Sort((x, y) => x.SeatSection.CompareTo(y.SeatSection));
                    return Model;
                case "NumberOfTickets":
                    Model.Sort((x, y) => x.NumberOfTickets.CompareTo(y.NumberOfTickets));                    
                    return Model;
                case "Price":
                    Model.Sort((x, y) => x.Price.CompareTo(y.Price));
                    return Model;
                case "Type":
                    Model.Sort((x, y) => x.TicketType.CompareTo(y.TicketType));
                    return Model;
                default:
                    return Model;
            }
        }
    }
}