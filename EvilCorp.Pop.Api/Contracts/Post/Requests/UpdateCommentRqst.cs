using System.ComponentModel.DataAnnotations;

namespace EvilCorp.Pop.Api.Contracts.Post.Requests
{
    public class UpdateCommentRqst
    {
        [Required]
        public string Text { get; set; }
    }
}
