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
using MimeKit;
using System.Net.Mail;
using Infrastructure.ResourceDisposal;

namespace InventorySystem.Pages.Users
{
    public class CreateModel : PageModel, IDisposable
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;
        public readonly EmailConfiguration _emailConfig;
        private readonly IConfiguration _configuration;
        private bool _disposed = false;

        public CreateModel(Infrastructure.Data.InventoryDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _disposed = true;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User UserIdentity { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            #region Send Email after registering (comment out cos of google Auth issue) 
            /*var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sifiso Mazibuko", "mazibujo19@gmail.com"));
            message.To.Add(new MailboxAddress("", UserIdentity.Email));
            message.Subject = UserIdentity.Name + " Registered!";

            message.Body = new TextPart
            {
                Text = string.Format("Hi " + UserIdentity.Name + ",\n" +
                               "\nWelcome to Inventory System." +
                               "\n"
                               + "You can perform CRUD operations on the Products."
                               + "\n\n" + "Thank you!"
                               + "\n\n" + "Kind Regards,"
                               + "\n Sifiso\n"
                       )


            };

            var username = _configuration.GetSection("EmailConfiguration").GetSection("Username").Value;
            var password = _configuration.GetSection("EmailConfiguration").GetSection("Password").Value;
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(username, password);

                    client.Send(message);
                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }

            }*/
            #endregion


            _context.User.Add(UserIdentity);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public User GetCurrentUser(string email)
        {
            return _context.User.Find(email);
        }

        public bool IsValid(string email)
        {
            var user = (from c in _context.User
                            where   c.Email == email
                            select c).FirstOrDefault();
            if (user !=  null) 
                return false;
            else
                return true;
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
