
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SchockenAppBackend.Hubs
{
    public class LobbyHub : Hub
    {
        public async Task JoinLobby(string lobbyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
            await Clients.Group(lobbyId).SendAsync("PlayerJoined", Context.ConnectionId);
        }

        public async Task RollDice(string lobbyId, int roll1, int roll2, int roll3)
        {
            await Clients.Group(lobbyId).SendAsync("DiceRolled", Context.ConnectionId, roll1, roll2, roll3);
        }
    }
}
