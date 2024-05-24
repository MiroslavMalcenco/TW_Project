namespace Gamma.Domain.Entities.Post
{
    public class PListingFilterData
    {
        public string KeyWord { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string Cuisine { get; set; }
        public int MinCalories { get; set; }
        public int MaxCalories { get; set; }
        public string Ingredients { get; set; }
        public string ChefSpeciality { get; set; }
        public string DietaryRestrictions { get; set; }
    }
}