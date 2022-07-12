using System.ComponentModel.DataAnnotations;

namespace EvilCorp.Pop.Api.Contracts.Post.Requests
{
    public class CommentRqst
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string UserProfileId { get; set; }
    }
}
