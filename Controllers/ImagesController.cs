using E_Commerce.Api.DataLayer.Database;
using E_Commerce.Api.DataLayer.Models;
using E_Commerce.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using AutoMapper;
using E_Commerce.Api.DTOs;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IDbContext _dbContext;

        private readonly IImageServices _imageServices;

        private readonly IMapper _mapper;

        public ImagesController(IDbContext dbContext, IImageServices imageServices, IMapper mapper)
        {
            _dbContext = dbContext;
            _imageServices = imageServices;
            _mapper = mapper;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageDto uploadImageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imagePath = await _imageServices.SaveImageAsync(uploadImageDto.ImageFile!);
            var file = new ImageFile(imagePath);
            await _dbContext.Images.InsertOneAsync(file);

            var dto = _mapper.Map<ImageFileReadDto>(file);

            return Ok(dto);
        }

        [HttpPost("upload/collection")]
        public async Task<IActionResult> UploadImages([FromForm] UploadImagesDto uploadImagesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paths = await _imageServices.SaveImagesAsync(uploadImagesDto.ImagesCollection);
            var files = paths.Select(p => new ImageFile(p)).ToImmutableList();
            await _dbContext.Images.InsertManyAsync(files);

            var filesDto = _mapper.Map<IEnumerable<ImageFileReadDto>>(files);

            return Ok(filesDto);
        }
    }
}