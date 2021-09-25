using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using Engineers.Models;
using System.Threading.Tasks;

namespace Engineers.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message) =>
            await Clients.User(message.UserId).SendAsync("mess", message);
    }
}
