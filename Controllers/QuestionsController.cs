#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutOfBound.Data;
using OutOfBound.Models;

namespace OutOfBound
{
    public class QuestionsController : Controller
    {
        private readonly OutOfBoundContext _context;

        public QuestionsController(OutOfBoundContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index(string searchString)
        {
            var questions = from m in _context.QuestionModel
                select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                questions = questions.Where(s => s.Title!.Contains(searchString));
            }

            return View(await questions.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionModel = await _context.QuestionModel
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (questionModel == null)
            {
                return NotFound();
            }
            
            await _context.AnswerModel
                .Where(m => id == m.QuestionModelID)
                .ToListAsync();

            return View(questionModel);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Text")] QuestionModel questionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionModel);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionModel = await _context.QuestionModel.FindAsync(id);
            if (questionModel == null)
            {
                return NotFound();
            }
            return View(questionModel);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Text")] QuestionModel questionModel)
        {
            if (id != questionModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionModelExists(questionModel.ID))
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
            return View(questionModel);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionModel = await _context.QuestionModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionModel == null)
            {
                return NotFound();
            }

            return View(questionModel);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionModel = await _context.QuestionModel.FindAsync(id);
            _context.QuestionModel.Remove(questionModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionModelExists(int id)
        {
            return _context.QuestionModel.Any(e => e.ID == id);
        }

        public async Task<IActionResult> PostAnswer(int id)
        {

            if (!QuestionModelExists(id))
            {
                return NotFound();
            }


            var answerModel = new AnswerModel();
            answerModel.QuestionModelID = id;

            return View(answerModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAnswer([Bind("Text, QuestionModelID")] AnswerModel answerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = answerModel.QuestionModelID });
            }
            
            return View(answerModel);
        }
    }
}
