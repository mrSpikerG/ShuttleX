using LogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ShuttleX.Controllers
{
    [Route("api/[controller]/[action]")]
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

        [HttpGet("GetAvailableChatsByName")]
        public IActionResult Get(int userId, string chatName)
        {
            var test = _chatService.GetAvailableChatsByName(userId, chatName);
            return Ok(test);
        }

        [HttpGet("GetUserChatsByName")]
        public IActionResult GetChats(int userId, string chatName)
        {
            var test = _chatService.GetUserChatsByName(userId, chatName);
            return Ok(test);
        }

        [HttpPut("Leave")]
        public IActionResult Leave(int userId, int chatId)
        {
            _chatService.LeaveFromChat(userId, chatId);
            return Ok();
        }

        [HttpPut("Join")]
        public IActionResult Join(int userId, int chatId)
        {
            _chatService.ConnectToChat(userId, chatId);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Delete(int userId,int chatId )
        {
            try
            {
                _chatService.DeleteChat(userId, chatId);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
