﻿using EvilCorp.Pop.Application.Models;
using MediatR;

namespace EvilCorp.Pop.Application.UserProfile.Commands
{
    public class CreateUserProfileCmd:IRequest<OperationResult<Domain.Aggregates.UserProfile.UserProfile>>
    {       
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string CurrentCity { get; private set; }
    }
}
