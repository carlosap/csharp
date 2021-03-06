﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using WebDbServer.TraceInfo;
using WebDbServer.Model;
namespace WebDbServer.Controllers
{
    /// api/updateuserclient?firstname=carlos&lastname=perez&email=cperez@gmail.com&tel=1234&emailenable=true&textenable=false
    [Route("api/[controller]")]
    public class UpdateUserClientController : Controller
    {
        private UserClientContext _context;
        public UpdateUserClientController(UserClientContext context) { _context = context; }
        public async Task<object> Get([FromQuery]string firstname, string lastname, string email, string tel, bool emailenable = true, bool textenable = true)
        {           
            if (firstname == null) firstname = string.Empty;
            return await Task.Run(async () =>
            {
                try
                {
                    object results;
                    await AddCorOptions();
                    using (var context = _context)
                    {
                        var user = context.Clients.FirstOrDefault(x => x.Email.Equals(email));
                        if (user != null)
                        {
                            user.Email = email;
                            user.FirstName = firstname;
                            user.LastName = lastname;
                            user.DateCreated = DateTime.Now;
                            user.Phone = tel;
                            user.TextEnable = textenable;
                            user.EmailEnable = emailenable;
                            context.Clients.Update(user);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            //bad idea!!! Fix me
                            user = context.Clients.FirstOrDefault(x => x.Phone.Equals(tel));
                            if (user != null)
                            {
                                user.Email = email;
                                user.FirstName = firstname;
                                user.LastName = lastname;
                                user.DateCreated = DateTime.Now;
                                user.Phone = tel;
                                user.TextEnable = textenable;
                                user.EmailEnable = emailenable;
                                context.Clients.Update(user);
                                await context.SaveChangesAsync();
                            }
                        }
                        results = context.Clients.OrderBy(x => x.DateCreated).ToList().Take(20);
                    }
                    return results;
                }
                catch (Exception ex)
                {
                    await Tracer.Exception("UpdateUserClientController:Get", ex);
                    return null;
                }
            });
        }

        private async Task AddCorOptions()
        {
            await Task.Run(() =>
            {
                Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                Response.Headers.Add("Access-Control-Allow-Headers",
                    new[] { "Origin, X-Requested-With, Content-Type, Accept" });
            });
        }
    }
}
