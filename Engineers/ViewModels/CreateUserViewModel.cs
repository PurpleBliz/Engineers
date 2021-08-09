using Microsoft.AspNetCore.Http;
using Engineers.Models;

namespace Engineers.ViewModels
{
    public class CreateUserViewModel : ApiUser
    {
        public string Password { get; set; }
    }
}