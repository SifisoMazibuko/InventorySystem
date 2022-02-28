using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationCore.Entities;
using Infrastructure.Data;
using ApplicationCore.Exceptions;

namespace InventorySystem.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;
        public const string SessionKeyId = "_Id";
        public const string SessionKeyName = "_Name";
        public const string SessionKeyEmailAddress = "_EmailAddress";

        public LoginModel(Infrastructure.Data.InventoryDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public User UserIdentity { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            try
            {
                if (IsValid(UserIdentity.Email))
                {
                    HttpContext.Session.SetString(SessionKeyName, UserIdentity.Name);
                    HttpContext.Session.SetString(SessionKeyEmailAddress, UserIdentity.Email);
                    var customer = _context.User
                           .Where(c => c.Email == UserIdentity.Email).ToList();
                            if (customer.Count > 0)
                                return RedirectToPage("/Products/Index");
                            else
                                return RedirectToPage("/Users/Login");
                        
                        

                       // return RedirectToPage("/Products/Index");
                }
                else
                {
                    Message = $"User {UserIdentity.Name} doesn't exist. Please Try again.";
                    return RedirectToPage("/Users/Login");
                }
            }
            catch (UserException ex)
            {
                throw new UserException("Error: Please try again" + ex.Message.ToString());
            }
        }

        public async Task<User> GetCurrentUser(string email)
        {
            try
            {
                    return await _context.User.FindAsync(email);
            }
            catch (UserException ex)
            {
                throw new UserException(ex.Message);
            }
        }

        public bool IsValid(string email)
        {
            var user = (from u in _context.User
                            where u.Email == email
                            select u).ToList();
            if (user.Count > 0)
                return true;
            else
                return false;
        }
    }
}
