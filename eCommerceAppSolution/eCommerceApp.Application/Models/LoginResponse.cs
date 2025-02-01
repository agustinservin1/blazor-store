using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Models
{
    public record LoginResponse(
        bool Succes=false,
        string Message=null!,
        string Token=null!,
        string RefreshToken = null!);
   
}
