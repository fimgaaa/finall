using MauiToDoFinal.Api.Models;
namespace MauiToDoFinal.Api.Services
{
    public interface IUserService
    {
        Task<User?> RegisterUserAsync(User user);
        Task<User?> LoginUserAsync(User loginUser);
        Task<bool> SendForgotPasswordEmailAsync(string email);
    }
}