using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterBZDomainModel.Models
{
    public class AnswerType
    {
        public int AnswerTypeId { get; set; }
        public string AnswerTypeName { get; set; }
        public string SyncCode { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}
