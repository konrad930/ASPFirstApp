
using System.Collections.Generic;
using Core.Entities;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace aspweb.Pages
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Cuisines { get; set; }
        public IHtmlHelper HtmlHelper { get; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            HtmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = HtmlHelper.GetEnumSelectList<CuisineType>();
            Restaurant = restaurantId.HasValue ? restaurantData.GetById(restaurantId.Value) : new Restaurant();
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = HtmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id == 0)
            {
                Restaurant = restaurantData.Add(Restaurant);
            }
            else
            {
                Restaurant = restaurantData.Update(Restaurant);
            }

            restaurantData.Commit();

            TempData["Message"] = "Restaurant saved !";
            return RedirectToPage("./Details", new { restaurantId = Restaurant.Id});
        }

    }
}