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
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public IActionResult Post(int userId,int chatId,string content)

        {            
            //this._messageService.cr
            return Ok(this._messageService.GetAll());
        }

    }
}
