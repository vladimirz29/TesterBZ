using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterBZDomainModel.Models
{
    public class CalculationScheme
    {
        public int CalculationSchemeId { get; set; }
        public string CalculationSchemeName { get; set; }
        public virtual List<QuestionBlock> QuestionsBlocks { get; set; }
        public virtual List<SchemeMask> SchemeMasks { get; set; }
        public string CalculationRule { get; set; }
    }
}
