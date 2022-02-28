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
    public class IndexModel : PageModel, IDisposable
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;
        private bool _disposed = false;

        public IndexModel(Infrastructure.Data.InventoryDbContext context)
        {
            _context = context;
            _disposed = true;
        }


        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            var user = _context.User.Where(x => x.Email == HttpContext.Session.GetString("_EmailAddress")).FirstOrDefault();
            //View products by user who added them
            Product = await _context.Product.Where(x => x.UserId == user.Id).ToListAsync();
            
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
        ~IndexModel()
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
