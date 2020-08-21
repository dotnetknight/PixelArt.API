using Microsoft.AspNetCore.Mvc;
using PixelArt.Models.Requests;
using PixelArt.Service;
using System.Threading.Tasks;

namespace PixelArtAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ConvertService _convertService;
        public ImageController(ConvertService convertService)
        {
            _convertService = convertService;
        }

        [HttpPost]
        public async Task<IActionResult> Convert([FromForm] ConvertRequest c)
        {
            return File(await _convertService.Convert(c.ImageFile, c.HexCodes), "image/png");
        }
    }
}