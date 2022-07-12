using AutoMapper;
using EvilCorp.Pop.Api.Contracts.Post.Requests;
using EvilCorp.Pop.Api.Contracts.Post.Responses;
using EvilCorp.Pop.Application.Post.Commands;
using EvilCorp.Pop.Domain.Aggregates.Post;

namespace EvilCorp.Pop.Api.AutMapper
{
    public class PostMap:Profile
    {
        public PostMap()
        {
            CreateMap<Post, PostRspn>();
            CreateMap<PostRqst, CreatePostCmd>();
            CreateMap<PostRqst, UpdatePostTextCmd>();

            CreateMap<CommentRqst, CreatePostCmd>();
            CreateMap<PostComment, CommentRspn>();
        }      
    }
}
