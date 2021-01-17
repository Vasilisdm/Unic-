using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Unic.Data;
using Unic.Models;

namespace Unic.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly Unic.Data.SchoolContext _context;

        public IndexModel(Unic.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Instructor> Instructor { get;set; }

        public async Task OnGetAsync()
        {
            Instructor = await _context.Instructors.ToListAsync();
        }
    }
}
