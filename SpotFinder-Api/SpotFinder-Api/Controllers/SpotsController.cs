using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotFinder_Api.Models;
using SpotFinder_Api.Services.Spots;

namespace SpotFinder_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotsController : ControllerBase
    {
        private readonly SpotsService _spotsService;

        public SpotsController( SpotsService spotsService)
        {
            _spotsService = spotsService;            
        }

        [HttpGet]
        public async Task<List<Spot>> Get() => await _spotsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Spot>> Get(string id)
        {
            var spot = await _spotsService.GetAsync(id);

            if ( spot == null)
            {
                return NotFound();
            }

            return spot;
        }

        [HttpGet("{location}")]
        public async Task<ActionResult<List<Spot>>> SearchLocation(string location) => await _spotsService.SearchLocation(location);
        
        //[HttpGet("{id:length(24)}")]
        //public async Task<ActionResult<Spot>> Get(string name)
        //{
        //    var spot = await _spotsService.GetAsync(name);

        //    if ( spot == null)
        //    {
        //        return NotFound();
        //    }

        //    return spot;
        //}

        [HttpPost]
        public async Task<IActionResult> Post(Spot newSpot)
        {
            await _spotsService.CreateAsync(newSpot);

            return CreatedAtAction(nameof(Post), new { id = newSpot.Id }, newSpot);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Spot updatedSpot)
        {
            var spot = await _spotsService.GetAsync(id);

            if (spot is null)
            {
                return NotFound();
            }

            updatedSpot.Id = spot.Id;
            await _spotsService.UpdateAsync(id, updatedSpot);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var spot = await _spotsService.GetAsync(id);

            if (spot is null)
            {
                return NotFound();
            }

            await _spotsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
