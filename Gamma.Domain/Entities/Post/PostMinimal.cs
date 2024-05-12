using System;

namespace Gamma.Domain.Entities.Post
{
    public class PostMinimal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public int Calories { get; set; }
        public int SpicinessLevel { get; set; }
        public string Cuisine { get; set; }
        public string DietaryRestrictions { get; set; }
        public int Rating { get; set; }
        public string ChefSpeciality { get; set; }
        public int Price { get; set; }
        public DateTime DateAdded { get; set; }
        public string ImagePath { get; set; }
    }
}