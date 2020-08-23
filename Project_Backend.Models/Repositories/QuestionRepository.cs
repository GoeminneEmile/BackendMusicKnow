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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly MusicKnowDbContext _context;

        public QuestionRepository(MusicKnowDbContext context)
        {
            this._context = context;
        }

        public async Task<IList<Questions>> GetQuestionsForQuizAsync(Guid quizid)
        {
            try
            {
                var result = await _context.Questions.Include(a => a.Answers).Where(q => q.QuizId == quizid).ToListAsync<Questions>();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }

        public async Task<Questions> AddQuestionAsync(Questions question)
        {
            try
            {
                var result = await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();
                return question;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }
    }
}
