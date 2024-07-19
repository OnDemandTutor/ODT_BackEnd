using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Tools;

namespace ODT_Model.DTO.Request;

public class RegisterRequest
{
    [StringLength(maximumLength: 40, MinimumLength = 8)]
    public required string Username { get; set; }
    [CustomDataValidation.PasswordValidation]
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    
    [CustomDataValidation.EmailValidation]
    public required string Email { get; set; }
    public string Gender { get; set; }
    public string RoleName { get; set; }
    
}