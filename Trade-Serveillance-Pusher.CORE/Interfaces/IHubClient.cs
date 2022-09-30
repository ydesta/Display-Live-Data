using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade_Serveillance_Pusher.CORE.Interfaces
{
   public interface IHubClient
    {
        Task BroadcastMessage();
    }
}
