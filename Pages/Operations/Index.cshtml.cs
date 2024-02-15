using CRUD_Project.Data;
using CRUD_Project.Models;
using CRUD_Project.Pages.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Project.Pages.Operations
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string username = "Index";
        public int userID;
        public string role = "User";
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<User> User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? userID)
        {

            if (_context.Users != null)
            {
                User = await _context.Users.ToListAsync();
                if (userID != null)
                {
                    var existingUser = await _context.Users.FirstOrDefaultAsync(m => m.Id == userID);
                    if (existingUser != null)
                    {
                        username = "Hello " + existingUser.first_name + "!";
                        role = existingUser.role.ToString();
                        this.userID = existingUser.Id;
                        return Page();
                    }
                } else {
                    return RedirectToPage("/Error");
                }
            } else
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }
    }
}
