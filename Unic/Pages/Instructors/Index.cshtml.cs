using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Unic.Data;
using Unic.Models;
using Unic.Models.SchoolViewModels;

namespace Unic.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly Unic.Data.SchoolContext _context;

        public IndexModel(Unic.Data.SchoolContext context)
        {
            _context = context;
        }

        public InstructorIndexData InstructorData { get;set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            InstructorData = new InstructorIndexData
            {
                Instructors = await _context.Instructors
                                            .Include(i => i.OfficeAssignment)
                                            .Include(i => i.CourseAssignments)
                                                .ThenInclude(ca => ca.Course)
                                                    .ThenInclude(c => c.Department)
                                            .Include(i => i.CourseAssignments)
                                                .ThenInclude(ca => ca.Course)
                                                    .ThenInclude(c => c.Enrollments)
                                                        .ThenInclude(e => e.Student)
                                            .AsNoTracking()
                                            .OrderBy(i => i.LastName)
                                            .ToListAsync()
            };

            if (id != null)
            {
                InstructorID = id.Value;
                var instructor = InstructorData.Instructors.Where(i => i.ID == id.Value).Single();
                InstructorData.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null)
            {
                courseID = courseID.Value;
                var selectedCourse = InstructorData.Courses.Where(c => c.CourseID == courseID.Value).Single();
                InstructorData.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}
