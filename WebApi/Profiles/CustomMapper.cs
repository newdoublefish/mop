using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            CreateMap<UserCreateRequestDto, User>();
            CreateMap<DepartmentCreateRequestDto, Department>();
            CreateMap<Department, DepartmentResponseDto>();
        }
    }
}
