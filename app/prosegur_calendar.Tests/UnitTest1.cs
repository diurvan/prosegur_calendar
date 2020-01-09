using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace prosegur_calendar.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var homeController = new prosegur_calendar.Controllers.HomeController();
            DateTime start = DateTime.Parse("2020-01-04");
            DateTime end = DateTime.Parse("2020-01-04");
            var result = homeController.GetEvents(start, end);
            Assert.NotNull(result);
        }
    }
}
