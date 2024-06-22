using System.ComponentModel.DataAnnotations;

namespace ODT_Model.DTO.Request;

public class UserResetPassDTO
{
    [Required]
    public string NewPassword { get; set; } = null!;

    [Required]
    [Compare("NewPassword", ErrorMessage = "Confirmed New Password does not match New Password.")]
    public string ConfirmedNewPassword { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}