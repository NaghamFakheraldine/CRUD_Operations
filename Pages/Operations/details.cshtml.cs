using CRUD_Project.Data;
using CRUD_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Project.Pages.Operations
{
    [BindProperties]
    public class detailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public int userID;
        public string role = "User";
        public detailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? userID)
        {
            if(id==null || userID == null)
            {
                return RedirectToPage("/Error");
            }
            
            this.userID = userID.Value;
            var mainUser = await _context.Users.FirstOrDefaultAsync(m => m.Id == userID);
            if (mainUser != null)
            {
                role = mainUser.role.ToString();
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
    }
}
