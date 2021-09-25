using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Engineers.Models;
using Engineers.IService;

namespace Engineers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespondController : ControllerBase
    {
        private readonly IRespondService _respondService;

        public RespondController(IRespondService respondService)
        {
            _respondService = respondService;
        }

        [HttpGet("List")]
        public Response Get() => _respondService.Get();

        [HttpGet("ByUser")]
        public Response GetByUser(string userId) => _respondService.GetByUserId(userId);

        [HttpGet("Get/{id}")]
        public Response GetById(int id) => _respondService.GetById(id);

        [HttpPost("Delete")]
        public Response Delete(int id) => _respondService.Delete(id);
    }
}
