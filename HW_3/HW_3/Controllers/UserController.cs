using HW_3.Abstractions;
using HW_3.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW_3.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IRepository<User> _userRepository;
        public UserController(IRepository<User> userRepository) {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get() =>
            await _userRepository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<User?> Get(Guid id) =>
            await _userRepository.GetAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User entity) {
            entity.Id = Guid.NewGuid();
            await _userRepository.AddAsync(entity);
            return Created("", entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] User entity) {
            var user = await _userRepository.GetAsync(id);
            if (user == null) return BadRequest("user doesn't exist");
            await _userRepository.UpdateAsync(user);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var user = await _userRepository.GetAsync(id);
            if (user == null) return BadRequest("user doesn't exist");
            await _userRepository.DeleteAsync(id);
            return Ok();
        }

    }
}
