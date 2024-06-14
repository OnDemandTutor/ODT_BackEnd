namespace ODT_Model.DTO.Response;

public class LoginDTOResponse
{
    public required long id {  get; set; }
    public required string username { get; set; }
    public required string email { get; set; }
    public required long permission_id { get; set; }
}