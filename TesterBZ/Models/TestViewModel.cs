using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesterBZDomainModel.Models;

namespace TesterBZ.Models
{
    public class TestViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public List<QuestionBlock> QuestionBlocks { get; set; }
    }
}