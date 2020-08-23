using Microsoft.EntityFrameworkCore;
using Project_Backend.Models.Data;
using Project_Backend.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Backend.Models.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly MusicKnowDbContext _context;

        public QuizRepository(MusicKnowDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Quizzes>> GetAllQuizzesAsync()
        {
            try
            {
                var result = await _context.Quizzes.Include(a => a.AppUser).Include(q => q.Questions).ThenInclude(q => q.Answers).ToListAsync<Quizzes>();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Quizzes>> GetQuizzesForUserAsync(string applicationuserid)
        {
            try
            {
                var result = await _context.Quizzes.Where(n => n.AppUserId == applicationuserid).ToListAsync<Quizzes>();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }

        public async Task<Quizzes> GetQuizForQuizIdAsync(Guid quizid)
        {
            try
            {
                var result = await _context.Quizzes.Include(p => p.Questions).ThenInclude(it => it.Answers).SingleOrDefaultAsync<Quizzes>(p => p.QuizId == quizid);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }

        public async Task<Quizzes> AddQuizAsync(Quizzes quiz)
        {
            try
            {
                var result = _context.Quizzes.AddAsync(quiz);
                await _context.SaveChangesAsync();
                return quiz;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }

        public async Task<Quizzes> DeleteAsyncApi(Guid quizId)
        {
            try
            {
                Quizzes quiz = await GetQuizForQuizIdAsync(quizId);
                ApiDelete api = new ApiDelete();
                api.DeletedItem = quiz.QuizId.ToString();
                api.DeletedName = quiz.Name;
                api.DeletedDescription = quiz.Description;
                api.DeletedDifficulty = quiz.Difficulty.ToString();
                api.DeletedQuestionCount = quiz.QuestionCount.ToString();
                await AddQuizDeletedAsync(api);
                return quiz;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }
        public async Task<Quizzes> DeleteAsync(Guid quizId)
        {
            try
            {
                Quizzes quiz = await GetQuizForQuizIdAsync(quizId);
                var result = _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
                ApiDelete api = new ApiDelete();
                api.DeletedItem = quiz.QuizId.ToString();
                api.DeletedName = quiz.Name;
                api.DeletedDescription = quiz.Description;
                api.DeletedDifficulty = quiz.Difficulty.ToString();
                api.DeletedQuestionCount = quiz.QuestionCount.ToString();
                await AddQuizDeletedAsync(api);
                return quiz;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }

        public async Task<Quizzes> EditQuizAsync(Quizzes quiz)
        {
            try
            {
                var result = _context.Quizzes.Update(quiz);
                await _context.SaveChangesAsync();
                return quiz;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }

        public async Task<ApiDelete> AddQuizDeletedAsync(ApiDelete api)
        {
            try
            {
                var result = _context.ApiDeletes.AddAsync(api);
                await _context.SaveChangesAsync();
                return api;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }
    }
}
