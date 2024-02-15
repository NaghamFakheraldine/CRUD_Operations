using CRUD_Project.Data;
using CRUD_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CRUD_Project.Pages.Login
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        public BindingModel bindingModel { get; set; } = new BindingModel();
        public int userID;

        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid Model");
                return Page();
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.email == bindingModel.Email);

            if (existingUser == null || existingUser.first_name != bindingModel.FirstName)
            {
                ModelState.AddModelError(string.Empty, "User doesn't exist or credentials are incorrect. Please check your information.");
                return Page();
            }

            userID = existingUser.Id;
            return RedirectToPage("/Operations/Index", new { userID });
        }
    }
}
