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
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        public IQuestionRepo questionsRepo { get; set; }

        public QuestionsController(IQuestionRepo newQuestionsRepo, ILogger<QuestionsController> logger)
        {
            questionsRepo = newQuestionsRepo;
            _logger = logger;
        }

        // GET: api/Questions
        [HttpGet]
        public IEnumerable<QuestionsModel> Get()
        {
            return questionsRepo.GetQuestionByDifficultyAndCategory(1, "Movie");
        }

        // GET: api/Questions/5
        [HttpGet("{id}", Name = "GetQuestionById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<QuestionsModel> GetById(int id)
        {
            return questionsRepo.GetQuestionById(id);
        }

        // POST: api/Questions
        [HttpPost]
        [ProducesResponseType(typeof(QuestionsModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] QuestionsModel q)
        {
            questionsRepo.AddQuestion(q);
            return CreatedAtAction(nameof(GetById), new { id = q.Id }, q);
        }
    }
}
