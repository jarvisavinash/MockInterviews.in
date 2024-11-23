using Microsoft.AspNetCore.Mvc;
using MockInterviews.Data;
using MockInterviews.Models;
using System.Linq;

namespace MockInterviews.Controllers
{
    public class InterviewController : Controller
    {
        private readonly AppDbContext _context;

        public InterviewController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Create Request
        public IActionResult CreateRequest()
        {
            ViewBag.Topics = _context.Topics.ToList(); // Fetch topics dynamically
            return View();
        }

        // POST: Create Request
        [HttpPost]
        public IActionResult CreateRequest(InterviewRequest request)
        {
            if (ModelState.IsValid)
            {
                _context.InterviewRequests.Add(request);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Topics = _context.Topics.ToList(); // Reload topics for the view
            return View(request);
        }
    }
}
