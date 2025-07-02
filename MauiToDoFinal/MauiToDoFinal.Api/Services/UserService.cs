using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using MauiToDoFinal.Api.Data;
using MauiToDoFinal.Api.Models;

namespace MauiToDoFinal.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext db, ILogger<UserService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<User?> RegisterUserAsync(User user)
        {
            var exists = await _db.Users.AnyAsync(u => u.Username == user.Username || u.Email == user.Email);
            if (exists) return null;

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User?> LoginUserAsync(User loginUser)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.Username == loginUser.Username && u.Password == loginUser.Password);
        }

        public async Task<bool> SendForgotPasswordEmailAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            try
            {
                var message = new MailMessage("no-reply@mauitodo.com", user.Email)
                {
                    Subject = "Parola Sıfırlama",
                    Body = $"Merhaba {user.Username}, sifre sifirlama isteginiz alindi."
                };

                using var smtp = new SmtpClient("localhost");
                await smtp.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send forgot password email");
                return false;
            }
        }
    }
}