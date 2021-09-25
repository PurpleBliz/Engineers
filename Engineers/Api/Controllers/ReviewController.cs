using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Engineers.Models;
using Engineers.Api.IService;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Engineers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewsService _reviewsService;

        public ReviewController(IReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpPost("Create")]
        public Response Create(Review review) => _reviewsService.Create(review);

        [HttpPost("GetByUser")]
        public Response GetByUser(string userId) => _reviewsService.GetByUser(userId);

        [HttpPost("GetByOrder")]
        public Response GetByOrder(int orderId) => _reviewsService.GetByOrder(orderId);
    }
}
