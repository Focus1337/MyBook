using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MyBook.Services.Hubs;

[Authorize]
public class ChatHub : Hub
{
    public async Task Send(string message)
    {
        await Clients.All.SendAsync("Send", message);
    }
    // public async Task Send(string message)
    // {
    //     await Clients.All.SendAsync("Receive", message);
    // }
}