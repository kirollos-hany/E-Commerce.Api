using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.Api.DataLayer.Database;
using E_Commerce.Api.DataLayer.Models;
using E_Commerce.Api.DTOs;
using E_Commerce.Api.Services;
using E_Commerce.Api.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NuGet.Versioning;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDbContext _dbContext;

        private readonly IMapper _mapper;

        public CategoriesController(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cats = await _dbContext.Categories.AsQueryable().OrderByDescending(c => c.CreatedAt).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryReadDto>>(cats));
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var category = await _dbContext.Categories.AsQueryable()
                .FirstOrDefaultAsync(c => c.Name.ToLower().Contains(name.ToLower()));
            if (category == default) return NotFound();

            return Ok(_mapper.Map<CategoryReadDto>(category));
        }

        [HttpGet("pages")]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int takeCount = 4)
        {
            var cats = await _dbContext.Categories.AsQueryable().OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * takeCount).Take(takeCount).ToListAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryReadDto>>(cats));
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById([Required(ErrorMessage = "Category id is required")] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _dbContext.Categories.AsQueryable().FirstOrDefaultAsync(c => c.Id == id);
            if (category == default) return NotFound();

            return Ok(_mapper.Map<CategoryReadDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto categoryCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageFile = await _dbContext.Images.AsQueryable()
                .FirstOrDefaultAsync(f => f.Id == categoryCreateDto.ImageId);
            var category = new Category(categoryCreateDto.Name, imageFile ?? new ImageFile(Defaults.DefaultItemImage));

            await _dbContext.Categories.InsertOneAsync(category);

            return CreatedAtRoute(nameof(GetCategoryById), new { id = category.Id }, _mapper.Map<CategoryReadDto>(category));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _dbContext.Categories.AsQueryable()
                .FirstOrDefaultAsync(c => c.Id == categoryUpdateDto.Id);
            if (category == default) return NotFound();

            var imageFile = await _dbContext.Images.AsQueryable()
                .FirstOrDefaultAsync(f => f.Id == categoryUpdateDto.ImageId);

            category.Update(categoryUpdateDto.Name, imageFile ?? category.Image);

            await _dbContext.Categories.ReplaceOneAsync(c => c.Id == category.Id, category);

            return CreatedAtRoute(nameof(GetCategoryById), new { id = category.Id }, _mapper.Map<CategoryReadDto>(category));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([Required(ErrorMessage = "Category id is required")] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _dbContext.Categories.AsQueryable().FirstOrDefaultAsync(c => c.Id == id);
            if (category == default) return NotFound();

            await _dbContext.Categories.DeleteOneAsync(c => c.Id == category.Id);

            return Ok();
        }
    }
}