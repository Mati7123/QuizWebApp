using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using quiz_backend.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace quiz_backend.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        readonly QuizContext _context;

        public QuestionsController(QuizContext context)
        {
            this._context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return _context.Questions;
        }

        [HttpGet("{quizId}")]
        public IEnumerable<Question> Get([FromRoute]int quizId)
        {
            return _context.Questions.Where(q => q.QuizId == quizId);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Question question)
        {
            var quiz = _context.Quiz.SingleOrDefault(q => q.Id == question.QuizId);

            if (quiz == null)
                return NotFound();

                _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return Ok(question);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }
            _context.Entry(question).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(question);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Question>> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
