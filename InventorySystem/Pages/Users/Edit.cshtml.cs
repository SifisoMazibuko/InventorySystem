#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
using Infrastructure.Data;

namespace InventorySystem.Pages.Users
{
    public class EditModel : PageModel, IDisposable
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;
        private bool _disposed = false;


        public EditModel(Infrastructure.Data.InventoryDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserIdentity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(UserIdentity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        /// <summary>
        /// Disposing unmanaged reources
        /// </summary>
        /// <param name="disposing"></param>
        private void Cleanup(bool disposing)
        {
            if (_disposed)
                _context.Dispose();
            if (!disposing) { }

            //Dispose Unmanaged resources
            _disposed = true;
        }

        //finalizer to ensure resources are automatically cleaned up  
        ~EditModel()
        {
            Cleanup(true);
        }

        public void Dispose()
        {
            Cleanup(false);
            GC.SuppressFinalize(this);
        }
    }
}
