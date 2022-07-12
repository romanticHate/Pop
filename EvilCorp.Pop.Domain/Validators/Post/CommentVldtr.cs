using EvilCorp.Pop.Domain.Aggregates.Post;
using FluentValidation;

namespace EvilCorp.Pop.Domain.Validators.Post
{
    internal class CommentVldtr : AbstractValidator<PostComment>
    {
        public CommentVldtr()
        {
            RuleFor(pc => pc.Text)
                .NotNull().WithMessage("Comment text should not be null")
                .NotEmpty().WithMessage("Comment text should not be empty")
                .MaximumLength(1000)
                .MinimumLength(50);
        }
    }
}
