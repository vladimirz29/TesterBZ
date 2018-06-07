using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterBZDomainModel.Models
{
    public class TestAdmission
    {
        [Key]
        public int TestAdmissionId { get; set; }
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
