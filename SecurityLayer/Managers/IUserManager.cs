using System.Security.Claims;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Api.SecurityLayer.Managers;

public interface IUserManager
{
    Task<bool> CheckPasswordAsync(MongoIdentityUser<string> user, string password);
    Task<IEnumerable<string>> GetUserRolesAsync(MongoIdentityUser<string> user);
    
    Task<MongoIdentityUser<string>?> FindByEmailAsync(string email);

    Task<MongoIdentityUser<string>?> FindByNameAsync(string userName);

    Task<MongoIdentityUser<string>?> GetUserAsync(ClaimsPrincipal userClaims);

    Task<bool> IsEmailConfirmedAsync(MongoIdentityUser<string> user);

    Task<string> GenerateEmailConfirmationTokenAsync(MongoIdentityUser<string> user);

    Task<IdentityResult> UpdateAsync(MongoIdentityUser<string> user);

    Task<IdentityResult> ConfirmEmailAsync(MongoIdentityUser<string> user, string token);

    Task<IdentityResult> ValidatePasswordAsync(string password);

    Task<IdentityResult> ValidateUserAsync(MongoIdentityUser<string> user);

    Task<IdentityResult> ChangePasswordAsync(MongoIdentityUser<string> user, string currentPassword, string newPassword);

    Task<string> GeneratePasswordResetTokenAsync(MongoIdentityUser<string> user);

    Task<IdentityResult> ResetPasswordAsync(MongoIdentityUser<string> user, string token, string newPassword);

    Task<MongoIdentityUser<string>?> FindByIdAsync(string id); 

    Task<IdentityResult> DeleteAsync(MongoIdentityUser<string> user);

    Task<IdentityResult> AddToRoleAsync(MongoIdentityUser<string> user, string roleName);

    Task<IdentityResult> CreateAsync(MongoIdentityUser<string> user, string password);

    Task<bool> IsInRoleAsync(MongoIdentityUser<string> user, string role);
}