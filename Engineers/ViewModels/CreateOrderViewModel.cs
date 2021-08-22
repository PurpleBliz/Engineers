using Engineers.Models;
using System.ComponentModel.DataAnnotations;

namespace Engineers.ViewModels
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "��� ���� ������ ���� �����������")]
        [Display(Name = "��������")]
        public string Name { get; set; }

        [Required(ErrorMessage = "��� ���� ������ ���� �����������")]
        [Display(Name = "��������")]
        public string Description { get; set; }

        [Required(ErrorMessage = "��� ���� ������ ���� �����������")]
        [Display(Name = "����")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "��� ���� ������ ���� �����������")]
        [Display(Name = "������������")]
        public string UserName { get; set; } = Roles.ADMIN_EN;
    }
}
