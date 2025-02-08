using FaceDetectionAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FaceDetectionAPI.Controllers
{
    [ApiController]
    [Route("api/face")]
    public class FaceDetectionController : ControllerBase
    {
        private readonly FaceDetectionService _faceDetectionService;

        public FaceDetectionController(FaceDetectionService faceDetectionService)
        {
            _faceDetectionService = faceDetectionService;
        }

        [HttpPost("detect")]
        public IActionResult DetectFaces(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var faces = _faceDetectionService.DetectFaces(filePath);
            return Ok(new { FacesDetected = faces.Count });
        }
    }
}
