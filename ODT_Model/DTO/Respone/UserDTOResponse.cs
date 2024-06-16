namespace ODT_Model.DTO.Response;

public class UserDTOResponse
{
    public long RoleId { get; set; }

    public string Username { get; set; }

    public string Fullname { get; set; }

    public string Email { get; set; }

    public string IdentityCard { get; set; }

    public string Gender { get; set; }

    public string Avatar { get; set; }

    public DateTime Dob { get; set; }

    public string Phone { get; set; }

    public DateTime CreateDate { get; set; }

    public bool Status { get; set; }
}