using AutoMapper;
using EvilCorp.Pop.Api.Contracts.Identity;
using EvilCorp.Pop.Application.Identity.Commands;

namespace EvilCorp.Pop.Api.AutMapper
{
    public class IdentityMap:Profile
    {
        public IdentityMap()
        {
            CreateMap<UserRegistration, RegisterIdentity>();
        }
    }
}
