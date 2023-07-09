using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO requestDTO)
        {
            ValidateFileUpload(requestDTO);

            if (ModelState.IsValid)
            {
                

                var imageDomainModel = new Image()
                {
                    File = requestDTO.File,
                    FileDescription = requestDTO.FileDescription,
                    FileName = requestDTO.FileName,
                    FileExtension = Path.GetExtension(requestDTO.File.FileName),
                    FileSizeInBytes = requestDTO.File.Length
                };

                // Add the User repository

                await imageRepository.UploadAsync(imageDomainModel);

                return Ok(imageDomainModel);
            }


            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDTO requestDTO)
        {
            var allowedExtensions = new string[] { ".jpeg", ".jpg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(requestDTO.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if(requestDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size must be less than 10MB");
            }
        }
    }
}
