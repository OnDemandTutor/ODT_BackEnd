using System.ComponentModel.DataAnnotations;

namespace ODT_Model.DTO.Request;

public class LoginDTORequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}