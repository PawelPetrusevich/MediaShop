using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MediaShop.WebApi
{
    public class NotificationHub : Hub
    {
        public override Task OnConnected()
        {
            var user = GetAuthenticatedUser();
            Clients.All.OnUserConnected(user);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        private string GetAuthenticatedUser()
        {
            var username = Context.QueryString["user"];
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new System.Exception("Failed to authenticate user.");
            }

            return username;
        }
    }
}