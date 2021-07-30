using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Task6.Hubs
{
    public class GameHub : Hub
    {
        public async Task CheckOpp(string groupName)
        {
            await Clients.Group(groupName).SendAsync("CheckOpp");
        }

        public async Task RemoveFromGame(string Name)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Name);
        }

        public async Task Move(int id_cell, string groupName)
        {
            await Clients.OthersInGroup(groupName).SendAsync("Move", id_cell);
        }

        public async Task AddToGame(string Name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Name);
        }
    }
}
