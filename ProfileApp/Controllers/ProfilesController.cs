using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileApp.Entities;

namespace ProfileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        // GET: api/<ProfilesController>
        [HttpGet]
        public IActionResult Get()
        {
            var profiles = Profile.Profiles;
            return Ok(profiles);
        }

        // GET api/<ProfilesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var profile = Profile.Profiles.Where(x => x.Id == id).FirstOrDefault();
            if(profile == null) return NotFound();
            return Ok(profile);
        }

        // POST api/<ProfilesController>
        [HttpPost]
        public IActionResult Post([FromBody] Profile model)
        {
            Profile.Profiles.Add(model);
            return CreatedAtAction(nameof(Post), new { id = model.Id});
        }

        // PUT api/<ProfilesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] string email)
        {
            var profile = Profile.Profiles.Where(x => x.Id == id).FirstOrDefault();
            if (profile == null) return NotFound();
            profile.Email = email;
            return Ok(profile);
        }

        // DELETE api/<ProfilesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var profile = Profile.Profiles.Where(x => x.Id == id).FirstOrDefault();
            if (profile == null) return NotFound();
            Profile.Profiles.Remove(profile);
            return Ok();
        }
    }
}
