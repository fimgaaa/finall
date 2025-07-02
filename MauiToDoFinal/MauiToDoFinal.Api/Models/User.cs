using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    [Required]
    [MinLength(4)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}
