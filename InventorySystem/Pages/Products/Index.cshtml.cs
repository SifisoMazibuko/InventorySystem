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
    public class IndexModel : PageModel
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;
        private bool _disposed = false;

        public void Dispose()
        {
            Cleanup(false);
            GC.SuppressFinalize(this);
        }
        public IndexModel(Infrastructure.Data.InventoryDbContext context)
        {
            _context = context;
            _disposed = true;
        }


        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
        }

        /// <summary>
        /// Disposing unmanaged reources
        /// </summary>
        /// <param name="calledFromFinalizer"></param>
        private void Cleanup(bool disposing)
        {
            if (this._disposed)
                return;
            if (!disposing) { }

            //Dispose Unmanaged resources
            _disposed = true;
        }

        //finalizer to ensure resources are automatically cleaned up  
        ~IndexModel()
        {
            Cleanup(true);
        }

    }
}
