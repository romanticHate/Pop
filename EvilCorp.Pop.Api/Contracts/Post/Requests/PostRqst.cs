using System.ComponentModel.DataAnnotations;

namespace EvilCorp.Pop.Api.Contracts.Post.Requests
{
    public class PostRqst
    {       
        [Required]
        public string TextContent { get; set; }   
    }
}
