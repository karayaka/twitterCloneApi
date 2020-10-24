using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using twitterClone.Entity.DataModels.UserConnectionDataModels;
using twitterClone.Entity.Interfaces;

namespace twitterClone.WepApi.Hubs
{
    [Authorize]
    public class MessageHub:Hub
    {
        private IUserConnectionRepository UserConnectionRepository;

        public MessageHub(IUserConnectionRepository _UserConnectionRepository)
        {
            UserConnectionRepository = _UserConnectionRepository;
        }

        public Task SedMessageToAll(string message)
        {
             return Clients.All.SendAsync("ReceiveMessage",message);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage",message);
        }

        public Task SendMessagesToUserID(string connectionID,string message)
        {
            
            return Clients.Client(connectionID).SendAsync("ReceiveMessage", message);
        }
        public override Task OnConnectedAsync()
        {
            var CunnectedUser = new ConnectionManageModel();
            CunnectedUser.Name = Context.User.Identity.Name;
            CunnectedUser.SurName = Context.User.FindFirst(ClaimTypes.Surname).Value;
            CunnectedUser.ID = Convert.ToInt32(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            CunnectedUser.Email = Context.User.FindFirst(ClaimTypes.Email).Value;
            CunnectedUser.ConnectionID = Context.ConnectionId;
            var Users= UserConnectionRepository.AddConnectedUsers(CunnectedUser);

            var userStrig = JsonConvert.SerializeObject(Users);

            Clients.All.SendAsync("UserConnected", userStrig);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var Users = UserConnectionRepository.RemoveConnectedUsers(Context.ConnectionId);

            var userStrig = JsonConvert.SerializeObject(Users);
            Clients.All.SendAsync("UserDisConnected", userStrig);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
