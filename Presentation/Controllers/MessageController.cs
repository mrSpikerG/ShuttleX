using LogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ShuttleX.SignalR;

namespace ShuttleX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IActionResult Get(int userId, int chatId)
        {
            try
            {

                var temp = this._messageService.GetMessagesByChat(userId, chatId);

                return Ok(temp);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
