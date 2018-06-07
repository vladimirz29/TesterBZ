using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesterBZ.Models
{
    public class QuestionAnswerViewModel
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerImage { get; set; }
        public int? AnswerTypeId { get; set; }
    }
}