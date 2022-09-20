using Microsoft.AspNetCore.Mvc;
using TennisBookings.Web.Controllers;
using TennisBookings.Web.Services;
using TennisBookings.Web.ViewModels;
using Xunit;

namespace TennisBookings.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        public IWeatherForecaster WeatherForecaster { get; }

        public HomeControllerTests(IWeatherForecaster weatherForecaster) 
        {
            this.WeatherForecaster = weatherForecaster;
        }

        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsSun()
        {
            var sut = new HomeController(WeatherForecaster);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("It's sunny right now", model.WeatherDescription);
        }

        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsRain()
        {
            var sut = new HomeController(WeatherForecaster);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("We're sorry but it's raining here.", model.WeatherDescription);
        }
    }
}
