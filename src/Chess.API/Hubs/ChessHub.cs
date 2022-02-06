using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Hubs;

[Authorize]
public class ChessHub : Hub
{
    
}