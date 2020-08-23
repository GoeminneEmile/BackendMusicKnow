using Project_Backend.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Backend.Models.ViewModels
{
    public class ScoreUserVM
    {
        public Guid QId { get; set; }
        public string QName { get; set; }
        public int QuestionCount { get; set; }
        public int QIndex { get; set; }
        public Questions Question { get; set; }
        public IList<Answer> Answers { get; set; }
        public IList<Comment> comments { get; set; }
    }
}
