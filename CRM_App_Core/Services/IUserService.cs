using CRM_App_Core.DTOs;
using CRM_App_Core.Models;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Core.Services
{
    public interface IUserService
    {
        Task<Response<NoContent>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<NoContent>> GetUserByNameAsync(string userName);
        Task<Response<List<User>>> GetAllUserAsync();
        Task<Response<NoContent>> CreateUserRoles(string userName, int opt);
        Task<IEnumerable<User>>GetUpcomingMeetingsAsync();
    }
}
