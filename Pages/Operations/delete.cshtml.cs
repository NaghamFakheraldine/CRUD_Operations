using CRUD_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Project.Pages.Operations
{
    [BindProperties]
    public class deleteModel : PageModel
    {
        private readonly CRUD_Project.Data.ApplicationDbContext _context;
        public string role = "User";
        public int userID;

        public deleteModel(CRUD_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? userID)
        {
            if(userID != null)
            {
                this.userID = userID.Value;
                // Store userID in TempData
                TempData["UserID"] = userID.Value;
            } 

            if(userID == null || id == null) {
                return RedirectToPage("/Error");
            }

            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id.Value);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                User = user;
                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
            }

            // Get userID from TempData
            int? userID = TempData["UserID"] as int?;
            return RedirectToPage("Index", new { userID });
        }
    }
}
