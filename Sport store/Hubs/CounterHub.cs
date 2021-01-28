using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Hubs
{
    public class CounterHub : Hub
    {
        private int count = 0;
        public override Task OnConnectedAsync()
        {
            count++;
            Clients.All.SendAsync("count", count);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            count--;
            Clients.All.SendAsync("count" , count);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
