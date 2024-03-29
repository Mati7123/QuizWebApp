using System;
using Microsoft.EntityFrameworkCore;
using quiz_backend.Models;

namespace quiz_backend
{
    public class QuizContext: DbContext
    {
       public QuizContext(DbContextOptions<QuizContext> options) : base (options) {  }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<quiz_backend.Models.Quiz> Quiz { get; set; }
        public DbSet<FavoriteQuiz> FavoriteQuizzes { get; set; }
    }
}
