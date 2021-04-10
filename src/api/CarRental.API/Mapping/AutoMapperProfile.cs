using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Core.Entities.Concrete;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;

namespace CarRental.API.Mapping
{
    public class AutoMapperProfile: Profile
    {

        public AutoMapperProfile()
        {
            // Domain to Resource Mappings
            CreateMap<Car, CarAddDto>();
            CreateMap<Rental, AppUserDto>();
               

            CreateMap<CreditCard, AppUserDto>();

            //CreateMap<CreditCardDto, RentalWithCreditCardDto>();
            //CreateMap<RentalAddDto, RentalWithCreditCardDto>();



            // Resource to Domain Mappings;
            CreateMap<CarAddDto, Car>();
            CreateMap<AppUserDto, Rental>()
                .ForMember(x => x.RentDate, opt => opt.MapFrom(r => Convert.ToDateTime(r.RentDate)))
                .ForMember(x => x.RentDate, opt => opt.MapFrom(r => Convert.ToDateTime(r.RentDate)));

            CreateMap<AppUserDto, CreditCard>();
            CreateMap<UserDataDto, AppUser>();
            CreateMap<UserDataDto, Customer>();


            //CreateMap<RentalWithCreditCardDto, CreditCardDto>();
            //CreateMap<RentalWithCreditCardDto ,RentalAddDto>();

        }
    }
}
