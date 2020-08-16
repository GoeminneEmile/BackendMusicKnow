using Microsoft.EntityFrameworkCore;
using Project_Backend.Models.Data;
using Project_Backend.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Backend.Models.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly MusicKnowDbContext _context;

        public AnswerRepository(MusicKnowDbContext context)
        {
            this._context = context;
        }

        public async Task<IList<Answer>> GetAnswersForQuestionAsync(string questionId)
        {
            try
            {
                var result = await _context.Answers.Where(n => n.QuestionId == Guid.Parse(questionId)).ToListAsync<Answer>();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }

        public async Task<Answer> AddAnswerAsync(Answer answer)
        {
            try
            {
                await _context.Answers.AddAsync(answer);
                await _context.SaveChangesAsync();
                return answer;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }
    }
}
