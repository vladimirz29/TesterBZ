using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterBZDomainModel.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerImage { get; set; }
        public int AnswerWeight { get; set; }
        public int? QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
