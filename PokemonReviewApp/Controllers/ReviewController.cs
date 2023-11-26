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
    public class ReviewController: Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository,IReviewerRepository reviewerRepository,IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int id)
        {
            if (!_reviewRepository.ReviewExists(id)) return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(review);
        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonReviews(int pokeId) { 
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetPokemonReviews(pokeId));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int pokemonId, [FromQuery] int reviewerId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null) return BadRequest(ModelState);

            var review = _reviewRepository.GetReviews()
                .Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Review already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);
            reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokemonId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);


            if (!_reviewRepository.createReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");


        }
    }
}
