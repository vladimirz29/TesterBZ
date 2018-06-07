using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesterBZ.Models;
using TesterBZDomainModel;
using TesterBZDomainModel.Models;

namespace TesterBZ.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (UserProfile.Roles.Any(x=>x.RoleId=="2"))
                return RedirectToAction("Index", "Manager");
            var model = new List<AvailableTestViewModel>();
            model=UserProfile.TestsAdmissions.Select(x => new AvailableTestViewModel
            {
                QuestionsCount=x.Test.Questions.Count,
                TestId=x.TestId,
                TestName=x.Test.TestName
            }).ToList();
            return View(model);
        }

        public ActionResult TestResults(int id)
        {
            var model = new TestResultViewModel
            {
                LastName=UserProfile.LastName,
                FirstName=UserProfile.FirstName,
                Score=UserProfile.UserAnswers.Where(x=>x.Value!=null && x.Question.TestId==id).Sum(x=>x.Value).Value
            };
            return View(model);
        }


        public ActionResult InsideTest(int id, int question=1)
        {
            if (Context.Tests.FirstOrDefault(x => x.TestId == id).Questions.Count<question)
            {
                return RedirectToAction("TestResults",new { id });
            }
            var qstn=Context.Tests.FirstOrDefault(x => x.TestId == id).Questions.Skip(question-1).FirstOrDefault();
            var model = new QuestionViewModel
            {
                QuestionId = qstn.QuestionId,
                QuestionText = qstn.QuestionText,
                QuestionAnswers = qstn.Answers.OrderBy(x => x.AnswerWeight).Select(x => x.AnswerText).ToList(),
                QuestionNumber = question,
                TotalQuestions = Context.Questions.Where(x => x.TestId == id).Count()
            }; 
            return View(model);
        }

        public ActionResult Answer(int id, int question, int value)
        {
            var name= User.Identity.Name;
            Context.Users.FirstOrDefault(x=>x.UserName==name).UserAnswers.Add(new UserAnswer
            {
                QuestionId = id,
                
                Value = value-2
            });
            Context.SaveChanges();
            return RedirectToAction("InsideTest", new { id = Context.Questions.FirstOrDefault(x => x.QuestionId == id).TestId, question = question + 1 });
        }

        public ActionResult SkipAnswer(int id, int question)
        {
            var name = User.Identity.Name;
            Context.Users.FirstOrDefault(x => x.UserName == name).UserAnswers.Add(new UserAnswer
            {
                QuestionId=Context.Questions.FirstOrDefault(x=>x.QuestionId==id).QuestionId,
                Value=null
            });
            Context.SaveChanges();
            return RedirectToAction("InsideTest",new { id=Context.Questions.FirstOrDefault(x=>x.QuestionId==id).TestId,question=question+1 });
        }

    }
}