using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Notification.Service.Hubs;

public class NotificationsHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        if (this.Context.User.Identity.IsAuthenticated)
        {
            await this.Groups.AddToGroupAsync(
                this.Context.ConnectionId,
                Constants.AuthenticatedUsersGroup);
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await this.Groups.RemoveFromGroupAsync(
            this.Context.ConnectionId,
            Constants.AuthenticatedUsersGroup);
    }
}
