using System.ComponentModel.DataAnnotations;
using AutoMapper;
using E_Commerce.Api.DataLayer.Database;
using E_Commerce.Api.DataLayer.Models;
using E_Commerce.Api.DTOs;
using E_Commerce.Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDbContext _dbContext;

        private readonly IMapper _mapper;

        public ProductsController(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("pages")]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int takeCount = 8)
        {
            var products = await _dbContext.Products.AsQueryable().OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * takeCount).Take(takeCount).ToListAsync();

            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById([Required(ErrorMessage = "Product id is required")] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _dbContext.Products.AsQueryable().FirstOrDefaultAsync(p => p.Id == id);
            if (product == default) return NotFound();

            return Ok(_mapper.Map<ProductReadDto>(product));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var product = await _dbContext.Products.AsQueryable()
                .FirstOrDefaultAsync(p => p.Name.ToLower().Contains(name.ToLower()));

            if (product == default) return NotFound();

            return Ok(_mapper.Map<ProductReadDto>(product));
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetByCategoryName([FromQuery] string name, [FromQuery] int page = 1,
            [FromQuery] int takeCount = 8)
        {
            var category = await _dbContext.Categories.AsQueryable()
                .FirstOrDefaultAsync(c => c.Name.ToLower().Contains(name.ToLower()));
            if (category == default) return Ok(new List<ProductReadDto>());

            var products = await _dbContext.Products.AsQueryable().Where(p => p.CategoryId == category.Id)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * takeCount)
                .Take(takeCount)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([Required(ErrorMessage = "Product id is required")] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _dbContext.Products.AsQueryable().FirstOrDefaultAsync(p => p.Id == id);
            if (product == default) return NotFound();

            await _dbContext.Products.DeleteOneAsync(p => p.Id == id);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _dbContext.Categories.AsQueryable()
                .FirstOrDefaultAsync(c => c.Id == productCreateDto.CategoryId);

            if (category == default)
            {
                ModelState.AddModelError("CategoryExists", "Category not found");
                return BadRequest(ModelState);
            }

            var imagesFiles = await _dbContext.Images.AsQueryable()
                .Where(i => productCreateDto.ImagesIds.Contains(i.Id)).ToListAsync();

            if (imagesFiles.Count == 0)
            {
                imagesFiles.Add(new ImageFile(Defaults.DefaultItemImage));
            }

            var product = new Product(productCreateDto.CategoryId, productCreateDto.Name, productCreateDto.Description,
                productCreateDto.Price, imagesFiles, productCreateDto.QuantityInStock);

            await _dbContext.Products.InsertOneAsync(product);

            return CreatedAtRoute(nameof(GetProductById), new { id = product.Id },
                _mapper.Map<ProductReadDto>(product));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _dbContext.Products.AsQueryable().FirstOrDefaultAsync(p => p.Id == productUpdateDto.Id);
            if (product == default) return NotFound();

            product.Update(productUpdateDto.CategoryId, productUpdateDto.Name, productUpdateDto.Description,
                productUpdateDto.Price, productUpdateDto.QuantityInStock, productUpdateDto.Images);

            await _dbContext.Products.ReplaceOneAsync(p => p.Id == product.Id, product);

            return CreatedAtRoute(nameof(GetProductById), new { id = product.Id }, product);
        }
    }
}