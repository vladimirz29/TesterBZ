using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterBZDomainModel.Models
{
    public class SchemeMask
    {
        public int SchemeMaskId { get; set; }
        public string Mask { get; set; }
        public int? Value { get; set; }
        public string SmallMessage { get; set; }
        public string LargeMessage { get; set; }
        public int? CalculationSchemeId { get; set; }
        public virtual CalculationScheme CalculationScheme { get; set; }
    }
}
