using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TicketingService.Adapters;
using TicketingService.Models;
using TicketingService.Services;

namespace TicketingService.Controllers
{
    [ApiController]
    [Route("persons")]
    [Authorize(Roles = "manager")]
    public class PersonController(ILogger<PersonController> logger, JsonPlaceholderAdapter jsonPlaceholderAdapter, PeopleManagementService peopleManagementService) : ControllerBase
    {

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPersonById([FromRoute]int id)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;

            var centre = peopleManagementService.GetStaffCentre(userName!);
            IEnumerable<Person> persons = GetPersonList(centre == null? null: [centre]);
            if(!persons.Any(p=>p.Id == id))
            {
                return NotFound("The requested person does not exist");
            }
            var result = new JsonResult(persons.First(p => p.Id == id))
            {
                StatusCode = (int?)HttpStatusCode.OK
            };
            return result;
            //return Ok(persons.First(p => p.Id == id));
        }
        [HttpGet]
        public IActionResult SearchPerson([FromQuery] string? searchTerm)
        {
            //var token = HttpContext.Session.GetString("token");
            //if(token != "123456789") {
            //    return Unauthorized();
            //}

            var userName = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;

            var centre = peopleManagementService.GetStaffCentre(userName!);
            IEnumerable<Person> persons = GetPersonList(centre == null ? null : [centre]);
            if (searchTerm == null)
            {
                return Ok(persons);
            }

            if (searchTerm != null && !persons.Any(p => p.Name.Contains(searchTerm)))
            {
                return NotFound("No result found");
            }
            return Ok(persons.First(p => p.Name.Contains(searchTerm!)));
        }

        [HttpGet]
        [Route("posts")]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await jsonPlaceholderAdapter.GetPosts();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return new JsonResult("Sorry, Unexpected error occurred while retrieving the posts")
                {
                    StatusCode = 500,
                };
            }
        }
        [HttpPost]
        [Route("posts")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest postRequest)
        {

            try
            {
                var posts = await jsonPlaceholderAdapter.AddPost(postRequest);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return new JsonResult("Sorry, Unexpected error occurred while retrieving the posts")
                {
                    StatusCode = 500,
                };
            }
        }

        private static IEnumerable<Person> GetPersonList(List<string>? centres)
        {
            IEnumerable<Person> persons = [
                new Person { Name = "Adewale", Id = 1, Age = 15, Centre ="Abeokuta" },
                new Person { Name = "Bimbo", Id = 2, Age = 10, Centre = "Lagos" }
                ];
            if(centres == null )
              return persons;
            
            return persons.Where(p => centres.Contains(p.Centre));
        }
    }

    public record Person
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set;}
        public required string Centre { get; set;}
    }
}
