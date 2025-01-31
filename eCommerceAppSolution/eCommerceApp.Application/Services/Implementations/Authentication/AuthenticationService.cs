﻿using AutoMapper;
using eCommerceApp.Application.Models;
using eCommerceApp.Application.Models.IdentityDto;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Application.Validations;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using FluentValidation;
using System.Runtime.CompilerServices;


namespace eCommerceApp.Application.Services.Implementations.Authentication
{
    public class AuthenticationService(
            ITokenManagement tokenManagement,
            IUserManagement userManagement,
            IRoleManagement roleManagement,
            IAppLogger<AuthenticationService> logger,
            IMapper mapper,
            IValidator<CreateUser> createUserValidator,
            IValidator<LoginUser> loginUserValidator,
            IValidationService validationService
        ) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var _validationResult = await validationService.ValidateAsync(user, createUserValidator);
            if (_validationResult.Succes) return _validationResult;
            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.UserName = user.Email;
            mappedModel.PasswordHash = user.Password;

            var result = await userManagement.CreateUser(mappedModel);
            if (!result)
            {
                return new ServiceResponse { Message = "Email Adress might be already in use or unknown error ocurred" };
            }
            var _user = await userManagement.GetUserByEmail(user.Email);
            var users = await userManagement.GetAllUsers();
            var assignedResult = (bool) await roleManagement.AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");
            if (!assignedResult)
            {
                //remove user
                int removeUserResult = await userManagement.RemoveUserByEmail(_user!.Email!);
                if (removeUserResult <= 0)
                {
                    //error ocurred while rolling back changes, then log the error
                    logger.LogError(
                        new Exception($"User with Email as {_user.Email} failed to be remove as a result of role assigning issue"),
                        "User could not be assigned Role");
                    return new ServiceResponse { Message = "Error occurred in creating account" };
                }

            }
            return new ServiceResponse { Succes = true, Message = "Account created!" };
        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var _validationResult = await validationService.ValidateAsync(user, loginUserValidator);
            if (!_validationResult.Succes)
                return new LoginResponse(Message: _validationResult.Message);
            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.PasswordHash = user.Password;
            bool loginResult = await userManagement.LoginUser(mappedModel);
            if (!loginResult)
                return new LoginResponse(Message: "Email not found or invalid credentials");
            var _user = await userManagement.GetUserByEmail(user.Email);
            var claims = await userManagement.GetUserClaims(_user!.Email!);
            string jwtToken = tokenManagement.GenerateToken(claims);
            string refreshToken = tokenManagement.GetRefreshToken();
            int saveTokenResult = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);
            return saveTokenResult <= 0 ? new LoginResponse(Message: "Internal error ocurred while authenticating") :
                new LoginResponse(Succes: true, Token: jwtToken, RefreshToken: refreshToken);

        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            bool validateTokenResult = await tokenManagement.ValidateRefreshToken(refreshToken);
            if (!validateTokenResult)
                return new LoginResponse(Message: "Invalid token");
            string userId = await tokenManagement.GetUserIdByRefreshToken(refreshToken);
            AppUser? user = await userManagement.GetUserByEmail(userId);
            var claims = await userManagement.GetUserClaims(user!.Email!);
            string newJwtToken = tokenManagement.GenerateToken(claims);
            string newRefreshToken = tokenManagement.GetRefreshToken();
            await tokenManagement.UpdateRefreshToken(userId, newRefreshToken);
            return new LoginResponse(Succes: true, Token:newJwtToken, RefreshToken:newRefreshToken);        

        }
    }
}
