using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCorp.Pop.Application.Identity
{
    public class ErrorMessage
    {
        public const string NonExistentIdentityUser = "Unable to find a user whit the specified username";
        public const string IncorrectPassword = "The provited password is incorrect";
    }
}
