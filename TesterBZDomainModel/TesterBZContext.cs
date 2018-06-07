using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterBZDomainModel.Models;

namespace TesterBZDomainModel
{
    public class TesterBZContext: IdentityDbContext<ApplicationUser>
    {
        public TesterBZContext():base("TesterBZContext",false)
        {

        }

        public static TesterBZContext Create()
        {
            return new TesterBZContext();
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }
        public virtual DbSet<TestAdmission> TestsAdmissions { get; set; }
        public virtual DbSet<QuestionBlock> QuestionBlocks { get; set; }
        public virtual DbSet<AnswerType> AnswerTypes { get; set; }
    }
}
