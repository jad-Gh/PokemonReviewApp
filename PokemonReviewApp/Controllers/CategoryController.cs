using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController: Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCatgeories());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id)) return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonsByCategory(int categoryId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid) return BadRequest();

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate) 
        { 
            if (categoryCreate == null) return BadRequest(ModelState);

            var category = _categoryRepository.GetCatgeories()
                .Where(c=>c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null) 
            {
                ModelState.AddModelError("", "Category already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap)) 
            {
                ModelState.AddModelError("", "Something went wrong saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Updatecategory(int categoryId,[FromBody] CategoryDto updateCategory)
        {
            if (updateCategory == null)
                return BadRequest(ModelState);
            if (categoryId != updateCategory.Id)
                  return BadRequest(ModelState);
            if (!_categoryRepository.CategoryExists(categoryId)) 
                return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var categoryMap = _mapper.Map<Category>(updateCategory);

            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }



    }
}
