using ShowMeTheTickets.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShowMeTheTickets.Infastructure
{
    public class FindCheapest
    {
        public static List<SearchVM> CheapestTickets(List<SearchVM> searchResults)
        {
            decimal cheapest = 0;
            foreach (var item in searchResults)
            {
                if(cheapest == 0)
                {
                    cheapest = item.PriceValue;
                } else if (item.PriceValue < cheapest)
                {
                    cheapest = item.PriceValue;
                }
            }

            foreach (var item in searchResults)
            {
                if(item.PriceValue == cheapest)
                {
                    item.Cheapest = true;
                }
            }
            return searchResults;
        }
    }
}