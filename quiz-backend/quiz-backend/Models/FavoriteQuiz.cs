using System;
namespace quiz_backend.Models
{
    public class FavoriteQuiz
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string UserId  { get; set; }
    }
}
