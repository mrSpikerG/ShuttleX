using LogicLayer.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ShuttleX.SignalR
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
        private readonly MessageService _messageService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ChatService chatService, MessageService messageService, ILogger<ChatHub> logger)
        {
            _chatService = chatService;
            _messageService = messageService;
            _logger = logger;
        }


        public async Task SendMessage(int chatId, int userId, string messageContent)
        {

            _messageService.CreateMessage(userId, chatId, messageContent);
            await Clients.Group($"chat_{chatId}").SendAsync("ReceiveMessage", userId, messageContent);
        }
        public async Task JoinChat(int chatId, int userId)
        {
            var groupName = $"chat_{chatId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("UserJoined", userId);
        }

        public async Task LeaveChat(int chatId, int userId)
        {
            var groupName = $"chat_{chatId}";
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("UserLeft", userId);
        }
    }
}