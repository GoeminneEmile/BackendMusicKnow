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
    public class CommentRepository : ICommentRepository
    {
        private readonly MusicKnowDbContext _context;

        public CommentRepository(MusicKnowDbContext context)
        {
            this._context = context;
        }
        public async Task<Comment> AddCommentAsync(Comment comments)
        {
            try
            {
                var result = _context.Comments.AddAsync(comments);
                await _context.SaveChangesAsync();
                return comments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return null;
            }
        }
        public async Task<IList<Comment>> GetAllCommentsForQuizzesAsync(string quizid)
        {
            try
            {
                var fdsfdsfs = await _context.Comments.Include(q => q.Quiz).ThenInclude(a => a.AppUser).Where(n => n.QuizId == Guid.Parse(quizid)).OrderByDescending(s => s.Comments).ToListAsync();
                var result = await _context.Comments.Include(e => e.Quiz)
                                                  .ThenInclude(e => e.AppUser)
                                                  .GroupBy(e => new { e.QuizId, e.AppUserId, e.Comments })
                                                  .Select(e => new { User = e.Key.AppUserId, Comment = e.Key.Comments, quiz = e.Key.QuizId })
                                                  .Where(e => e.User == quizid)
                                                  .ToDictionaryAsync(e => new Comment { Comments = e.Comment, AppUserId = e.User, });
                List<Comment> comments = new List<Comment>();
                foreach (var obj in result)
                {
                    Comment comment = new Comment()
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
    }
}
