using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterBZDomainModel.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionImage { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
        public int? QuestionBlockId { get; set; }
        public virtual QuestionBlock QuestionBlock { get; set; }
        public int? AnswerTypeId { get; set; }
        public AnswerType AnswerType { get; set; }
    }
}
