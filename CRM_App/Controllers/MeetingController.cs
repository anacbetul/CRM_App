using CRM_App_Core.DTOs;
using CRM_App_Core.Models;
using CRM_App_Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_App.Controllers
{
    [Authorize(Roles = "manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : CustomBaseController
    {
        private readonly IServiceGeneric<Meeting, MeetingDto> _meetingService;
        private readonly IServiceGeneric<Meeting, MeetingCreateDto> _meetingCreateService;

        public MeetingController(IServiceGeneric<Meeting, MeetingDto> meetingService, IServiceGeneric<Meeting, MeetingCreateDto> meetingCreateService)
        {
            _meetingService = meetingService;
            _meetingCreateService = _meetingCreateService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMeetings()
        {
            return ActionResultInstance(await _meetingService.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> SaveMeeting(MeetingDto meetingDto)
        {
            return ActionResultInstance(await _meetingService.AddAsync(meetingDto));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMeeting(MeetingDto meetingDto)
        {
            return ActionResultInstance(await _meetingService.Update(meetingDto, meetingDto.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            return ActionResultInstance(await _meetingService.Remove(id));
        }

    }
}
