using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Models
{
    public class LoginResponse(
        bool Succes=false,
        string Message=null!,
        string Token=null!,
        string RefreshToken = null!);
   
}
