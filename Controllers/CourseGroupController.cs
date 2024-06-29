using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactServer.Models;
using ReactServer.Services;

namespace ReactServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseGroupController : ControllerBase
    {
        private readonly ICourseGroupRepository _repository;

        public CourseGroupController(ICourseGroupRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseGroup>>> GetCourseGroups()
        {
            var courseGroups = await _repository.GetAllAsync();
            return Ok(courseGroups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseGroup>> GetCourseGroup(int id)
        {
            var courseGroup = await _repository.GetByIdAsync(id);
            if (courseGroup == null)
            {
                return NotFound();
            }
            return Ok(courseGroup);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCourseGroup(CourseGroup courseGroup)
        {
            await _repository.AddAsync(courseGroup);
            return CreatedAtAction(nameof(GetCourseGroup), new { id = courseGroup.Id }, courseGroup);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourseGroup(int id, CourseGroup courseGroup)
        {
            if (id != courseGroup.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(courseGroup);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourseGroup(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }

}
