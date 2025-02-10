using System.ComponentModel.DataAnnotations;

namespace Otanimes.ApplicationServices.VIewModels.User;

public record LoginViewModel
{
    [Required]
    [EmailAddress]
    [MinLength(5)]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(100)]
    public string Password { get; set; }
}