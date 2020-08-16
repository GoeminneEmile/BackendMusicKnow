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
    public class ScoreRepository : IScoreRepository
    {
        private readonly MusicKnowDbContext _context;

        public ScoreRepository(MusicKnowDbContext MusicKnowDbContext)
        {
            this._context = MusicKnowDbContext;
        }
        public async Task<IList<Score>> GetAllScoresForQuizzesAsync(string quizid)
        {
            try
            {
                var fdsfdsfs = await _context.Scores.Include(q => q.Quiz).ThenInclude(a => a.AppUser).Where(n => n.QuizId == Guid.Parse(quizid)).OrderByDescending(s => s.FinalScore).ToListAsync();
                var result = await _context.Scores.Include(e => e.Quiz)
                                                  .ThenInclude(e => e.AppUser)
                                                  .GroupBy(e => new { e.QuizId, e.AppUserId, e.MaxScore })
                                                  .Select(e => new { Score = e.Max(x => x.FinalScore), User = e.Key.AppUserId, maxScore = e.Key.MaxScore, quiz = e.Key.QuizId })
                                                  .Where(e => e.User == quizid)
                                                  .OrderByDescending(e => e.Score)
                                                  .ToDictionaryAsync(e => new Score { FinalScore = e.Score, AppUserId = e.User, MaxScore = e.maxScore });
                List<Score> scores = new List<Score>();
                foreach (var obj in result)
                {
                    Score score = new Score()
                    {
                        QuizId = obj.Value.quiz
                    };
                }
                return fdsfdsfs;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }

        public async Task<Score> AddScoreAsync(Score score)
        {
            try
            {
                var result = _context.Scores.AddAsync(score);
                await _context.SaveChangesAsync();
                return score;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }
    }
}
