using Microsoft.AspNetCore.SignalR;

namespace PNTestApi.Hubs
{
    public class LocationsHub : Hub
    {
        public async Task SendRequestInfo(string message)
        {
            await Clients.All.SendAsync("ReceiveRequestInfo", message);
        }
    }
}
