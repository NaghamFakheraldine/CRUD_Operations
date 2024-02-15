using CRUD_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_Project.Pages.Operations
{
    [BindProperties]
    public class createModel : PageModel
    {
        private readonly CRUD_Project.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;
        public User User { get; set; } = default!;
        public int userID;

        public createModel(CRUD_Project.Data.ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }

        public IActionResult OnGet(int? userID)
        {
            if(userID != null)
            {
                this.userID = userID.Value;

                // Store userID in TempData
                TempData["UserID"] = userID.Value;
                return Page();
            }
            return RedirectToPage("/Error");
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (User.ImageFile == null)
            {
                ModelState.AddModelError("User.ImageFile", "The image file is required");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the email already exists
            if (_context.Users.Any(u => u.email == User.email))
            {
                ModelState.AddModelError("User.email", "Email already exists. Choose a different email.");
                return Page();
            }

            //save image file
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(User.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/UserImages/" + newFileName;

            using (var stream = System.IO.File.Create(imageFullPath))
            {
                User.ImageFile.CopyTo(stream);
            }

            User user = new User()
            {
                first_name = User.first_name,
                last_name = User.last_name,
                email = User.email,
                phone_number = User.phone_number,
                country = User.country,
                gender = User.gender,
                ImageFile = User.ImageFile,
                ImageFileName = newFileName,
                date_of_birth = User.date_of_birth,
                role = User.role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            ModelState.Clear();

            // Get userID from TempData
            int? userID = TempData["UserID"] as int?;
            return RedirectToPage("Index", new { userID });
        }
    }
}
