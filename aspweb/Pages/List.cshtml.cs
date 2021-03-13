
using System.Collections.Generic;
using Core.Entities;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspweb.Pages
{
    public class ListModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        public IEnumerable<Restaurant> Restaurants { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public ListModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public void OnGet()
        {
            Restaurants = _restaurantData.GetByName(SearchTerm);
        }
    }
}