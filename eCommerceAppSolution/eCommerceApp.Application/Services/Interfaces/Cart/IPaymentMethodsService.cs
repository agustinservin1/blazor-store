using eCommerceApp.Application.Models.CartDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Interfaces.Cart
{
    public interface IPaymentMethodsService
    {
        Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods();
    }
}
