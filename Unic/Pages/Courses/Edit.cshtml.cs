using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unic.Data;
using Unic.Models;

namespace Unic.Pages.Courses
{
    public class EditModel : DepartmentNamePageModel
    {
        private readonly SchoolContext _context;

        public EditModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Courses
                .Include(c => c.Department).FirstOrDefaultAsync(m => m.CourseID == id);

            if (Course == null)
            {
                return NotFound();
            }

            PopulateDepartmentsDropDownList(_context, Course.DepartmentID);
            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseToUpdate = await _context.Courses.FindAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Course>(
                courseToUpdate,
                "course",
                c => c.Credits,
                c => c.DepartmentID,
                c => c.Title))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("./Index");
            }

            PopulateDepartmentsDropDownList(_context, courseToUpdate.CourseID);
            return Page();
    }
}
