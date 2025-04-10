using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Presentation_WebApp.Hubs;

//[Authorize]
[AllowAnonymous]
public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    public async Task Typing()
    {
        string userName = Context.User?.Identity?.Name ?? "Anonymous";
        await Clients.Others.SendAsync("UserTyping", userName);
    }

    public async Task SendMessageToAll(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return; // Prevent empty messages

        string userName = Context.User?.Identity?.Name ?? "Anonymous";
        DateTime timestamp = DateTime.Now; // Use UTC for consistency

        await Clients.All.SendAsync("ReceiveMessage", userName, message, timestamp);
    }
}
