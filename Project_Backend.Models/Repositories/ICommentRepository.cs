using Project_Backend.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Backend.Models.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> AddCommentAsync(Comment comments);
        Task<IList<Comment>> GetAllCommentsForQuizzesAsync(string quizid);
    }
}