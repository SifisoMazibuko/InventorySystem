#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationCore.Entities;
using Infrastructure.Data;
using InventorySystem.Models;

namespace InventorySystem.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;
        public CreateModel(Infrastructure.Data.InventoryDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [TempData]
        public string loggedInUser { get; set; }

        [BindProperty]
        public User UserIdentity { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.User.Add(UserIdentity);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public User GetCurrentUser(string email)
        {
            return _context.User.Find(email);
        }

        public bool IsValid(string email, string name)
        {
            var user = (from c in _context.User
                            where   c.Email == email
                            select c).FirstOrDefault();
           // return user.Any();
            if (user !=  null) 
                return false;
            else
                return true;
        }
    }
}
