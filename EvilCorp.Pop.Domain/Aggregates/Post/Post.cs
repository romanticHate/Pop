using EvilCorp.Pop.Domain.Exceptions;
using EvilCorp.Pop.Domain.Validators.Post;

namespace EvilCorp.Pop.Domain.Aggregates.Post
{
    public class Post
    {
        private readonly List<PostComment> _comments = new List<PostComment>();
        private readonly List<PostInteraction> _interaction = new List<PostInteraction>();

        private Post()
        {
           
        }

        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public UserProfile.UserProfile UserProfile { get; private set; }
        public string TextContent { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }

        public IEnumerable<PostComment> Comments { get { return _comments; } }
        public IEnumerable<PostInteraction> Interactions { get { return _interaction; } }
       
        /// <summary>
        /// Creates a new post instance
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="textContent">Post content</param>
        /// <returns><see cref="Post"/></returns>
        /// <exception cref="PostNotValidException"></exception>
        public static Post CreatePost(Guid userProfileId, string textContent)
        {
            var validator = new PostVldtr();
            var objToVldt = new Post
            {
                UserProfileId = userProfileId,
                TextContent = textContent,
                CreatedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
            var result = validator.Validate(objToVldt);

            if (!result.IsValid) {
                var exception = new PostNotValidException("Post is not valid");
                result.Errors.ForEach(vr => exception.Errors.Add(vr.ErrorMessage));
                //foreach (var error in vldtResult.Errors)
                //{
                //    exception.Errors.Add(error.ErrorMessage);
                //}
                throw exception;
            }
            
            return objToVldt;           
        }

        /// <summary>
        /// Updates the post text
        /// </summary>
        /// <param name="newtext">The updated post text</param>
        /// <exception cref="PostNotValidException"></exception>
        public void UpdatePostText(string newtext)
        {
            if (string.IsNullOrWhiteSpace(newtext))
            {
                var exception = new PostNotValidException("Cannot update post." + " Post text is not valid");
                exception.Errors.Add("The provided text is either null or contains only white space");
                throw exception;
            }
          TextContent = newtext;
          LastModified = DateTime.Now;
        }

        public void AddPostComment(PostComment newComment)
        {
            // We dont add the property we add the field
            _comments.Add(newComment);
        }

        public void RemovePostComment(PostComment toRemove)
        {
            // We dont add the property we add the field
            _comments.Remove(toRemove);
        }

        public void AddInteraction(PostInteraction newInteraction)
        {
            // We dont add the property we add the field
            _interaction.Add(newInteraction);
        }

        public void RemoveInteraction(PostInteraction toRemove)
        {
            // We dont add the property we add the field
            _interaction.Remove(toRemove);
        }
    }
}
