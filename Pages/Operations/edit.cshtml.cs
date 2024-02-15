using CRUD_Project.Data;
using CRUD_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Project.Pages.Operations
{
    [BindProperties]
    public class editModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public int userID;
        public User User { get; set; } = default!;

        public editModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> OnGetAsync(int? id, int? userID)
        {
            if(userID == null || id == null)
            {
                return RedirectToPage("/Error");
            }

            this.userID = userID.Value;

            // Store userID in TempData
            TempData["UserID"] = userID.Value;

            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id.Value);
            if (user == null)
            {
                return NotFound();
            }

            User = user;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Ensure the user with the given email exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.email == User.email);

            if (existingUser == null)
            {
                return NotFound();
            }

            // Check if email is unique before updating
            if (_context.Users.Any(u => u.email == User.email && u.Id != User.Id))
            {
                ModelState.AddModelError("User.email", "Email already exists. Choose a different email.");
                return Page();
            }

            // Update only specific fields
            existingUser.first_name = User.first_name;
            existingUser.last_name = User.last_name;
            existingUser.date_of_birth = User.date_of_birth;
            existingUser.gender = User.gender;
            existingUser.country = User.country;
            existingUser.phone_number = User.phone_number;
            existingUser.email = User.email;
            existingUser.role = User.role;

            // Check if ImageFile is provided
            if (User.ImageFile != null)
            {
                // Handle image upload logic here
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(User.ImageFile.FileName);
                string imageFullPath = Path.Combine(_environment.WebRootPath, "UserImages", newFileName);

                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    User.ImageFile.CopyTo(stream);
                }

                existingUser.ImageFileName = newFileName;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Get userID from TempData
            int? userID = TempData["UserID"] as int?;
            return RedirectToPage("Index", new { userID });
        }
        private bool UserExists(int id)
        {
            return _context.Users?.Any(e => e.Id == id) ?? false;
        }
    }
}
