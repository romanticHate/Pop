namespace EvilCorp.Pop.Api
{
    public class ApiRoute
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";
        public class UserProfile
        {
            public const string RouteId = "{id}";
        }
        public class Post
        {
            public const string RouteId = "{id}";
            public const string Comments = "{postId}/comments";
            public const string CommentById = "{postId}/comments/{commentId}";
        }
        public static class Identity
        {
            public const string LogIn = "login";
            public const string Registration = "registration";
        }
    }
}
