using EvilCorp.Pop.Domain.Exceptions;
using EvilCorp.Pop.Domain.Validators.Post;

namespace EvilCorp.Pop.Domain.Aggregates.Post
{
    public class PostComment
    {      
        public Guid CommentId { get; private set; }
        public Guid PostId { get; private set; }
        public string Text { get; private set; }
        public Guid UserProfileId { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }

        
        /// <summary>
        /// Create post comment
        /// </summary>
        /// <param name="postId">The ID of the post witch the comment belongs</param>
        /// <param name="text">Text content of the comment</param>
        /// <param name="userProfileId">The ID of the user who created the commend</param>
        /// <returns> <see cref="PostCommentNotValidException"/></returns>
        /// <exception cref="PostCommentNotValidException">Throw if the data provided for the post comment is NOT valid</exception>
        public static PostComment CreatePostComment(Guid postId,string text,Guid userProfileId)
        {

            var validator = new CommentVldtr();
            var objToVldt = new PostComment
            {
                PostId = postId,
                Text = text,
                UserProfileId = userProfileId,
                DateCreated = DateTime.Now,
                LastModified = DateTime.Now
            };
            var vldtResult  = validator.Validate(objToVldt);

            if (vldtResult.IsValid) return objToVldt;
            var exception = new PostCommentNotValidException("Post comment is not valid");
            foreach (var error in vldtResult.Errors)
            {
                exception.Errors.Add(error.ErrorMessage);
            }
            throw exception;
        }

        // Public methods
        public void UpdateCommentText(string updateText)
        {           
            Text = updateText;
            LastModified = DateTime.Now;
        }
    }
}
