using System.ComponentModel.DataAnnotations;

namespace Gamma.Domain.Entities.User
{
    public class UChangePasswordData
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите старый пароль")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Пароль слишком короткий")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Введите новый пароль")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Пароль слишком короткий")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Подтвердите новый пароль")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Пароль слишком короткий")]
        public string ConfirmedPassword { get; set; }
    }
}