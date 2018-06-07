using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterBZ.Models
{
    public class AddQuestionViewModel
    {
        public int TestId { get; set; }
        public List<KeyValuePair<int,string>> Blocks { get; set; }
        public List<KeyValuePair<int, string>> AnswerTypes { get; set; }
    }
}
