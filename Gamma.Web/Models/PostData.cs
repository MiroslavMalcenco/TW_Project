using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Gamma.Web.Models
{
    public class PostData
    {
        public const string errMsg = "Слишком большой ввод";
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название блюда")]
        [StringLength(50, ErrorMessage = errMsg)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите ингредиенты блюда")]
        [StringLength(50, ErrorMessage = errMsg)]
        public string Ingredients { get; set; }
        [Required(ErrorMessage = "Введите калорийность блюда")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Введите действительную калорийность")]
        public int Calories { get; set; }
        [Required(ErrorMessage = "Введите соответствие блюдам шеф-повара")]
        [StringLength(50, ErrorMessage = errMsg)]
        public string ChefSpeciality { get; set; }
        [Required(ErrorMessage = "Введите рейтинг блюда")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Введите действительный рейтинг")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Введите уровень остроты блюда")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Введите действительный уровень остроты")]
        public int SpicinessLevel { get; set; }
        [Required(ErrorMessage = "Введите диеты и запреты")]
        [StringLength(50, ErrorMessage = errMsg)]
        public string DietaryRestrictions { get; set; }
        [Required(ErrorMessage = "Введите цену блюда")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Введите действительную цену")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Введите традиционную кухню блюда")]
        [StringLength(50, ErrorMessage = errMsg)]
        public string Cuisine { get; set; }
        public string Comment { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateAdded { get; set; }
        public string Author { get; set; }
        public string AuthorPhoneNumber { get; set; }
    }
}