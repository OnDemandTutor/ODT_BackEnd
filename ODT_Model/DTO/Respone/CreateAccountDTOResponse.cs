using System;

namespace ODT_Model.DTO.Response;

public class CreateAccountDTOResponse
{
    public required string Fullname { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required DateTime dob { get; set; }
    public string IdentityCard { get; set; }
    public string phone { get; set; }
}