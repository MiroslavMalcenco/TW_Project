using System;

namespace Gamma.Web.Models
{
    public class PostListingData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public int Calories { get; set; }
        public string SpicinessLevel { get; set; }
        public string Cuisine { get; set; }
        public string DietaryRestrictions { get; set; }
        public int Rating { get; set; }
        public string ChefSpeciality { get; set;}
        public DateTime DateAdded { get; set; }
    }
}