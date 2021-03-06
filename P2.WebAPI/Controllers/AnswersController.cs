using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Library.IRepositories;
using BLL.Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace P2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly ILogger<AnswersController> _logger;

        public IAnswersRepo answersRepo { get; set; }

        public AnswersController(IAnswersRepo newAnswerRepo, ILogger<AnswersController> logger)
        {
            answersRepo = newAnswerRepo;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetAnswerById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AnswerModel> GetById(int id)
        {
            return answersRepo.GetAnswerById(id);
        }

        // POST: api/Answers
        [HttpPost]
        [ProducesResponseType(typeof(AnswerModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AnswerModel a)
        {
            await answersRepo.AddAnswer(a);
            return CreatedAtAction(nameof(GetById), new { id = a.Id }, a);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "DeleteAnswerById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (answersRepo.GetAnswerById(id) is AnswerModel answer) //if found
            {
                //delete user
                answersRepo.DeleteAnswer(answer.Id);
                await answersRepo.Save();
                return NoContent(); // 204
            }

            //if not found,
            return NotFound(); //404
        }
    }
}
