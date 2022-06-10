using System.Security.Claims;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Api.SecurityLayer.Managers;

public class UserManager : IUserManager
{
    private readonly UserManager<MongoIdentityUser<string>> _userManager;


    public UserManager(UserManager<MongoIdentityUser<string>> userManager)
    {
        _userManager = userManager;
    }

    public Task<bool> CheckPasswordAsync(MongoIdentityUser<string> user, string password) =>
        _userManager.CheckPasswordAsync(user, password);

    public async Task<IEnumerable<string>> GetUserRolesAsync(MongoIdentityUser<string> user) =>
        await _userManager.GetRolesAsync(user);

    public Task<MongoIdentityUser<string>?> FindByEmailAsync(string email) => _userManager.FindByEmailAsync(email);

    public Task<MongoIdentityUser<string>?> FindByNameAsync(string userName) => _userManager.FindByNameAsync(userName);

    public Task<MongoIdentityUser<string>?> GetUserAsync(ClaimsPrincipal userClaims) =>
        _userManager.GetUserAsync(userClaims);

    public Task<bool> IsEmailConfirmedAsync(MongoIdentityUser<string> user) => _userManager.IsEmailConfirmedAsync(user);

    public Task<string> GenerateEmailConfirmationTokenAsync(MongoIdentityUser<string> user) =>
        _userManager.GenerateEmailConfirmationTokenAsync(user);

    public Task<IdentityResult> UpdateAsync(MongoIdentityUser<string> user) => _userManager.UpdateAsync(user);

    public Task<IdentityResult> ConfirmEmailAsync(MongoIdentityUser<string> user, string token) =>
        _userManager.ConfirmEmailAsync(user, token);

    public async Task<IdentityResult> ValidatePasswordAsync(string password)
    {
        foreach (var validator in _userManager.PasswordValidators)
        {
            var result = await validator.ValidateAsync(_userManager, new(), password);
            if (!result.Succeeded)
            {
                return result;
            }
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> ValidateUserAsync(MongoIdentityUser<string> user)
    {
        foreach (var validator in _userManager.UserValidators)
        {
            var result = await validator.ValidateAsync(_userManager, user);
            if (!result.Succeeded)
            {
                return result;
            }
        }

        return IdentityResult.Success;
    }

    public Task<IdentityResult> ChangePasswordAsync(MongoIdentityUser<string> user, string currentPassword,
        string newPassword) => _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

    public Task<string> GeneratePasswordResetTokenAsync(MongoIdentityUser<string> user) =>
        _userManager.GeneratePasswordResetTokenAsync(user);

    public Task<IdentityResult> ResetPasswordAsync(MongoIdentityUser<string> user, string token, string newPassword) =>
        _userManager.ResetPasswordAsync(user, token, newPassword);

    public Task<MongoIdentityUser<string>?> FindByIdAsync(string id) => _userManager.FindByIdAsync(id);

    public Task<IdentityResult> DeleteAsync(MongoIdentityUser<string> user) => _userManager.DeleteAsync(user);

    public Task<IdentityResult> AddToRoleAsync(MongoIdentityUser<string> user, string roleName) =>
        _userManager.AddToRoleAsync(user, roleName);

    public Task<IdentityResult> CreateAsync(MongoIdentityUser<string> user, string password) =>
        _userManager.CreateAsync(user, password);

    public Task<bool> IsInRoleAsync(MongoIdentityUser<string> user, string role) =>
        _userManager.IsInRoleAsync(user, role);
}