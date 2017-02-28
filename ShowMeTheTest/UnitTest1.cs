using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShowMeTheTickets.Controllers;
using System.Web.Mvc;
using System.Threading.Tasks;
using ShowMeTheTickets.ViewModels;
using System.Collections.Generic;
using ShowMeTheTickets.Infastructure;

namespace ShowMeTheTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SortByPriceTest()
        {
            // Arrange
            List<SearchVM> searchModel = new List<SearchVM>();
            for(var i = 1; i < 5; i++)
            {
                searchModel.Add(new SearchVM(){ PriceValue = 5 - i });
            }

            // Act
            List<SearchVM> Sorted = Sorter.SortBy(searchModel, "Price");

            // Assert
            Assert.AreEqual(searchModel[0].PriceValue, 1);
        }
    }
}
