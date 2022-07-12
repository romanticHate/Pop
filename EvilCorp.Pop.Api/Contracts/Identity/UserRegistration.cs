using System.ComponentModel.DataAnnotations;

namespace EvilCorp.Pop.Api.Contracts.Identity
{
    public class UserRegistration
    {
       
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(3)]
        public string CurrentCity { get; set; }

    }
}
