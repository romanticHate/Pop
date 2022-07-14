using EvilCorp.Pop.Application.Models;
using MediatR;

namespace EvilCorp.Pop.Application.Identity.Commands
{
    public class LoginCmd:IRequest<OperationResult<string>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
