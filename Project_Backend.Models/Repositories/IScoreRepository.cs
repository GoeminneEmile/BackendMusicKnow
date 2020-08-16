using Project_Backend.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Backend.Models.Repositories
{
    public interface IScoreRepository
    {
        Task<Score> AddScoreAsync(Score score);
        Task<IList<Score>> GetAllScoresForQuizzesAsync(string quizid);
    }
}