using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly AppUserRepository _userRepo;
        protected APIResponse _response;

        public AppUserController(AppUserRepository userRepo)
        {
            _userRepo = userRepo;
            _response = new APIResponse();
        }

        //POST: api/AppUser/Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterationRequest model)
        {
            bool ifUserNameUnique = await _userRepo.IsUniqueAsync(model.Email, model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add("Username already exists");
                return BadRequest(_response);
            }

            var user = await _userRepo.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add("Error while registering");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // POST: api/AppUser/login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var tokens = await _userRepo.Login(model);
            if (tokens == null || string.IsNullOrEmpty(tokens.AccessToken))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.Data = tokens;
            return Ok(_response);
        }

        // Post: api/AppUser/Refresh
        [HttpPost("Refresh")]
        public async Task<IActionResult> GetNewTokenFromRefreshToken([FromBody] Tokens model)
        {
            if (ModelState.IsValid)
            {
                var tokens = await _userRepo.RefreshAccessToken(model);
                if (tokens == null || string.IsNullOrEmpty(tokens.AccessToken))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Errors.Add("Invalid token");
                    return BadRequest(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Data = tokens;
                return Ok(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add("Invalid token");
                return BadRequest(_response);
            }
        }

        // Post: api/AppUser/Logout
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] Tokens model)
        {
            if (ModelState.IsValid)
            {
                await _userRepo.RevokeRefreshToken(model);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Errors.Add("Invalid Input");
            return BadRequest(_response);
        }
    }
}
