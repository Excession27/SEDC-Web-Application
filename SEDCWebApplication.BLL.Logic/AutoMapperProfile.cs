using AutoMapper;
using Microsoft.Extensions.Configuration;
using SEDCWebApplication.BLL.Logic.Helpers;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.Data.Entities;
using System;

namespace SEDCWebApplication.BLL.Logic
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile(IConfiguration configuration)
        {

            /*            CreateMap<Employee, EmployeeDTO>()
                            .ForMember(dest => dest.Email, src => src.MapFrom(src => src.UserName))
                                .ForMember(dest => dest.Role, src => src.MapFrom(src => src.RoleId));*/



            /*            CreateMap<EmployeeDTO, Employee>()
                            .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.Email))
                                .ForMember(dest => dest.RoleId, src => src.MapFrom(src => src.Role));*/

            /*            CreateMap<Customer, CustomerDTO>();*/
            /*            CreateMap<CustomerDTO, Customer>()
                            .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.Email))
                                .ForMember(dest => dest.ContactId, src => src.MapFrom(src => src.ContactId));*/

            /*            CreateMap<ProductDTO, Product>();
                        CreateMap<Product, ProductDTO>();*/


            // Entity Factory
            //CreateMap<DAL.EntityFactory.Entities.Employee, EmployeeDTO>();          
                            

            //CreateMap<EmployeeDTO, DAL.EntityFactory.Entities.Employee>()          
                //.ForMember(dest => dest.Role, src => src.Ignore());

            //CreateMap<CustomerDTO, DAL.EntityFactory.Entities.Customer>();
            //CreateMap<DAL.EntityFactory.Entities.Customer, CustomerDTO>();

            //CreateMap<ProductDTO, DAL.EntityFactory.Entities.Product>();
            //CreateMap<DAL.EntityFactory.Entities.Product, ProductDTO>();

            //Database Factory
            CreateMap<CustomerDTO, DAL.DatabaseFactory.Entities.Customer>();
            CreateMap<DAL.DatabaseFactory.Entities.Customer, CustomerDTO>();

            CreateMap<ProductDTO, DAL.DatabaseFactory.Entities.Product>();
            CreateMap<DAL.DatabaseFactory.Entities.Product, ProductDTO>();

            CreateMap<DAL.DatabaseFactory.Entities.Employee, EmployeeDTO>();          
            CreateMap<EmployeeDTO, DAL.DatabaseFactory.Entities.Employee>()          
                            .ForMember(dest => dest.RoleId, src => src.Ignore());

            CreateMap<DAL.DatabaseFactory.Entities.User, UserDTO>().ForMember(dest => dest.Role, src => src.MapFrom(src => EnumHelper.GetString<RoleEnum>(src.RoleId))); ;
            //CreateMap<UserDTO, DAL.DatabaseFactory.Entities.User>();
        }
    }
}
