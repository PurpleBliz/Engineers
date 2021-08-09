using System.ComponentModel.DataAnnotations;

namespace Engineers.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "��� ��������")]
        public int Year { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "������")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "������ �� ���������")]
        [DataType(DataType.Password)]
        [Display(Name = "����������� ������")]
        public string PasswordConfirm { get; set; }
    }
}
