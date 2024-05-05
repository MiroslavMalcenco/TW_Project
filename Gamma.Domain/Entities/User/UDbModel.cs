using Gamma.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamma.Domain.Entities.User
{
    public class UDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Введите ваше полное имя")]
        [StringLength(25, ErrorMessage ="Полное имя слишком длинное")]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Введите вашу электронную почту")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Электронная почта слишком длинная")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Электронная почта неверна")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 8, ErrorMessage ="Пароль слишком длинный")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Введите имя пользователя")]
        [StringLength(30, MinimumLength = 5, ErrorMessage ="Имя пользователя слишком длинное")]
        public string UserName { get; set; }
        public string RegisterIP { get; set; }
        [Required(ErrorMessage ="Согласитесь со всеми условиями и требованиями")]
        public bool Terms { get; set; }
        [DataType(DataType.Date)]
        public DateTime LoginDateTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegisterDateTime { get; set; }
        [Required]
        public URole AccessLevel { get; set; }
        public string PhoneNumber { get; set; }
    }
}