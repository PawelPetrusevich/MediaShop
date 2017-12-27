using System;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<Notification, NotificationDto>().ReverseMap().ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now));
        }
    }
}
