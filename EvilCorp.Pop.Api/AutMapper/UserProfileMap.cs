using AutoMapper;
using EvilCorp.Pop.Api.Contracts.UserProfile.Requests;
using EvilCorp.Pop.Api.Contracts.UserProfile.Responses;
using EvilCorp.Pop.Application.UserProfile.Commands;
using EvilCorp.Pop.Domain.Aggregates.UserProfile;

namespace EvilCorp.Pop.Application.Mapper
{
    internal class UserProfileMap:Profile
    {
        public UserProfileMap()
        {
            CreateMap<UserProfileRqst, CreateUserProfileCmd>();
            CreateMap<UserProfileRqst, UpdateBasicInfoCmd>();
            CreateMap<Domain.Aggregates.UserProfile.UserProfile, UserProfileRspn>();
            CreateMap<BasicInfo, BasicInfoRspn>();
        }
    }
}
