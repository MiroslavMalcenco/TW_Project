using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gamma.Domain.Entities.Post
{
    public class PDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название блюда")]
        [StringLength(50, ErrorMessage = "Название слишком длинное")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите ингредиенты блюда")]
        [StringLength(50)]
        public string Ingredients { get; set; }
        [Required(ErrorMessage = "Введите калорийность блюда")]
        public int Calories { get; set; }
        [Required(ErrorMessage = "Введите рейтинг блюда")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Введите уровень остроты блюда")]
        public int SpicinessLevel { get; set; }
        public string ChefSpeciality { get; set; }
        [Required(ErrorMessage = "Введите диету и запреты")]
        public string DietaryRestrictions { get; set; }
        [Required(ErrorMessage = "Введите стоимость блюда")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Введите традиционную кухню")]
        public string Cuisine { get; set; } 
        public string Comment { get; set; } 
        public string ImagePath { get; set; } 
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public string Author { get; set; }
    }
}