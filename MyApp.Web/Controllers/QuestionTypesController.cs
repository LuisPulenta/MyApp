using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Web.Data;
using MyApp.Web.Data.Entities;
using MyApp.Web.Models;

namespace MyApp.Web.Controllers
{
    public class QuestionTypesController : Controller
    {
        private readonly DataContext _dataContext;

        public QuestionTypesController(DataContext context)
        {
            _dataContext = context;
        }

        // GET: QuestionTypes
        public IActionResult Index()
        {
            return View(_dataContext.QuestionTypes
                .Include(q => q.Questions).OrderBy(p => p.Name));
        }






        // GET: QuestionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionType = await _dataContext.QuestionTypes
                .Include(o => o.Questions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionType == null)
            {
                return NotFound();
            }

            return View(questionType);
        }

        // GET: QuestionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] QuestionType questionType)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(questionType);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionType);
        }

        // GET: QuestionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionType = await _dataContext.QuestionTypes.FindAsync(id);
            if (questionType == null)
            {
                return NotFound();
            }
            return View(questionType);
        }

        // POST: QuestionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] QuestionType questionType)
        {
            if (id != questionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(questionType);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionTypeExists(questionType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(questionType);
        }

        // GET: QuestionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionType = await _dataContext.QuestionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionType == null)
            {
                return NotFound();
            }

            return View(questionType);
        }

        // POST: QuestionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionType = await _dataContext.QuestionTypes.FindAsync(id);
            _dataContext.QuestionTypes.Remove(questionType);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionTypeExists(int id)
        {
            return _dataContext.QuestionTypes.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddQuestion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionType = await _dataContext.QuestionTypes.FindAsync(id.Value);
            if (questionType == null)
            {
                return NotFound();
            }

            var model = new QuestionViewModel
            {
                QuestionTypeId = questionType.Id,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestion(QuestionViewModel model)
        {

            

            if (ModelState.IsValid)
            {
                var questionType = await _dataContext.QuestionTypes.FindAsync(model.QuestionTypeId);

                int? maxid = _dataContext.Questions.Where(x=>  x.QuestionType.Id == questionType.Id).Select(x => x.IdSubject).Max();
                int? maxid2 = 0;

                 if (maxid == null)
                    {
                    maxid2 = 0;
                    }
                 else
                    {
                    maxid2 = maxid;
                    }


                var qst = new Question
                    {


                        IdSubject = maxid2+1,
                        QuestionType = questionType,
                        Subject = model.Subject,
                    };
                _dataContext.Questions.Add(qst);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.QuestionTypeId}");
            }
            
            return View(model);
        }

        public async Task<IActionResult> EditQuestion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var question = await _dataContext.Questions
                .Include (p=>p.QuestionType)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (question == null)
            {
                return NotFound();
            };
            var qst = new QuestionViewModel
            {
                Id=question.Id,
                IdSubject = question.IdSubject,
                QuestionTypeId = question.QuestionType.Id,
                Subject = question.Subject,
            };
            return View(qst);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestion(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var qst = new Question
                {
                    Id=model.Id,
                    IdSubject = model.IdSubject,
                    Subject = model.Subject,
                };
                _dataContext.Questions.Update(qst);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.QuestionTypeId}");
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteQuestion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var question = await _dataContext.Questions
               .Include(p => p.QuestionType)
               .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (question == null)
            {
                return NotFound();
            };
            var qst = new QuestionViewModel
            {
                Id = question.Id,
                IdSubject = question.IdSubject,
                QuestionTypeId = question.QuestionType.Id,
                Subject = question.Subject,
            };
            return View(qst);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(QuestionViewModel model)
        {
            var state = await _dataContext.Questions.FindAsync(model.Id);
            _dataContext.Questions.Remove(state);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{model.QuestionTypeId}");
            
        }
    }
}
