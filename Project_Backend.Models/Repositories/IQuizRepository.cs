using Project_Backend.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Backend.Models.Repositories
{
    public interface IQuizRepository
    {
        Task<Quizzes> AddQuizAsync(Quizzes quiz);
        Task<Quizzes> EditQuizAsync(Quizzes quiz);
        Task<Quizzes> DeleteAsync(Guid quizid);
        Task<Quizzes> DeleteAsyncApi(Guid quizid);
        Task<IEnumerable<Quizzes>> GetAllQuizzesAsync();
        Task<Quizzes> GetQuizForQuizIdAsync(Guid quizid);
        Task<IEnumerable<Quizzes>> GetQuizzesForUserAsync(string appuserid);
        Task<ApiDelete> AddQuizDeletedAsync(ApiDelete api);
    }
}