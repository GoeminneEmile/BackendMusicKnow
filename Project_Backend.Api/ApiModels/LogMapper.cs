using Project_Backend.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Backend.Api.ApiModels
{
    public static class LogMapper
    {
        public static Logs_DTO ConvertTo_DTO(Quizzes quiz, ref Logs_DTO logs_DTO)
        {
            try
            {
                logs_DTO.DeletedItem = quiz.QuizId.ToString();
                logs_DTO.DeletedName = quiz.Name;
                logs_DTO.DeletedDescription = quiz.Description;
                logs_DTO.DeletedDifficulty = quiz.Difficulty.ToString();
                logs_DTO.DeletedQuestionCount = quiz.QuestionCount.ToString();

                return logs_DTO;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("foutcode: ", ex);
                return logs_DTO;
            }

        }
    }
}
