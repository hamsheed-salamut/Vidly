using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Vidly.Hubs
{
    public class NotificationHub : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}

        public static void Send()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

            context.Clients.All.displayStatus();
        }
    }
}