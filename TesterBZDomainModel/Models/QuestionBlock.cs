using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterBZDomainModel.Models
{
    public class QuestionBlock
    {
        [Key]
        public int QuestionBlockId { get; set; }
        public string BlockName { get; set; }
        public virtual List<Question> Questions { get; set; }
        public int? TestId { get; set; }
        public Test Test { get; set; }
        public string Annotation { get; set; }
    }
}
