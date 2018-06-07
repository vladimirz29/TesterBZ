using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterBZDomainModel.Models
{
    public class UserAnswer
    {
        public int UserAnswerId { get; set; }
        public int? Value { get; set; }
        /// <summary>
        /// UserProfile id
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
