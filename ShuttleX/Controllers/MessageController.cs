using LogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public IActionResult Post(int userId,int chatId,string content)

        {            
            this._messageService.cr
            return Ok(this._messageService.GetAll());
        }

    }
}
