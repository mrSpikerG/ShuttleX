using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using LogicLayer.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace ShuttleX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this._userService.GetAll());
        }

        // GET api/<ChatController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(this._userService.GetById(id));
        }

        // POST api/<ChatController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO user)
        {
            try
            {
                var newUser = new User()
                {
                    Age = user.Age,
                    Login = user.Login,
                    Password = user.Password,
                    Name = user.Name,
                    Surname = user.Surname
                };
                this._userService.CreateUser(newUser);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return Ok();
        }

        // PUT api/<ChatController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDTO user)
        {
            try
            {
                var newUser = new User()
                {
                    Age = user.Age,
                    Login = user.Login,
                    Password = user.Password,
                    Name = user.Name,
                    Surname = user.Surname
                };
                this._userService.UpdateUser(newUser);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return Ok();
        }

        // DELETE api/<ChatController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var founded = this._userService.GetById(id);
                this._userService.DeleteUser(founded);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return Ok();
        }
    }
}
