using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unic.Data;
using Unic.Models;

namespace Unic.Pages.Instructors
{
    public class EditModel : InstructorCoursesPageModel
    {
        private readonly SchoolContext _context;

        public EditModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Instructor Instructor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Instructor = await _context.Instructors
                                       .Include(i => i.OfficeAssignment)
                                       .Include(i => i.CourseAssignments)
                                            .ThenInclude(ca => ca.Course)
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(m => m.ID == id);

            if (Instructor == null)
            {
                return NotFound();
            }

            PopulateAssignedCourseData(_context, Instructor);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructorToUpdate = await _context.Instructors
                                                   .Include(i => i.OfficeAssignment)
                                                   .Include(i => i.CourseAssignments)
                                                        .ThenInclude(ca => ca.Course)
                                                   .FirstOrDefaultAsync(i => i.ID == id);

            if (instructorToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Instructor>(
                instructorToUpdate,
                "Instrcutor",
                i => i.FirstMidName,
                i => i.LastName,
                i => i.HireDate,
                i => i.OfficeAssignment))
            {
                if (string.IsNullOrEmpty(instructorToUpdate.OfficeAssignment?.Location))
                {
                    instructorToUpdate.OfficeAssignment = null;
                }

                UpdateInstructorCourses(_context, instructorToUpdate, selectedCourses);
                await _context.SaveChangesAsync();
                return Page();
            }

            UpdateInstructorCourses(_context, instructorToUpdate, selectedCourses);
            PopulateAssignedCourseData(_context, instructorToUpdate);
            return Page();
        }
    }
}
