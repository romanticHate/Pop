using EvilCorp.Pop.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCorp.Pop.Application.Identity.Commands
{
    public class RegisterIdentity:IRequest<OperationResult<string>>
    {       
        public string Username { get; set; }      
        public string Password { get; set; }   
        public string FirstName { get; set; }    
        public string LastName { get; set; }       
        public string EmailAddress { get; set; }
        public string Phone { get; set; }      
        public DateTime DateOfBirth { get; set; }     
        public string CurrentCity { get; set; }
    }
}
