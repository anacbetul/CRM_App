using CRM_App_Core.Services;
using CRM_App_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Security.AccessControl;
using CRM_App_Core.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace CRM_App.Controllers
{
    [Authorize(Roles = "manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : CustomBaseController
    {
        private readonly IServiceGeneric<ClientInfo, ClientDto> _clientService;
        private readonly IServiceGeneric<ClientInfo, ClientCreateDto> _clientCreateService;

        public ClientController(IServiceGeneric<ClientInfo, ClientDto> clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return ActionResultInstance(await _clientService.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> SaveClient(ClientCreateDto clientCreateDto)
        {
            return ActionResultInstance(await _clientCreateService.AddAsync(clientCreateDto));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientDto clientDto)
        {
            return ActionResultInstance(await _clientService.Update(clientDto,clientDto.Id));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            return ActionResultInstance(await _clientService.Remove(id));
        }

    }
}
