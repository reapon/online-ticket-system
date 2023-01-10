using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTicketSystem.Data;
using OnlineTicketSystem.Models;
using OnlineTicketSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext context;
        public TicketController(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IActionResult> TicketIndex(string user)
        {            
            ViewBag.User = user;          
            var result = await context.Tickets.Include(x => x.TicketMessages).Where(x => x.CreatedBy == user).ToListAsync();
            ViewBag.Messages = result;
            return View(new CreateTicket());
        }

       

        //public IActionResult Index(string user)
        //{
        //    ViewBag.User = HttpContext.User.Identity.Name;

        //    var allTicket = context.Tickets.Where(x => x.CreatedBy == user).ToList();
        //    ViewBag.AllTicket = allTicket;

        //    var result = context.Tickets.Include(x => x.TicketMessages).Where(x => x.CreatedBy == user).ToList();

        //    ViewBag.TicketList = result;
        //    return View(result);
        //}

        //public IActionResult TicketCreate()
        //{
        //    return View("SingleMessage", new CreateTicket());
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TicketCreate(CreateTicket model)
        {
            if (ModelState.IsValid)
            {
                if (model.TicketType=="newTicket")
                {
                    //create ticket
                    Ticket ticket = new Ticket();
                    ticket.CreatedTime = DateTime.Now;
                    ticket.CreatedBy = HttpContext.User.Identity.Name;
                    context.Add(ticket);
                    await context.SaveChangesAsync();
                    //create message
                    TicketMessage message = new TicketMessage();
                    message.MessageBody = model.MessageBody;
                    message.MessageTime = DateTime.Now;
                    message.UserName = HttpContext.User.Identity.Name;
                    message.TicketID = ticket.TicketID;

                    context.Add(message);
                    await context.SaveChangesAsync();
                    return RedirectToAction("TicketIndex", new { user = ticket.CreatedBy });
                }
                else
                {
                    var user = HttpContext.User.Identity.Name;
                    ViewBag.User = user;
                    ViewBag.TicketID = model.TicketID;
                   

                    var result = await context.Tickets.Include(x => x.TicketMessages).Where(x => x.CreatedBy == user).ToListAsync();

                    ViewBag.Messages = result;


                    return View("SingleMessage", new CreateTicket());
                }
                
                
            }
            return View(model);

        }

        //public async Task<IActionResult> SingleMessage(int ticketId)
        //{
        //    var user = HttpContext.User.Identity.Name;
        //    ViewBag.User = user;
        //    ViewBag.TicketID = ticketId;

        //    var result =await context.Tickets.Include(x=>x.TicketMessages).FirstOrDefaultAsync(x=>x.TicketID== ticketId && x.CreatedBy==user);

        //    ViewBag.Messages = result;


        //    return View(result);
        //}


        public async Task<IActionResult> SingleMessage(string userName)
        {
            var user = HttpContext.User.Identity.Name;
            ViewBag.User = user;
            ViewBag.UserName = userName;
            //var tickId = Convert.ToInt32(ticketId);

            var result = await context.Tickets.Include(x => x.TicketMessages).Where(x=>x.CreatedBy==userName).ToListAsync();

            ViewBag.Messages = result;

            //var result = await context.TicketMessages.Where(x => x.TicketID == tickId && x.UserName == user).ToListAsync();
            //ViewBag.Messages = result;


            return View(result);
        }


        [HttpPost]
        public JsonResult MessageSend(int ticketId, string message)
        {
            if (ModelState.IsValid)
            {
                var model = new TicketMessage
                {
                    UserName = HttpContext.User.Identity.Name,
                    MessageTime = DateTime.Now,
                    MessageBody = message,
                    TicketID = ticketId
                };
                context.TicketMessages.Add(model);
                context.SaveChanges();
            }

            return Json(new { res = true });


        }

        public async Task<IActionResult> AllTicket()
        {
           
            
            var ticket = await context.Tickets.Distinct().ToListAsync();
            return View(ticket);
        }

        public async Task<IActionResult> GiveSupport(string user)
        {
            var support = HttpContext.User.Identity.Name;
            ViewBag.Support = support;
            
            var result = await context.Tickets.Include(x => x.TicketMessages).Where(x => x.CreatedBy == user).ToListAsync();
           // ViewBag.Messages = result;            
            return View(result);
        }


     


        [HttpPost]
        public JsonResult SupportMessage(int ticketId, string message)
        {
            if (ModelState.IsValid)
            {
                var model = new TicketMessage
                {
                    UserName = HttpContext.User.Identity.Name,
                    MessageTime = DateTime.Now,
                    MessageBody = message,
                    TicketID = ticketId
                };


                context.TicketMessages.Add(model);
                context.SaveChanges();
            }

            return Json(new { res = true });


        }

      
    }
}
