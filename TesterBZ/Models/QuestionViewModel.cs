using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesterBZ.Models
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<string> QuestionAnswers { get; set; }
        public int QuestionNumber { get; set; }
        public int TotalQuestions { get; set; }
        public string QuestionImage { get; set; }
        public string QuestionBlockName { get; set; }
        public int? QuestionBlockId { get; set; }
    }
}