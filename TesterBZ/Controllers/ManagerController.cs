using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesterBZ.Models;

namespace TesterBZ.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : BaseController
    {
        public ActionResult Index()
        {
            var data = Context.Tests.Select(x => new AvailableTestViewModel
            {
                QuestionsCount = x.Questions.Count,
                TestId = x.TestId,
                TestName = x.TestName
            }).ToList();

            return View(data);
        }

        public ActionResult RemoveTest(int id)
        {
            var userAnswers = Context.UserAnswers.Where(x => x.Question.TestId == id).ToList();
            Context.TestsAdmissions.RemoveRange(Context.TestsAdmissions.Where(x => x.TestId == id));

            Context.UserAnswers.RemoveRange(userAnswers);

            Context.Answers.RemoveRange(Context.Answers.Where(x => x.Question.TestId == id).ToList());

            Context.Questions.RemoveRange(Context.Questions.Where(x => x.TestId == id).ToList());

            Context.Tests.Remove(Context.Tests.FirstOrDefault(x => x.TestId == id));

            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Test Id</param>
        /// <returns></returns>
        public ActionResult AccessManage(int id)
        {
            var test = Context.Tests.FirstOrDefault(x => x.TestId == id);
            var model = new AccessManageViewModel
            {
                TestId = id,
                TestName = test.TestName
            };
            model.UsersAccesses = Context.Users.Select(x => new UserTestAccessModel
            {
                FullName = x.LastName + " " + x.FirstName,
                UserId = x.Id
            }).ToList();
            foreach (var item in model.UsersAccesses)
            {
                try
                {
                    item.IsAvailable = test.TestsAdmissions.Any(x => x.User.Id == item.UserId);
                }
                catch { }
            }
            return View(model);
        }

        public ActionResult SetAccess(bool value, string userId, int testId)
        {
            if (value)
            {
                if (!Context.TestsAdmissions.Any(x => x.TestId == testId && x.User.Id == userId))
                {
                    Context.TestsAdmissions.Add(new TesterBZDomainModel.Models.TestAdmission
                    {
                        TestId = testId,
                        User = Context.Users.FirstOrDefault(x => x.Id == userId)
                    });
                    Context.SaveChanges();
                }
            }
            else
            {
                if (Context.TestsAdmissions.Any(x => x.TestId == testId && x.User.Id == userId))
                {
                    Context.TestsAdmissions.Remove(Context.TestsAdmissions.FirstOrDefault(x => x.TestId == testId && x.User.Id == userId));
                    Context.SaveChanges();
                }
            }
            return RedirectToAction("AccessManage", new { id = testId });
        }

        [HttpGet]
        public ActionResult AddTest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTest(string testName)
        {
            Context.Tests.Add(new TesterBZDomainModel.Models.Test
            {
                Questions = new List<TesterBZDomainModel.Models.Question>(),
                TestName = testName,
                TestsAdmissions = new List<TesterBZDomainModel.Models.TestAdmission>()
            });
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditTest(int id)
        {
            var model = Context.Tests.Where(x => x.TestId == id).Select(x => new TestViewModel
            {
                TestId = x.TestId,
                TestName = x.TestName,
                Questions = x.Questions.Select(y => new QuestionViewModel
                {
                    QuestionId = y.QuestionId,
                    QuestionNumber = 0,
                    QuestionText = y.QuestionText,
                    QuestionImage = y.QuestionImage,
                    QuestionBlockId=y.QuestionBlockId,
                    QuestionBlockName=y.QuestionBlock.BlockName
                }).ToList(),
                QuestionBlocks = x.QuestionsBlocks.ToList()
            }).FirstOrDefault();
            return View(model);
        }

        public ActionResult EditMainTestInfo(int testId, string testName)
        {
            Context.Tests.FirstOrDefault(x => x.TestId == testId).TestName = testName;
            Context.SaveChanges();
            return RedirectToAction("EditTest", new { id = testId });
        }

        public ActionResult RemoveQuestion(int id)
        {
            var qstn = Context.Questions.FirstOrDefault(x => x.QuestionId == id);
            var testId = qstn.TestId;
            Context.UserAnswers.RemoveRange(Context.UserAnswers.Where(x => x.QuestionId == qstn.QuestionId).ToList());
            Context.Answers.RemoveRange(Context.Answers.Where(x => x.QuestionId == qstn.QuestionId).ToList());
            Context.Questions.Remove(qstn);
            Context.SaveChanges();
            return RedirectToAction("EditTest", new { id = testId });
        }

        public ActionResult RemoveQuestionBlock(int id)
        {
            var block = Context.QuestionBlocks.FirstOrDefault(x => x.QuestionBlockId == id);
            var testId = block.TestId;
            Context.UserAnswers.RemoveRange(Context.UserAnswers.Where(x => x.Question.QuestionBlockId == id).ToList());
            Context.Answers.RemoveRange(Context.Answers.Where(x => x.Question.QuestionBlockId == id).ToList());
            Context.Questions.RemoveRange(Context.Questions.Where(x => x.QuestionBlockId == id).ToList());
            Context.QuestionBlocks.Remove(block);
            Context.SaveChanges();
            return RedirectToAction("EditTest", new { id = testId });
        }

        [HttpGet]
        public ActionResult AddQuestion(int testId)
        {
            var model = new AddQuestionViewModel
            {
                TestId = testId,
                Blocks = Context.QuestionBlocks.Where(x => x.TestId == testId).Select(x => new { x.BlockName, x.QuestionBlockId }).ToList().Select(x => new KeyValuePair<int, string>(x.QuestionBlockId, x.BlockName)).ToList(),
                AnswerTypes = Context.AnswerTypes.ToList().Select(x => new KeyValuePair<int, string>(x.AnswerTypeId, x.AnswerTypeName)).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddQuestion(HttpPostedFileBase questionImage, int testId, string questionText, int blockId, int answerTypeId)
        {
            var path = Server.MapPath("/Content/QuestionsImages");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filename = null;
            if (questionImage != null)
            {
                filename = Guid.NewGuid().ToString() + ".jpg";
                questionImage.SaveAs(path + "/" + filename);
            }
            Context.Questions.Add(new TesterBZDomainModel.Models.Question
            {
                Answers = new List<TesterBZDomainModel.Models.Answer>(),
                AnswerTypeId = answerTypeId,
                QuestionBlockId = blockId,
                QuestionImage = questionImage==null?null:"/Content/QuestionsImages/" + filename,
                QuestionText = questionText,
                TestId = testId
            });
            Context.SaveChanges();
            return RedirectToAction("EditTest", new { id = testId });
        }

        [HttpGet]
        public ActionResult AddQuestionBlock(int testId)
        {
            var model = new AddQuestionBlockViewModel
            {
                TestId = testId
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddQuestionBlock(int testId, string blockName)
        {
            Context.QuestionBlocks.Add(new TesterBZDomainModel.Models.QuestionBlock
            {
                TestId = testId,
                Questions = new List<TesterBZDomainModel.Models.Question>(),
                BlockName = blockName,
            });
            Context.SaveChanges();
            return RedirectToAction("EditTest", new { id = testId });
        }

        [HttpGet]
        public ActionResult EditBlock(int id)
        {
            var block = Context.QuestionBlocks.FirstOrDefault(x => x.QuestionBlockId == id);
            var questionBlock = new EditQuestionBlockViewModel
            {
                BlockId=id,
                BlockName= block.BlockName,
                TestId=block.TestId
            };
            return View(questionBlock);
        }

        [HttpPost]
        public ActionResult EditBlock(int blockId,string blockName)
        {
            Context.QuestionBlocks.FirstOrDefault(x => x.QuestionBlockId == blockId).BlockName = blockName;
            Context.SaveChanges();
            return RedirectToAction("EditTest",new { id=blockId });
        }

        [HttpGet]
        public ActionResult EditQuestion(int id)
        {
            
            return View();
        }

        public ActionResult QuestionAnswersList(int id)
        {
            var model = Context.Questions.Where(x => x.QuestionId == id).Select(x => new QuestionAnswersViewModel
            {
                TestId=x.TestId,
                QuestionName=x.QuestionText,
                QuestionImage=x.QuestionImage,
                QuestionId=x.QuestionId,
                QuestionAnswers=x.Answers.Select(y=>new QuestionAnswerViewModel
                {
                    AnswerId=y.AnswerId,
                    AnswerImage=y.AnswerImage,
                    AnswerText=y.AnswerText,
                    AnswerTypeId=x.AnswerTypeId
                }).ToList()
            }).FirstOrDefault();
            return View(model);
        }

        public ActionResult AddAnswer(int questionId)
        {
            var model = new AddAnswerViewModel
            {
                QuestionId=questionId
            };
            return View(model);
        }

    }
}