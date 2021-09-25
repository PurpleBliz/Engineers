using Engineers.Api.Models;

namespace Engineers.ViewModels
{
    public class CreateUserViewModel : ApiUser
    {
        public string Password { get; set; }
    }
}