using LogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ShuttleX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public IActionResult Post(int userId, string chatName)
        {
            _chatService.CreateChat(userId, chatName);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(int userId)
        {
            _chatService.CreateChat(userId, chatName);
            return Ok();
        }

    }
}
