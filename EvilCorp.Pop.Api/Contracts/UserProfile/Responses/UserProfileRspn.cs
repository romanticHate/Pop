namespace EvilCorp.Pop.Api.Contracts.UserProfile.Responses
{
    public record UserProfileRspn
    {
        public Guid UserProfileId { get; private set; }
        public BasicInfoRspn BasicInfo { get; set; }     
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }
}
