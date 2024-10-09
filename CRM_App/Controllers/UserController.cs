using CRM_App_Core.DTOs;
using CRM_App_Core.Models;
using CRM_App_Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var response = await _userService.CreateUserAsync(createUserDto);

            // Yanıta göre HTTP durum kodunu ve mesajı ayarla
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            else
            {
                // Hataları döndür
                return BadRequest(response);
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return ActionResultInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUser()
        {
            return ActionResultInstance(await _userService.GetAllUserAsync());
        }
        


        [HttpPost("CreateUserRoles/{userName}")]
        public async Task<IActionResult> CreateUserRoles(string userName, int opt)
        {
            return ActionResultInstance(await _userService.CreateUserRoles(userName, opt));
        }


    }
} 
