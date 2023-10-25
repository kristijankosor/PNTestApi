using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PNTestApi.Models;
using PNTestApi.Interfaces;
using PNTestApi.Repositories;

namespace PNTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly IQueryRepository _queryRepository;
        public QueriesController(IQueryRepository queryRepository) 
        {
            _queryRepository = queryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Query>))]
        public async Task<IActionResult> PastQueries()
        {
            var queries = _queryRepository.GetQueries();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(queries);
        }
    }
}
