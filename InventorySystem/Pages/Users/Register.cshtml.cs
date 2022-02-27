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

namespace InventorySystem.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly Infrastructure.Data.InventoryDbContext _context;
        public readonly EmailConfiguration _emailConfig;
        private IConfiguration _configuration;
        
        public CreateModel(Infrastructure.Data.InventoryDbContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [TempData]
        public string loggedInUser { get; set; }

        [BindProperty]
        public User UserIdentity { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sifiso Mazibuko", "mazibujo19@gmail.com"));
            message.To.Add(new MailboxAddress("",UserIdentity.Email));
            message.Subject = UserIdentity.Name + " Registered!";

            message.Body = new TextPart
            {
                Text = string.Format("Hi " + UserIdentity.Name + ",\n" +
                               "\nWelcome to Inventory System." +
                               "\n"
                               + "You can perform CRUD operation on Products."
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
                    client.Authenticate(username,password);

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
               
            }

            _context.User.Add(UserIdentity);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public User GetCurrentUser(string email)
        {
            return _context.User.Find(email);
        }

        public bool IsValid(string email, string name)
        {
            var user = (from c in _context.User
                            where   c.Email == email
                            select c).FirstOrDefault();
           // return user.Any();
            if (user !=  null) 
                return false;
            else
                return true;
        }
    }
}
