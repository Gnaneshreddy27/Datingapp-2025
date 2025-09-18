using System.Security.Cryptography;
using System.Text;
using API.data;
using API.DTOs;
using API.entities;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers;

public class AccountController(AppDBcontext context) : BaseAPIController 
{
    [HttpPost("register")]//api/account/register

    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key

        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
        
    }

}
