namespace EvilCorp.Pop.Domain.Aggregates.UserProfile
{
    public  class UserProfile
    {
        public UserProfile()
        {

        }

        public Guid UserProfileId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }

        // Factory method
        public static UserProfile CreateUserProfile(string identityId, BasicInfo basicInfo)
        {
            // TO DO: add validation, error handling, error notification
            return new UserProfile
            {               
                IdentityId = identityId,
                BasicInfo = basicInfo,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
        }

        // Public methods
        public void UpdateBasicInfo(BasicInfo newInfo)
        {
            BasicInfo = newInfo;
            LastModified = DateTime.UtcNow;
        }
    }
}
