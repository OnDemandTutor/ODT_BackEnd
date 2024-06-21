using ODT_Model.DTO.Response;

namespace ODT_Service.Interface;

public interface IEmailConfig
{
    public ResponseDTO SendEmail(EmailDTO emailDTO);
}