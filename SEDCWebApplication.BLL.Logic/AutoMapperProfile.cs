using AutoMapper;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.Data.Entities;
using System;

namespace SEDCWebApplication.BLL.Logic
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
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


            CreateMap<DAL.EntityFactory.Entities.Employee, EmployeeDTO>();          
                            //.ForMember(dest => dest.Role, src => src.MapFrom(src => src.RoleId));

            CreateMap<EmployeeDTO, DAL.EntityFactory.Entities.Employee>()          
                //.ForMember(dest => dest.RoleId, src => src.MapFrom(src => src.Role))
                .ForMember(dest => dest.Role, src => src.Ignore());

            CreateMap<CustomerDTO, DAL.EntityFactory.Entities.Customer>();
            CreateMap<DAL.EntityFactory.Entities.Customer, CustomerDTO>();

            CreateMap<ProductDTO, DAL.EntityFactory.Entities.Product>();
            CreateMap<DAL.EntityFactory.Entities.Product, ProductDTO>();
        }
    }
}
