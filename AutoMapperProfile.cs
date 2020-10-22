using AutoMapper;
using Iot_app.Dtos;
using Iot_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iot_app
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DHT22, GetDataDto>();
            CreateMap<AddDataDto, DHT22>();
            CreateMap<BME680, GetBMEDto>();
            CreateMap<AddBMEDto, BME680>();
        }
    }
}
