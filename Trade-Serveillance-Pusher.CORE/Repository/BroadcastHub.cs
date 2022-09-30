using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade_Serveillance_Pusher.CORE.Interfaces;

namespace Trade_Serveillance_Pusher.CORE
{
    public class BroadcastHub : Hub<IHubClient>
    {
    }
}
