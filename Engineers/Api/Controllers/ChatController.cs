using Engineers.Context;
using Engineers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engineers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly ApplicationContext _context;

        public ChatController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("Send")]
        public Response Send(Message message)
        {
            return new Response
            {
                Code = 200,
                Text = "Ok",
                Data = message,
                Success = true
            };
        }

        [HttpGet("GetMessages")]
        public Response GetMessages(string userId)
        {
            return new Response
            {
                Code = 200,
                Text = "Ok",
                Data = _context.Messages.Where(m => m.UserId == userId).ToList(),
                Success = true
            };
        }
    }
}
