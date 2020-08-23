using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Backend.Models.Models;
using Project_Backend.Models.Repositories;
using Project_Backend.Models.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Project_Backend.Web
{
    public class QuizzesController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment _he;
        private readonly IQuizRepository _quizRepository;
        private readonly IScoreRepository _scoreRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [Obsolete]
        public QuizzesController(IQuizRepository quizRepository, ICommentRepository commentRepository, IAnswerRepository answerRepository, IQuestionRepository questionRepository, IScoreRepository scoreRepository, UserManager<AppUser> userManager, IHostingEnvironment e, IHttpContextAccessor httpContextAccessor)
        {
            _he = e;
            _userManager = userManager;
            _quizRepository = quizRepository;
            _scoreRepository = scoreRepository;
            _commentRepository = commentRepository;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: Quiz
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult> QuizzesAsync()
        {
            var result = await _quizRepository.GetAllQuizzesAsync();
            return View(result);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> MyQuizzesAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _quizRepository.GetQuizzesForUserAsync(userId);
            return View(result);
        }

        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult> ScoresQuizAsync(string quizid)
        {
            var result = await _scoreRepository.GetAllScoresForQuizzesAsync(quizid);
            ViewBag.quizid = quizid;
            return View(result);
        }

        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult> CommentQuizAsync(string quizid)
        {
            var result = await _commentRepository.GetAllCommentsForQuizzesAsync(quizid);
            ViewBag.quizid = quizid;
            Debug.WriteLine(result);
            return View("CommentsQuiz", result);
        }

        [Authorize(Roles = "Administrator, User")]
        public ActionResult ResultQuiz()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult> StartQuizAsync(string quizid)
        {
            var result = await _quizRepository.GetQuizForQuizIdAsync(Guid.Parse(quizid));
            return View(result);
        }

        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult> PlayQuizAsync(string quizid)
        {
            try
            {
                Quizzes quiz = await _quizRepository.GetQuizForQuizIdAsync(Guid.Parse(quizid));
                ScoreUserVM scoreUser_VM = new ScoreUserVM() { QName = quiz.Name, QId = quiz.QuizId };
                var questions = await _questionRepository.GetQuestionsForQuizAsync(scoreUser_VM.QId);
                var question = questions[scoreUser_VM.QIndex];

                var answers = await _answerRepository.GetAnswersForQuestionAsync(question.QuestionId.ToString());

                scoreUser_VM.Question = question;
                scoreUser_VM.Answers = answers;
                ViewBag.Score = 0;
                return View("PlayQuiz", scoreUser_VM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return View();
            }
        }

        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult> NextQuestion(Guid QId, string QName, int QIndex, string Correct, int Score)
        {
            try
            {
                ScoreUserVM score1 = new ScoreUserVM() { QName = QName, QId = QId, QIndex = QIndex };
                var questions = await _questionRepository.GetQuestionsForQuizAsync(score1.QId);
                score1.QIndex++;
                if (Correct == "correct") { Score += 1; }
                if (score1.QIndex >= questions.Count())
                {
                    Score newscore = new Score()
                    {
                        AppUserId = _userManager.GetUserId(User),
                        QuizId = score1.QId,
                        MaxScore = questions.Count(),
                        FinalScore = Score
                    };
                    await _scoreRepository.AddScoreAsync(newscore);
                    return View("ResultQuiz", newscore);
                }
                else
                {
                    var objquestion = questions[score1.QIndex];
                    IList<Answer> answers = await _answerRepository.GetAnswersForQuestionAsync(objquestion.QuestionId.ToString());
                    score1.Question = objquestion;
                    score1.Answers = answers;
                    ViewBag.score = Score;
                    return View("PlayQuiz", score1);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CreateQuiz() => View();

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuiz(Quizzes quiz)
        {
            try
            {
                IList<Questions> questions = new List<Questions>();
                for (var i = 0; i < quiz.QuestionCount; i++)
                {
                    List<Answer> answers = new List<Answer>();
                    for (var j = 0; j < 4; j++)
                    {
                        Answer answer = new Answer() { };
                        answers.Add(answer);
                    }
                    Questions question1 = new Questions() { Answers = answers, Quiz = quiz };
                    questions.Add(question1);
                }
                ViewBag.QuizId = quiz.QuizId;
                ViewBag.QuizName = quiz.Name;
                ViewBag.QuizDescript = quiz.Description;
                ViewBag.QuizDiff = quiz.Difficulty;
                return View("AddQuestionToQuiz", questions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteQuiz(string quizid)
        {
            try
            {
                Quizzes quiz = await _quizRepository.GetQuizForQuizIdAsync(Guid.Parse(quizid));
                var result = await _quizRepository.DeleteAsync(quiz.QuizId);
                return View("MyQuizzes", result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<ActionResult> AddQuestionToQuiz(IList<Questions> questions, List<IFormFile> questionimg, List<string> listimgindex, string quizname, string quizdiff, string quizdescript)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var uploadPath = Path.Combine(_he.WebRootPath, "images");
                int imgCounter = 0;
                int isaindex = 0;
                Quizzes quiz = new Quizzes() { Name = quizname, Description = quizdescript, Difficulty = (Quizzes.Difficulties)Enum.Parse(typeof(Quizzes.Difficulties), quizdiff), QuestionCount = questions.Count(), AppUserId = userId };
                await _quizRepository.AddQuizAsync(quiz);
                foreach (var question in questions)
                {
                    question.Quiz = quiz;
                    await _questionRepository.AddQuestionAsync(question);
                    if (Int16.Parse(listimgindex[isaindex]) == 1)
                    {
                        if (questionimg[imgCounter].Length > 0)
                        {
                            var filePath = Path.Combine(uploadPath, question.AnswerId + ".jpg");
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await questionimg[imgCounter].CopyToAsync(fileStream);
                            }
                        }
                        imgCounter++;
                    }
                    isaindex++;
                }
                var result = await _quizRepository.GetAllQuizzesAsync();
                return View("Quizzes", result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                return View("MyQuizzes");
            }
        }
    }
}
