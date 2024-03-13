using Gamma.BusinessLogic.DBModel;
using Gamma.Domain.Entities.Post;
using Gamma.Domain.Entities.Response;
using System.Collections.Generic;
using System.Linq;

namespace Gamma.BusinessLogic.Core
{
    public class PostApi
    {
        public ServiceResponse AddPost(PDbModel newPost)
        {
            var response = new ServiceResponse();
            try
            {
                using (var db = new PostContext())
                {
                    db.Posts.Add(newPost);
                    db.SaveChanges();
                }
                response.StatusMessage = "Обзор успешно сохранен";
                response.Status = true;
            }
            catch
            {
                response.StatusMessage = "При сохранении обзора возникла ошибка";
                response.Status = false;
            }
            return response;
        }
        public IEnumerable<PostMinimal> ReturnPostsBySearchWrapData(PSearchWrapData searchWrapData)
        {
            List<PostMinimal> list = new List<PostMinimal>();
            using (var db = new PostContext())
            {
                var results = db.Posts.Where(i => i.Ingredients.Contains(searchWrapData.IngredientsOrName) || i.Name.Contains(searchWrapData.IngredientsOrName));
                if (!string.IsNullOrEmpty(searchWrapData.PriceRange))
                {
                    var range = searchWrapData.PriceRange.Split('-');
                    int minPrice = int.Parse(range[0]);
                    int maxPrice = range.Length > 1 ? int.Parse(range[1]) : int.MaxValue;
                    results = results.Where(i => i.Price >= minPrice && i.Price < maxPrice);
                }
                if (!string.IsNullOrEmpty(searchWrapData.Cuisine))
                {
                    results = results.Where(i => i.Cuisine.Contains(searchWrapData.Cuisine));
                }
                foreach (var item in results)
                {
                    var postMinimal = new PostMinimal
                    {
                        Id = item.Id,
                        ChefSpeciality = item.ChefSpeciality,
                        Cuisine = item.Cuisine,
                        Price = item.Price,
                        Calories = item.Calories,
                        DateAdded = item.DateAdded,
                        Rating = item.Rating,
                        DietaryRestrictions = item.DietaryRestrictions,
                        Ingredients = item.Ingredients,
                        Name = item.Name,
                        SpicinessLevel = item.SpicinessLevel,
                        ImagePath = item.ImagePath
                    };
                    list.Add(postMinimal);
                }
            }
            return list.ToList();
        }
        public IEnumerable<PostMinimal> ReturnLatestPosts()
        {
            List<PostMinimal> list = new List<PostMinimal>();
            try
            {
                using (var db = new PostContext())
                {
                    var results = db.Posts.OrderByDescending(x => x.DateAdded).Take(4).ToList();
                    foreach (var item in results)
                    {
                        var postMinimal = new PostMinimal
                        {
                            Id = item.Id,
                            ChefSpeciality = item.ChefSpeciality,
                            Cuisine = item.Cuisine,
                            Price = item.Price,
                            Calories = item.Calories,
                            DateAdded = item.DateAdded,
                            Rating = item.Rating,
                            DietaryRestrictions = item.DietaryRestrictions,
                            Ingredients = item.Ingredients,
                            Name = item.Name,
                            SpicinessLevel = item.SpicinessLevel,
                            ImagePath = item.ImagePath
                        };
                        list.Add(postMinimal);
                    }
                }
                return list.ToList();
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<PostMinimal> ReturnPostsByIngredientsOrCuisine(string IngredientsOrCuisine)
        {
            List<PostMinimal> list = new List<PostMinimal>();
            using (var db = new PostContext())
            {
                var results = db.Posts.Where(i => i.Ingredients.Contains(IngredientsOrCuisine) || i.Cuisine.Contains(IngredientsOrCuisine));
                foreach (var item in results)
                {
                    var postMinimal = new PostMinimal
                    {
                        Id = item.Id,
                        ChefSpeciality = item.ChefSpeciality,
                        Cuisine = item.Cuisine,
                        Price = item.Price,
                        Calories = item.Calories,
                        DateAdded = item.DateAdded,
                        Rating = item.Rating,
                        DietaryRestrictions = item.DietaryRestrictions,
                        Ingredients = item.Ingredients,
                        Name = item.Name,
                        SpicinessLevel = item.SpicinessLevel,
                        ImagePath = item.ImagePath
                    };
                    list.Add(postMinimal);
                }
            }
            return list.ToList();
        }
        public IEnumerable<PostMinimal> ReturnPostsByAuthor(string author)
        {
            List<PostMinimal> list = new List<PostMinimal>();
            using (var db = new PostContext())
            {
                var results = db.Posts.Where(a => a.Author.Contains(author));
                foreach (var item in results)
                {
                    var postMinimal = new PostMinimal
                    {
                        Id = item.Id,
                        ChefSpeciality = item.ChefSpeciality,
                        Cuisine = item.Cuisine,
                        Price = item.Price,
                        Calories = item.Calories,
                        DateAdded = item.DateAdded,
                        Rating = item.Rating,
                        DietaryRestrictions = item.DietaryRestrictions,
                        Ingredients = item.Ingredients,
                        Name = item.Name,
                        SpicinessLevel = item.SpicinessLevel,
                        ImagePath = item.ImagePath
                    };
                    list.Add(postMinimal);
                }
            }
            return list.ToList();
        }
        public IEnumerable<PostMinimal> ReturnPostsByListingFilter(PListingFilterData filter)
        {
            List<PostMinimal> list = new List<PostMinimal>();
            using (var db = new PostContext())
            {
                var query = db.Posts.AsQueryable();
                if (!string.IsNullOrEmpty(filter.KeyWord))
                {
                    query = query.Where(c => c.Ingredients.Contains(filter.KeyWord) || c.Name.Contains(filter.KeyWord));
                }
                if (filter.MinPrice > 0 || filter.MaxPrice > 0)
                {
                    query = query.Where(c => c.Price >= filter.MinPrice && c.Price <= filter.MaxPrice);
                }
                if (!string.IsNullOrEmpty(filter.Cuisine) && filter.Cuisine != "Не указан")
                {
                    query = query.Where(c => c.Cuisine.Equals(filter.Cuisine));
                }
                if (filter.MinCalories > 0 || filter.MaxCalories > 0)
                {
                    query = query.Where(c => c.Calories >= filter.MinCalories && c.Calories <= filter.MaxCalories);
                }
                if (!string.IsNullOrEmpty(filter.Ingredients) && filter.Ingredients != "Не указан")
                {
                    query = query.Where(c => c.Ingredients.Equals(filter.Ingredients));
                }
                if (!string.IsNullOrEmpty(filter.ChefSpeciality) && filter.ChefSpeciality != "Не указан")
                {
                    query = query.Where(c => c.ChefSpeciality.Equals(filter.ChefSpeciality));
                }
                if (!string.IsNullOrEmpty(filter.DietaryRestrictions) && filter.DietaryRestrictions != "Не указан")
                {
                    query = query.Where(c => c.DietaryRestrictions.Equals(filter.DietaryRestrictions));
                }
                var results = query.ToList();
                foreach (var item in results)
                {
                    var postMinimal = new PostMinimal
                    {
                        Id = item.Id,
                        ChefSpeciality = item.ChefSpeciality,
                        Cuisine = item.Cuisine,
                        Price = item.Price,
                        Calories = item.Calories,
                        DateAdded = item.DateAdded,
                        Rating = item.Rating,
                        DietaryRestrictions = item.DietaryRestrictions,
                        Ingredients = item.Ingredients,
                        Name = item.Name,
                        SpicinessLevel = item.SpicinessLevel,
                        ImagePath = item.ImagePath
                    };
                    list.Add(postMinimal);
                }
            }
            return list.ToList();
        }
    }
}