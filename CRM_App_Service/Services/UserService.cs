using CRM_App_Core.DTOs;
using CRM_App_Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using User = CRM_App_Core.Models.User;

namespace CRM_App_Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMeetingService _meetingService;
        private readonly UserService _userService;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMeetingService meetingService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _meetingService = meetingService;
        }

        public async Task<Response<NoContent>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                UserName = createUserDto.UserName,
                Email = createUserDto.Email
            };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<NoContent>.Fail(new ErrorDto(errors, true), 400);
            }

            return Response<NoContent>.Success(ObjectMapper.Mapper.Map<NoContent>(user), 200);
        }

        public async Task<Response<NoContent>> CreateUserRoles(string userName, int opt)
        {
            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new() { Name = "admin" });
                await _roleManager.CreateAsync(new() { Name = "manager" });

            }
            var user = await _userManager.FindByNameAsync(userName);
            if(opt == 1)
            {
                await _userManager.AddToRoleAsync(user, "admin");

            }else if(opt == 2) {
                await _userManager.AddToRoleAsync(user, "manager");
            
            }

            return Response<NoContent>.Success(StatusCodes.Status201Created);
        }

        

        public async Task<Response<NoContent>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Response<NoContent>.Fail("User not found", 404, true);
            }

            return Response<NoContent>.Success(ObjectMapper.Mapper.Map<NoContent>(user), 200);
        }
        
        public async Task<IEnumerable<User>> GetUpcomingMeetingsAsync()
        {
            var upcomingMeetings = await _meetingService.GetUpcomingMeetingsAsync();

            // Get the distinct users who have upcoming meetings
            var userIdsWithMeetings = upcomingMeetings.Select(m => m.UserId).Distinct();

            var usersWithMeetings = new List<User>();

            foreach (var userId in userIdsWithMeetings)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    usersWithMeetings.Add(user);
                }
            }

            return usersWithMeetings;
        }

        public async Task<Response<List<User>>> GetAllUserAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return Response<List<User>>.Success(users, 200);
        }
    }
}
