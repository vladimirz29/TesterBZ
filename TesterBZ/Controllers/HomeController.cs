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
            if (UserProfile.Roles.Any(x => x.RoleId == "2"))
                return RedirectToAction("Index", "Manager");
            var model = new List<AvailableTestViewModel>();
            model = UserProfile.TestsAdmissions.Select(x => new AvailableTestViewModel
            {
                QuestionsCount = x.Test.Questions.Count,
                TestId = x.TestId,
                TestName = x.Test.TestName
            }).ToList();
            return View(model);
        }

        public ActionResult TestResults(int id)
        {
            if (Context.UserAnswers.Where(x => x.Question.TestId == id).Count() < Context.Questions.Count(x => x.TestId == id))
                return RedirectToAction("Index");
            var test = Context.Tests.FirstOrDefault(x => x.TestId == id);
            var blockResults = new List<BlockResultViewModel>();

            foreach (var item in test.QuestionsBlocks)
            {
                var rule = item.CalculationScheme.CalculationRule;
                var type = rule.Substring(0, 5);
                var param = rule[5];
                var arg = rule.Substring(rule.IndexOf('=') + 1);
                if (type == "pmask") //Перевалочная маска
                {
                    if (param == 'e') //каждые n
                    {
                        var list = new List<string>();
                        for (int t = 0; t < int.Parse(arg); t++)
                        {
                            var answs = new List<int>();
                            for (int i = t; i < item.Questions.Count; i += int.Parse(arg))
                            {
                                try
                                {
                                    var questionId = item.Questions[i].QuestionId;
                                    answs.Add(Context.UserAnswers.FirstOrDefault(x => x.QuestionId == questionId).Value.Value);
                                }
                                catch { break; }
                            }
                            list.Add(answs.Count(x => x == 1) > answs.Count(x => x == 0) ? "1" : "0");
                        }
                        string mask = string.Join("", list);
                        if (item.CalculationScheme.SchemeMasks.Any(x=>x.Mask==mask))
                        {
                            var maskDB = item.CalculationScheme.SchemeMasks.FirstOrDefault(x => x.Mask == mask);
                            blockResults.Add(new BlockResultViewModel
                            {
                                BlockName=item.BlockName,
                                ShortText=maskDB.SmallMessage,
                                LargeText=maskDB.LargeMessage
                            });
                        }
                    }
                }
            }

            var model = new TestResultViewModel
            {
                LastName = UserProfile.LastName,
                FirstName = UserProfile.FirstName,
                Score = UserProfile.UserAnswers.Where(x => x.Value != null && x.Question.TestId == id).Sum(x => x.Value).Value,
                BlocksResults=blockResults
            };
            return View(model);
        }

        public ActionResult InsideTest(int id, int question = 1)
        {
            if (Context.Tests.FirstOrDefault(x => x.TestId == id).Questions.Count < question || Context.Tests.FirstOrDefault(x=>x.TestId==id).Questions.Count<=Context.UserAnswers.Where(x=>x.Question.TestId==id).Count())
            {
                return RedirectToAction("TestResults", new { id });
            }
            var qstn = Context.Tests.FirstOrDefault(x => x.TestId == id).Questions.OrderBy(x=>x.QuestionBlockId).Skip(question - 1).FirstOrDefault();

            var model = new QuestionViewModel
            {
                QuestionId = qstn.QuestionId,
                QuestionText = qstn.QuestionText,
                QuestionAnswers = qstn.Answers.OrderBy(x => x.AnswerWeight).Select(x => new QuestionAnswerViewModel
                {
                    AnswerId = x.AnswerId,
                    AnswerImage = x.AnswerImage,
                    AnswerText = x.AnswerText,
                    AnswerTypeId = x.Question.AnswerTypeId
                }).ToList(),
                QuestionNumber = question,
                TotalQuestions = Context.Questions.Where(x => x.TestId == id).Count(),
                Annotation = question == 1 ? qstn.QuestionBlock.Annotation : null,
                AnswerTypeSyncCode = qstn.AnswerType.SyncCode
            };
            return View(model);
        }

        public ActionResult Answer(int id, int question, int value)
        {
            var name = User.Identity.Name;
            Context.Users.FirstOrDefault(x => x.UserName == name).UserAnswers.Add(new UserAnswer
            {
                QuestionId = id,

                Value = value
            });
            Context.SaveChanges();
            return RedirectToAction("InsideTest", new { id = Context.Questions.FirstOrDefault(x => x.QuestionId == id).TestId, question = question + 1 });
        }

        public ActionResult SkipAnswer(int id, int question)
        {
            var name = User.Identity.Name;
            Context.Users.FirstOrDefault(x => x.UserName == name).UserAnswers.Add(new UserAnswer
            {
                QuestionId = Context.Questions.FirstOrDefault(x => x.QuestionId == id).QuestionId,
                Value = null
            });
            Context.SaveChanges();
            return RedirectToAction("InsideTest", new { id = Context.Questions.FirstOrDefault(x => x.QuestionId == id).TestId, question = question + 1 });
        }

    }
}