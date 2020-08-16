using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Backend.Models.Models;

namespace Project_Backend.Api.ApiModels
{
    public class ScoreMapper
    {
        public static ScoreTable_DTO ConvertTo_DTO(Score score, ref ScoreTable_DTO score_DTO)
        {
            score_DTO.QuizName = (score_DTO.QuizName is null) ? "" : score.Quiz.Name;
            score_DTO.ApplicationUser = (score.AppUser is null) ? "" : score.AppUser.UserName;
            score_DTO.FinalScore = score.FinalScore;
            score_DTO.MaxScore = score.MaxScore;
            score_DTO.Datefinished = score.DateFinished;
            return score_DTO;
        }
    }
}
