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

namespace InventorySystem.Pages.Products
{
    public class CreateModel : PageModel, IDisposable
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;
        private bool _disposed = false;

        public CreateModel(Infrastructure.Data.InventoryDbContext context)
        {
            _context = context;
            _disposed = true;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _context.User.Where(x => x.Email == HttpContext.Session.GetString("_EmailAddress")).FirstOrDefault();

            if (user != null)
            {
                Product.UserId = user.Id;

                _context.Product.Add(Product);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("User doesn't exist");
            }

            return RedirectToPage("./Index");
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
        ~CreateModel()
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
