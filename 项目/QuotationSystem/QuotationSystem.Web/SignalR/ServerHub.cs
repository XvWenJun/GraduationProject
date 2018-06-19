using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using QuotationSystem.Web.Concrete;
using System.Collections;

namespace QuotationSystem.Web.SignalR
{
    [HubName("ServerHub")]
    public class ServerHub : Hub
    {
        private static List<OnlineUser> UserList = new List<OnlineUser>();

        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                OnlineUser user = UserList.Where(model => model.ContextId == Context.ConnectionId).FirstOrDefault();
                UserList.Remove(user);
            }
            return base.OnDisconnected(stopCalled);
        }

        public void AddOnlineUser(int id)
        {
            OnlineUser user = new OnlineUser { id = id, ContextId = Context.ConnectionId };
            if (UserList.Where(model => model.id == id).Count() == 0)
            {
                UserList.Add(user);
            }
        }

        /// <summary>
        /// 供客户端调用的服务器端代码
        /// </summary>
        /// <param name="message"></param>
        public void AddNotice(int id)
        {
            IEnumerable<OnlineUser> users = UserList.Where(model => model.id == id);
            foreach (var user in users)
            {
                Clients.Client(user.ContextId).updateNotices();
            }
        }
    }
}