using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.AuthExceptions
{
    public class RegistrationBadRequestExceptions(List<string> errors) : BadRequestExceptions(string.Join(", ", errors))
    {
    }
}
