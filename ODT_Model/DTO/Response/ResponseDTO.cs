namespace ODT_Model.DTO.Response;

public class ResponseDTO
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
}

public class ResponseDTO<T> : ResponseDTO
{
    public T Data { get; set; }
}