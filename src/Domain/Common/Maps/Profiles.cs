using AutoMapper;
using Domain.Common.DTOs.Responses;
using Domain.Entities;

namespace Domain.Common.Maps;

public class Profiles:Profile
{
    public Profiles()
    {
        CreateMap<Address, AddressResponse>();

        CreateMap<Account, AccountResponse>()
            .ForMember(a=>a.Type,s=>s.MapFrom(src=>src.Type.ToString()));

        CreateMap<Card, CardResponse>()
            .ForMember(c=>c.State,s=>s.MapFrom(src=>src.State.ToString()))
            .ForMember(c => c.Type, s => s.MapFrom(src => src.Type.ToString()))
            .ForMember(c => c.Currency, s => s.MapFrom(src => src.Currency.ToString()))
            .ForMember(c => c.User, s => s.MapFrom(src => src.User.UserName))
            .ForMember(c => c.UserId, s => s.MapFrom(src => src.User.Id));

        CreateMap<Transaction, TransactionResponse>()
            .ForMember(t=>t.Type,s=>s.MapFrom(src=>src.Type.ToString()))
            .ForMember(t => t.User, s => s.MapFrom(src => src.User.UserName))
            .ForMember(t => t.UserId, s => s.MapFrom(src => src.User.Id));

        CreateMap<Vendor, VendorResponse>();

        CreateMap<User, UserResponse>();
    }
}
