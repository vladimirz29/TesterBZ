using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterBZDomainModel.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual List<TestAdmission> TestsAdmissions { get; set; }
        public virtual List<QuestionBlock> QuestionsBlocks { get; set; }
    }
}
