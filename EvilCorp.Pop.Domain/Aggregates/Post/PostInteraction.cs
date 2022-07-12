using EvilCorp.Pop.Domain.Validators.Post;

namespace EvilCorp.Pop.Domain.Aggregates.Post
{
    public class PostInteraction
    {       
        public Guid InteractionId { get; private set; }
        public Guid PostId { get; private set; }
        public Guid? UserProfileId { get; private set; }
        public UserProfile.UserProfile UserProfile { get; private set; }
        public InteractionType InteractionType { get; private set; }

        public static PostInteraction CreatePostInteraction(Guid postId, InteractionType type)
        {
            var validator = new PostInteractionVldtr();
            return new PostInteraction
            {
                PostId = postId,
                InteractionType = type
            };
        }
    }
}
