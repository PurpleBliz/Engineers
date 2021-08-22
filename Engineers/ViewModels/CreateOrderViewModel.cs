using Engineers.Models;
using System.ComponentModel.DataAnnotations;

namespace Engineers.ViewModels
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "Ёто поле должно быть установлено")]
        [Display(Name = "Ќазвание")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ёто поле должно быть установлено")]
        [Display(Name = "ќписание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Ёто поле должно быть установлено")]
        [Display(Name = "÷ена")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "Ёто поле должно быть установлено")]
        [Display(Name = "ѕользователь")]
        public string UserName { get; set; } = Roles.ADMIN_EN;
    }
}
