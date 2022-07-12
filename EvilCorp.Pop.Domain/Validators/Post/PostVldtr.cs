using EvilCorp.Pop.Domain.Aggregates.Post;
using FluentValidation;

namespace EvilCorp.Pop.Domain.Validators.Post
{
    public class PostVldtr:AbstractValidator<Aggregates.Post.Post>
    {
        public PostVldtr()
        {
            RuleFor(p => p.TextContent)
                  .NotNull().WithMessage("Comment text should not be null")
                  .NotEmpty().WithMessage("Comment text should not be empty");
        }
    }
}
