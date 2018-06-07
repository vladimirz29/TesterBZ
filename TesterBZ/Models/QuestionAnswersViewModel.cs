using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesterBZ.Models
{
    public class QuestionAnswersViewModel
    {
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public string QuestionImage { get; set; }
        public List<QuestionAnswerViewModel> QuestionAnswers { get; set; }
    }
}