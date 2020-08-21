using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PixelArt.Models.Requests
{
    public class ConvertRequest
    {
        public IFormFile ImageFile { get; set; }
        public List<string> HexCodes { get; set; }
    }
}
