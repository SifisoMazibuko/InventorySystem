#nullable disable
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
    public class IndexModel : PageModel
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;

        public IndexModel(Infrastructure.Data.InventoryDbContext context)
        {
            _context = context;
        }

        public IList<User> UserIdentity { get;set; }

        public async Task OnGetAsync()
        {
            UserIdentity = await _context.User.ToListAsync();
        }
    }
}
