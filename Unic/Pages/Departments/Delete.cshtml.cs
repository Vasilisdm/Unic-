using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Unic.Data;
using Unic.Models;

namespace Unic.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly SchoolContext _context;

        public DeleteModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Department Department { get; set; }
        public string ConcurrencyErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department = await _context.Departments
                .Include(d => d.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (concurrencyError.GetValueOrDefault())
            {
                ConcurrencyErrorMessage = "The record you attempted to delete "
                  + "was modified by another user after you selected delete. "
                  + "The delete operation was canceled and the current values in the "
                  + "database have been displayed. If you still want to delete this "
                  + "record, click the Delete button again.";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                if (await _context.Departments.AnyAsync(d => d.DepartmentID == id))
                {
                    _context.Departments.Remove(Department);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return RedirectToPage("./Delete", new { concurrencyError = true, id });
            }
        }
    }
}