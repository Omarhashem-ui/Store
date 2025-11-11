using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shared.ErrorModels
{
    public class ValdiationErrorResponse
    {
        public int StatusCode { get; set; } = StatusCodes.Status404NotFound;
        public string ErrorMessage { get; set; } = "Valdiation Error :(";
        public IEnumerable<ValdiationError> Errors { get; set; }

    }
    public class ValdiationError
    {
        public string Field { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
