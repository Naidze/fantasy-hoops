﻿using fantasy_hoops.Models;
using fantasy_hoops.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace fantasy_hoops.Services
{
    public interface IUserService
    {

        Task<bool> Login(LoginViewModel model);
        Task<bool> Register(RegisterViewModel user);
        Task<IdentityResult> GoogleLogin(ClaimsPrincipal user);
        Task<bool> GoogleRegister(ClaimsPrincipal model);
        void Logout();
        Task<string> RequestToken(string username);
        Task<string> RequestTokenById(string id);
        Task<string> RequestTokenByEmail(string id);
        Task<bool> UpdateProfile(EditProfileViewModel model);
        bool UploadAvatar(AvatarViewModel model);
        bool ClearAvatar(AvatarViewModel model);

    }
}
