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
    public class DeleteModel : PageModel
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;
        private bool _disposed = false;

        public void Dispose()
        {
            Cleanup(false);
            
        }
        public DeleteModel(Infrastructure.Data.InventoryDbContext context)
        {
            _context = context;
            _disposed = true;
        }

        [BindProperty]
        public User UserIdentity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserIdentity = await _context.User.FirstOrDefaultAsync(m => m.Id == id);

            if (UserIdentity == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserIdentity = await _context.User.FindAsync(id);

            if (User != null)
            {
                _context.User.Remove(UserIdentity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Disposing unmanaged reources
        /// </summary>
        /// <param name="disposing"></param>
        private void Cleanup(bool disposing)
        {
            if (this._disposed)
                return;
            if (!disposing) { }

            //Dispose Unmanaged resources
            _disposed = true;
        }

        //finalizer to ensure resources are automatically cleaned up  
        ~DeleteModel()
        {
            Cleanup(true);
        }
    }
}
