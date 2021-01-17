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

namespace Unic.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly Unic.Data.SchoolContext _context;

        public IndexModel(Unic.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<CourseViewModel> Courses { get;set; }

        public async Task OnGetAsync()
        {

            Courses = await _context.Courses
                                    .Select(c => new CourseViewModel
                                    {
                                        CourseID = c.CourseID,
                                        Title = c.Title,
                                        Credits = c.Credits,
                                        DepartmentName = c.Department.Name
                                    })
                                    .ToListAsync();
        }
    }
}
