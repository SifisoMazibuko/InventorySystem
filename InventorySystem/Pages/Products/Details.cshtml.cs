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

namespace InventorySystem.Pages.Products
{
    public class DetailsModel : PageModel, IDisposable
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;
        private bool _disposed = false;

        public DetailsModel(Infrastructure.Data.InventoryDbContext context)
        {
            _context = context;
            _disposed = true;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
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
        ~DetailsModel()
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
