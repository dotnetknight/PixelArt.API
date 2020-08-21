using PixelArt.Models.Models;
using System.Collections.Generic;

namespace PixelArt.Models.Responses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
