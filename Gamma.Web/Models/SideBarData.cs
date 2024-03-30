using System.Collections.Generic;

namespace Gamma.Web.Models
{
    public class SideBarData
    {
        public string KeyWord { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public List<int> PriceRange { get; set; }
        public string Cuisine { get; set; }
        public List<string> CuisineList { get; set; }
        public int MinCalories { get; set; }
        public int MaxCalories { get; set; }
        public List<int> CaloriesRange { get; set; }
        public string Type { get; set; }
        public string Ingredients { get; set; }
        public List<string> IngredientsList { get; set; }
        public string ChefSpeciality { get; set; }
        public string DietaryRestrictions { get; set; }
    }
}