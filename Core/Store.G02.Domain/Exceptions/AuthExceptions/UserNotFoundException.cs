using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.AuthExceptions
{
    public class UserNotFoundException(string email) : NotFoundExceptions($"Not found user is email: {email}")
    {
    }
}
