using Project_Backend.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Backend.Models.Repositories
{
    public interface IAnswerRepository
    {
        Task<Answer> AddAnswerAsync(Answer answer);
        Task<IList<Answer>> GetAnswersForQuestionAsync(string questionId);
    }
}
