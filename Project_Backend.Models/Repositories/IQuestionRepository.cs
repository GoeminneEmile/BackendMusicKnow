using Project_Backend.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Backend.Models.Repositories
{
    public interface IQuestionRepository
    {
        Task<Questions> AddQuestionAsync(Questions question);
        Task<IList<Questions>> GetQuestionsForQuizAsync(Guid quizid);
    }
}