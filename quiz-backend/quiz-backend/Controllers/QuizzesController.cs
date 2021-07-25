using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_backend.Models;

namespace quiz_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly QuizContext _context;

        public QuizzesController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/Quizzes
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuiz()
        {
            var userId = HttpContext.User.Claims.First().Value;

            return await _context.Quiz.Where(q => q.OwnerId == userId).ToListAsync();
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetAllQuiz()
        {
       
            return await _context.Quiz.ToListAsync();
        }

        // GET: api/Quizzes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quiz.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetMyFavoriteQuiz()
        {
            var userId = HttpContext.User.Claims.First().Value;

            var userFavorite = await _context.FavoriteQuizzes.Where(q => q.UserId == userId).ToListAsync();

            List<Quiz> myfavoirteQuizzes = new List<Quiz>();

            foreach (var favoriteQuiz in userFavorite)
            {
                myfavoirteQuizzes.Add(await _context.Quiz.Where(q => q.Id == favoriteQuiz.QuizId).FirstOrDefaultAsync());    
            }
            
            if (myfavoirteQuizzes == null)
            {
                return NotFound();
            }

            return myfavoirteQuizzes;
        }

        // PUT: api/Quizzes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [Authorize]
        [HttpPost("my/{id}")]
        public async Task<ActionResult<FavoriteQuiz>> PostFavoriteQuiz(int id)
        {
            FavoriteQuiz favoriteQuiz = new FavoriteQuiz();

            favoriteQuiz.QuizId = id;

            var userId = HttpContext.User.Claims.First().Value;

            favoriteQuiz.UserId = userId;

            var favoriteQuizzessContex = _context.FavoriteQuizzes;
            if (!favoriteQuizzessContex.Any(fq => fq.QuizId == id && fq.UserId == userId)){
                favoriteQuizzessContex.Add(favoriteQuiz);
            }
          
            await _context.SaveChangesAsync();

            return Ok(favoriteQuiz);
        }

        [Authorize]
        [HttpDelete("my/{id}")]
        public async Task<ActionResult<FavoriteQuiz>> DeleteFavoriteQuiz(int id)
        {
            var userId = HttpContext.User.Claims.First().Value;
            var favoriteQuizzessContex = _context.FavoriteQuizzes;

            if (favoriteQuizzessContex.Any(fq => fq.QuizId == id && fq.UserId == userId))
            {
                var favoriteToDelete = favoriteQuizzessContex.Where(fq => fq.QuizId == id && fq.UserId == userId).First();
                favoriteQuizzessContex.Remove(favoriteToDelete);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/Quizzes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.Claims.First().Value;

            quiz.OwnerId = userId;

            _context.Quiz.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Quiz>> DeleteQuiz(int id)
        {
            var quiz = await _context.Quiz.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quiz.Remove(quiz);
            var questions = _context.Questions.Where(questions => questions.QuizId == id);
            foreach (var question in questions)
            {
                _context.Questions.Remove(question);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(int id)
        {
            return _context.Quiz.Any(e => e.Id == id);
        }
    }
}
