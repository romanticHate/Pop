using EvilCorp.Pop.Domain.Aggregates.UserProfile;
using System.ComponentModel.DataAnnotations;

namespace EvilCorp.Pop.Api.Contracts.UserProfile.Requests
{
    public record UserProfileRqst
    {
        //[Required]
        //[MinLength(3)]
        //[MaxLength(50)]
        public string FirstName { get; set; }
        //[Required]
        //[MinLength(3)]
        //[MaxLength(50)]
        public string LastName { get; set; }
        ////[Required]
        ////[EmailAddress]
        ////[MaxLength(150)]
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        //[Required]
        public DateTime DateOfBirth { get; set; }
        //[Required]
        //[MaxLength(3)]
        public string CurrentCity { get; set; }
    }
}
