﻿using ClientLibrary.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> CreateUser(CreateUser user);
        Task<LoginResponse> LoginUser(CreateUser user);
        Task<LoginResponse> ReviveToken(string refreshToken);
    }
}
