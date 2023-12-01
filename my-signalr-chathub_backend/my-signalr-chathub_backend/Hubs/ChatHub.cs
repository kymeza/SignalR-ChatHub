using Microsoft.AspNetCore.SignalR;

namespace my_signalr_chathub_backend.Hubs
{
    public class ChatHub : Hub
    {
        public static Dictionary<string, string> Users { get; set; } = new Dictionary<string, string>();

        public async Task SetUsername(string username)
        {
            bool isTaken = Users.Any(p => p.Value == username);
            if (isTaken)
            {
                Context.Abort();
                return;
            }

            Users[Context.ConnectionId] = username;
            await Clients.All.SendAsync("OnJoin", DateTime.Now, username, Users.Count);
        }

        public async Task SendMessage(string message)
        {
            string username = Users[Context.ConnectionId];
            await Clients.All.SendAsync("NewMessage", DateTime.Now, username, message);
        }

        public override async Task OnDisconnectedAsync(Exception? exception) 
        {
            string username = Users[Context.ConnectionId];
            bool isTaken = Users.Count(p => p.Value == username) > 1;
            if (isTaken) { return; }
            Users.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("OnLeft", DateTime.Now, username, Users.Count);
        }


    }
}
