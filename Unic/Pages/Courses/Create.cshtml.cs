using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unic.Data;
using Unic.Models;

namespace Unic.Pages.Courses
{
    public class CreateModel : DepartmentNamePageModel
    {
        private readonly SchoolContext _context;

        public CreateModel(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateDepartmentsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyCourse = new Course();

            if (await TryUpdateModelAsync<Course>(emptyCourse,
                "course",
                s => s.CourseID,
                s => s.DepartmentID,
                s => s.Title,
                s => s.Credits))
            {
                _context.Add(emptyCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction("./Index");
            }

            PopulateDepartmentsDropDownList(_context, emptyCourse.DepartmentID);
            return Page();
        }
    }
}
