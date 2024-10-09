using AutoMapper;
using CRM_App_Core.DTOs;
using CRM_App_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App_Service
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<NoContent, User>().ReverseMap();
            CreateMap<ClientDto, ClientInfo>().ReverseMap();
            CreateMap<MeetingDto, Meeting>().ReverseMap();
        }

    }
}
