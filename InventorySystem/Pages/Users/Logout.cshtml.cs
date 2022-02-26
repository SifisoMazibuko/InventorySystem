using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
using Infrastructure.Data;

namespace InventorySystem.Pages.Users
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGetAsync()
        {
             HttpContext.Session.Remove("_EmailAddress");
            return RedirectToPage("/Users/Login");
        }
    }
}
