using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;
using System.Xml.Linq;
using Octokit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GitHubAngularSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReposController : ControllerBase
    {
        // GET: api/<ReposController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
            
        }

        // GET api/<ReposController>/5
        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> Get(string searchTerm)
        {
            var client = new GitHubClient(new Octokit.ProductHeaderValue("Olga"));

            var credentials = new Credentials("epolga", "********", AuthenticationType.Basic);
            var result = await client.Search.SearchRepo(new SearchRepositoriesRequest(searchTerm));
            if (result == null)
            {
                return NotFound();
            }
            var repos = new List<Repo>();

            Octokit.Repository repository = null;
            foreach (var sourceRepo in result.Items)
            {
                repos.Add(new Repo
                {
                    id = sourceRepo.Id.ToString(),
                    name = sourceRepo.Name,
                    avatarUrl = $"https://avatars.githubusercontent.com/u/{sourceRepo.Id}",
                    url = $"{sourceRepo.Url}",
                });
            }
            return Ok(repos);
        }
               
            
        // POST api/<ReposController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<ReposController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            HttpContext.Session.SetString(id.ToString(), value);
            var result = HttpContext.Session.GetString(id.ToString());
            var keys = HttpContext.Session.Keys;

        }

        // DELETE api/<ReposController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
