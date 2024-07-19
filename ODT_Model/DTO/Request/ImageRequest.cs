using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace ODT_Model.DTO.Request;

public class ImageRequest
{
    public required IFormFile Image { get; set; }
}