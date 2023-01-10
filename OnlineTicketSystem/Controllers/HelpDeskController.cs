using Google.DataTable.Net.Wrapper;
using Google.DataTable.Net.Wrapper.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineTicketSystem.Data;
using OnlineTicketSystem.Models;
using OnlineTicketSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Controllers
{
    [Authorize]
    public class HelpDeskController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly RoleManager<IdentityRole> roleManage;

        public HelpDeskController(ApplicationDbContext _context, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager, IConfiguration config, RoleManager<IdentityRole> roleManage)
        {
            context = _context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.config = config;
            this.roleManage = roleManage;
        }

        

        public async Task<IActionResult> Index()
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var group = await context.Groups.Where(x=>x.ComID==comid).ToListAsync();
            return View(group);
        }

        public async Task<IActionResult> TicketCreate(int id)
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var result = await context.Groups.FindAsync(id);
            ViewBag.Group = result.GroupName;
            HttpContext.Session.SetInt32("GroupID", id);
            

            var user = HttpContext.User.Identity.Name;
            ViewBag.User = user;
            var tickets = await context.Tickets.Include(x => x.TicketMessages).Include(x => x.Status).Include(x => x.Group).Where(x => x.CreatedBy == user && x.ComID==comid).ToListAsync();
            ViewBag.Messages = tickets;

            return View(new CreateTicket());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TicketCreate(CreateTicket model)
        {
            var comid = HttpContext.Request.Cookies["ComID"];
            var groupid = HttpContext.Session.GetInt32("GroupID");

            if (ModelState.IsValid)
            {
                if (model.TicketType == "newTicket")
                {

                    //create ticket
                    Ticket ticket = new Ticket();
                    ticket.CreatedTime = DateTime.Now;
                    ticket.CreatedBy = HttpContext.User.Identity.Name;
                    ticket.GroupID = groupid;
                    ticket.StatusID = 1;
                    ticket.ComID = comid;
                    context.Add(ticket);
                    await context.SaveChangesAsync();


                    string uniqueFileName = null;
                    if (model.Photo != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }

                    //create message
                    TicketMessage message = new TicketMessage();
                    message.MessageBody = model.MessageBody;
                    message.MessageTime = DateTime.Now;
                    message.UserName = HttpContext.User.Identity.Name;
                    message.TicketID = ticket.TicketID;
                    message.PhotoPath = uniqueFileName;
                    message.ComID = comid;

                    context.Add(message);
                    await context.SaveChangesAsync();


                    //Send Mail
                    var userMail = await context.UserMails.FirstOrDefaultAsync(x => x.ClientUser == ticket.CreatedBy);
                    
                    var callBackUrl = Url.ActionLink("AllTicket", "HelpDesk");
                    if (userMail != null)
                    {
                        string mailList = userMail.TicketMailList;
                        string subject = $"A ticket has been created by : {ticket.CreatedBy} on - {ticket.CreatedTime}...";
                        string body = $"Issue is : {message.MessageBody} .. Click here to visit support center <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>Go to Support Center</a>";
                        SendEmail(mailList, subject, body);
                    }



                    return RedirectToAction("TicketCreate", new { id = groupid });
                }
            }
            return View(model);
        }


        [HttpPost]
        public JsonResult UserMessageSend(int ticketId, string message)
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            if (ModelState.IsValid)
            {
                var ticket = context.Tickets.Find(ticketId);

                if (ticket == null)
                {
                    return Json(NotFound());
                }

                context.Tickets.Attach(ticket);

                ticket.StatusID = 3;
                context.SaveChanges();

                var model = new TicketMessage
                {
                    UserName = HttpContext.User.Identity.Name,
                    MessageTime = DateTime.Now,
                    MessageBody = message,
                    TicketID = ticketId,
                    ComID=comid
                };


                context.TicketMessages.Add(model);
                context.SaveChanges();

                var userMail = context.UserMails.FirstOrDefault(x => x.ClientUser == ticket.CreatedBy && x.ComID==comid);

                if (userMail != null)
                {
                    string mailList = userMail.TicketMailList;
                    string subject = $"Ticket Re-Opened By : {ticket.CreatedBy} which was closed in {ticket.ClosedTime}...";
                    string body = $"Issue : {message}";
                    SendEmail(mailList, subject, body);
                }

            }

            return Json(new { res = true });


        }

        [Authorize]
        public async Task<IActionResult> AllTicket()
        {
            var user = HttpContext.User.Identity.Name;
            var comid = HttpContext.Request.Cookies["ComID"];


            var ticket = await (from p in context.Tickets
                                join e in context.Groups
                                on p.GroupID equals e.GroupID
                                join u in context.GroupUsers
                                on e.GroupID equals u.GroupID
                                where u.UserName == user where p.ComID==comid
                                select new AllTicketViewModel
                                {
                                    TicketID = p.TicketID,
                                    CreatedBy = p.CreatedBy,
                                    CreatedTime = p.CreatedTime,
                                    AssignedTo = p.AssignedTo,
                                    StatusID = p.StatusID,
                                    SupportGivenBy = p.SupportGivenBy,
                                    GroupName = e.GroupName,
                                    GroupID= e.GroupID
                                }).ToListAsync();
            return View(ticket);


        }

        public async Task<IActionResult> GiveSupport(string user)
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var support = HttpContext.User.Identity.Name;
            ViewBag.Support = support;

            ViewData["CategoryID"] = new SelectList(context.Categories, "CategoryName", "CategoryName");

            var result = await context.Tickets.Include(x => x.TicketMessages).Include(x => x.Status).Include(x => x.Group).Where(x => x.CreatedBy == user && x.ComID==comid).ToListAsync();
            return View(result);
        }



        [HttpPost]
        public JsonResult SupportMessageSend(int ticketId, string message, string[] categoryType)
        {
            var support = HttpContext.User.Identity.Name;
            var comid = HttpContext.Request.Cookies["ComID"];

            if (ModelState.IsValid)
            {
                var ticket = context.Tickets.Find(ticketId);

                if (ticket == null)
                {
                    return Json(NotFound());
                }

                context.Tickets.Attach(ticket);

                string supportType = String.Join(",", categoryType);

                ticket.StatusID = 2;
                ticket.SupportType = supportType;
                ticket.SupportGivenBy = support;
                ticket.ClosedTime = DateTime.Now;
                context.SaveChanges();

                //create a new message under specific ticket
                var model = new TicketMessage
                {
                    UserName = support,
                    MessageTime = DateTime.Now,
                    MessageBody = message,
                    TicketID = ticketId,
                    ComID=comid
                };


                context.TicketMessages.Add(model);
                context.SaveChanges();



                var supportMail = context.UserMails.FirstOrDefault(x => x.ClientUser == ticket.CreatedBy && x.ComID==comid);

                if (supportMail != null)
                {
                    string mailList = supportMail.SupportMailList;
                    string subject = $"Ticket Solved By : {ticket.SupportGivenBy} on - {ticket.ClosedTime}, which was Opened By {ticket.CreatedBy}...";
                    string body = $"Response Message Was : {message}";
                    SendEmail(mailList, subject, body);
                }



                foreach (var c in categoryType)
                {
                    if (!context.Categories.Any(o => o.CategoryName == c))
                    {
                        var cate = new Category()
                        {
                            CategoryName = c,
                            ComID= comid
                        };

                        context.Add(cate);
                        context.SaveChanges();
                    }

                }


            }

            return Json(new { res = true });


        }


        [HttpPost]
        public JsonResult MakeTicketPending(int ticketId)
        {
            if (ModelState.IsValid)
            {
                var ticket = context.Tickets.Find(ticketId);
                if (ticket == null)
                {
                    return Json(NotFound());
                }
                context.Tickets.Attach(ticket);
                ticket.StatusID = 4;
                context.SaveChanges();
            }
            return Json(new { res = true });
        }




        public async Task<IActionResult> AdminTicketPage()
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var tickets = await context.Tickets.Include(x => x.Status).Include(x => x.Group).Where(x=>x.ComID==comid).ToListAsync();
            return View(tickets);
        }


        public IActionResult AssignTicketToAgentByAdmin(int ticketid, int id)
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            ViewBag.TicketId = ticketid;
            var result = context.GroupUsers.Where(x => x.GroupID == id).ToList();
            ViewData["User"] = new SelectList(context.GroupUsers.Where(x => x.GroupID == id && x.ComID==comid), "UserName", "UserName");
            return View(result);
        }

        [HttpPost]
        public JsonResult AssignAgentByAdmin(int ticketId, string user)
        {
            if (ModelState.IsValid)
            {
                var ticket = context.Tickets.Find(ticketId);
                if (ticket == null)
                {
                    return Json(NotFound());
                }

                context.Tickets.Attach(ticket);

                ticket.AssignedTo = user;
                context.SaveChanges();
            }

            return Json(new { res = true, redirectUrl = Url.Action("AllTicket", "HelpDesk"), isRedirect = true });

        }



        public IActionResult AssignTicketToAgent(int ticketid, int id)
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            ViewBag.TicketId = ticketid;
            var result = context.GroupUsers.Where(x => x.GroupID == id).ToList();
            ViewData["User"] = new SelectList(context.GroupUsers.Where(x => x.GroupID == id && x.ComID==comid), "UserName", "UserName");
            return View(result);
        }


        [HttpPost]
        public JsonResult AssignAgent(int ticketId, string user)
        {
            if (ModelState.IsValid)
            {
                var ticket = context.Tickets.Find(ticketId);
                if (ticket == null)
                {
                    return Json(NotFound());
                }

                context.Tickets.Attach(ticket);

                ticket.AssignedTo = user;
                context.SaveChanges();
            }

            return Json(new { res = true, redirectUrl = Url.Action("AdminTicketPage", "HelpDesk"), isRedirect = true });

        }



        public async Task<IActionResult> CategoryIndex()
        {
            var comid = HttpContext.Request.Cookies["ComID"];
            var category = await context.Categories.Where(x=>x.ComID== comid).ToListAsync();
            return View(category);
        }


        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            ViewBag.ComId = HttpContext.Request.Cookies["ComID"];
            if (id == 0)
            {
                return View(new Category());
            }
            else
            {
                var category = await context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View("AddOrEdit", category);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryID,CategoryName,ComID")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryID <= 0)
                {
                    context.Add(category);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(CategoryIndex));
                }
                else
                {
                    try
                    {

                        context.Update(category);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    return RedirectToAction(nameof(CategoryIndex));

                }
            }
            return View(category);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();

            }
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(CategoryIndex));
        }


        public async Task<IActionResult> GroupIndex()
        {
            var comid= HttpContext.Request.Cookies["ComID"];

            var groups = await context.Groups.Where(x=>x.ComID==comid).ToListAsync();

            return View(groups);
        }

        public async Task<IActionResult> UserMailList()
        {
            var comid = HttpContext.Request.Cookies["ComID"];
            var userMail = await context.UserMails.Where(x=>x.ComID==comid).ToListAsync();
            return View(userMail);
        }



        [HttpGet]
        public async Task<IActionResult> UserMailAddOrEdit(int id = 0)
        {
            //var user = await userManager.GetUsersInRoleAsync("User");
            var comid = HttpContext.Request.Cookies["ComID"];

            ViewBag.ComId = comid;

            var user = userManager.GetUsersInRoleAsync("User").Result.Where(x => x.ComID == comid);


            //var user = await userManager.Users.Where(x => x.ComID == comid).ToListAsync();

            ViewData["User"] = new SelectList(user, "UserName", "UserName");

            if (id == 0)
            {
                return View(new UserMail());
            }
            else
            {
                var userMail = await context.UserMails.FindAsync(id);
                if (userMail == null)
                {
                    return NotFound();
                }
                return View("UserMailAddOrEdit", userMail);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserMailAddOrEdit(UserMail userMail)
        {
            if (ModelState.IsValid)
            {
                if (userMail.ID <= 0)
                {
                    if (context.UserMails.Any(o => o.ClientUser == userMail.ClientUser))
                    {
                        return BadRequest();
                    }

                    context.Add(userMail);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(UserMailList));
                }
                else
                {
                    try
                    {

                        context.Update(userMail);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    return RedirectToAction(nameof(UserMailList));

                }
            }
            return View(userMail);
        }

        public async Task<IActionResult> DeleteClientMailList(int id)
        {


            var userMail = await context.UserMails.FindAsync(id);
            if (userMail == null)
            {
                return NotFound();

            }
            context.UserMails.Remove(userMail);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(UserMailList));
        }




        [HttpGet]
        public async Task<IActionResult> GroupAddOrEdit(int id = 0)
        {
            ViewBag.ComId= HttpContext.Request.Cookies["ComID"];
            if (id == 0)
            {
                return View(new Group());
            }
            else
            {
                var group = await context.Groups.FindAsync(id);
                if (group == null)
                {
                    return NotFound();
                }
                return View("GroupAddOrEdit", group);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GroupAddOrEdit(Group group)
        {
            if (ModelState.IsValid)
            {
                if (group.GroupID <= 0)
                {
                    context.Add(group);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(GroupIndex));
                }
                else
                {
                    try
                    {

                        context.Update(group);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    return RedirectToAction(nameof(GroupIndex));

                }
            }
            return View(group);
        }


        public async Task<IActionResult> DeleteGroup(int id)
        {


            var group = await context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();

            }
            context.Groups.Remove(group);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(GroupIndex));
        }

        
        public async Task<IActionResult> AdminGroupList()
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var admin = HttpContext.User.Identity.Name;

            var group = await (from p in context.Groups
                           join e in context.GroupUsers
                           on p.GroupID equals e.GroupID
                           where e.UserName == admin where p.ComID==comid
                               select new Group
                           {
                               GroupName = p.GroupName,
                               GroupID= p.GroupID
                           }).ToListAsync();

            return View(group);
        }


        public async Task<IActionResult> AssignAdmin(int id)
        {
            //var admin = await userManager.GetUsersInRoleAsync("Admin");

            var comid = HttpContext.Request.Cookies["ComID"];

            var admin = userManager.GetUsersInRoleAsync("Admin").Result.Where(x => x.ComID == comid);


            //var allUser = await userManager.Users.Where(x => x.ComID == comid).ToListAsync();

            var result = await context.GroupUsers.Include(x => x.Group).Where(x => x.GroupID == id).ToListAsync();
            ViewBag.GroupId = id;

            ViewData["User"] = new SelectList(admin, "UserName", "UserName");
            return View(result);
        }


        [HttpPost]
        public JsonResult AssignAdminToGroup(string user, int groupid)
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            if (ModelState.IsValid)
            {

                var model = new GroupUser
                {
                    UserName = user,
                    GroupID = groupid,
                    ComID=comid
                };

                if (context.GroupUsers.Any(o => o.UserName == user && o.GroupID == groupid))
                {
                    return Json(new { res = false }); ;
                }
                else
                {
                    context.GroupUsers.Add(model);
                    context.SaveChanges();
                }

            }

            return Json(new { res = true });


        }



        public async Task<IActionResult> AssignAgent(int id)
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            //var agent = await userManager.GetUsersInRoleAsync("Agent");
            var agent = userManager.GetUsersInRoleAsync("Agent").Result.Where(x => x.ComID == comid);


            //var allUser = await userManager.Users.Where(x => x.ComID == comid).ToListAsync();

            var result = await context.GroupUsers.Include(x => x.Group).Where(x => x.GroupID == id).ToListAsync();
            ViewBag.GroupId = id;
            ViewData["User"] = new SelectList(agent, "UserName", "UserName");
            return View(result);
        }

        [HttpPost]
        public JsonResult AssignAgentToGroup(string user, int groupid)
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            if (ModelState.IsValid)
            {

                var model = new GroupUser
                {
                    UserName = user,
                    GroupID = groupid,
                    ComID=comid
                };

                if (context.GroupUsers.Any(o => o.UserName == user && o.GroupID == groupid))
                {
                    return Json(new { res = false }); ;
                }
                else
                {
                    context.GroupUsers.Add(model);
                    context.SaveChanges();
                }

            }

            return Json(new { res = true });


        }


        public async Task<IActionResult> AdminDashBoard()
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            ViewBag.Com = comid;

            

            var tickets = await context.Tickets.Include(x => x.Status).Where(x=>x.ComID==comid).ToListAsync();
            return View(tickets);
        }

        public IActionResult TicketStatusReport()
        {
            return View();
        }

        public IActionResult SupportAgentReport()
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var agent = userManager.GetUsersInRoleAsync("Agent").Result.Where(x=>x.ComID==comid);


            //var agent = await userManager.Users.Where(x => x.ComID == comid).ToListAsync();

            ViewData["User"] = new SelectList(agent, "UserName", "UserName");
            return View();
        }

        public IActionResult GroupTicketReport()
        {
            return View();
        }

        public ActionResult TodayGroupStatus()
        {
            var comid = HttpContext.Request.Cookies["ComID"];


            var group = context.Tickets.Include(x => x.Group).Where(x => x.CreatedTime.Date.Day == DateTime.Today.Date.Day && x.ComID== comid).Select(x => new { x.Group.GroupName, x.GroupID }).Distinct().ToList();

            var list = new List<GroupTicketCount>();
            foreach (var item in group)
            {
                var x = context.Tickets.Where(x => x.GroupID == item.GroupID && x.CreatedTime.Date.Day== DateTime.Today.Date.Day && x.ComID==comid).Count();

                list.Add(new GroupTicketCount { GroupName = item.GroupName, TicketCount = x });

            }

            var json = list.ToGoogleDataTable()
                    .NewColumn(new Column(ColumnType.String, "Ticket Created In This Group Today"), x => x.GroupName)
                    .NewColumn(new Column(ColumnType.Number, "Ticket Count"), x => x.TicketCount)
                    .Build()
                    .GetJson();

            return Content(json);
        }


        public ActionResult MonthlyGroupStatus()
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var group = context.Tickets.Include(x => x.Group).Where(x => x.CreatedTime.Date.Month == DateTime.Today.Date.Month && x.ComID==comid).Select(x => new { x.Group.GroupName, x.GroupID }).Distinct().ToList();

            var list = new List<GroupTicketCount>();
            foreach (var item in group)
            {
                var x = context.Tickets.Where(x => x.GroupID == item.GroupID && x.CreatedTime.Date.Month == DateTime.Today.Date.Month && x.ComID==comid).Count();

                list.Add(new GroupTicketCount { GroupName = item.GroupName, TicketCount = x });

            }

            var json = list.ToGoogleDataTable()
                    .NewColumn(new Column(ColumnType.String, "Ticket Created In This Group In This Month"), x => x.GroupName)
                    .NewColumn(new Column(ColumnType.Number, "Ticket Count"), x => x.TicketCount)
                    .Build()
                    .GetJson();

            return Content(json);
        }


        public ActionResult YearlyGroupStatus()
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var group = context.Tickets.Include(x => x.Group).Where(x => x.CreatedTime.Date.Year == DateTime.Today.Date.Year && x.ComID==comid).Select(x => new { x.Group.GroupName, x.GroupID }).Distinct().ToList();

            var list = new List<GroupTicketCount>();
            foreach (var item in group)
            {
                var x = context.Tickets.Where(x => x.GroupID == item.GroupID && x.CreatedTime.Date.Year == DateTime.Today.Date.Year && x.ComID==comid).Count();

                list.Add(new GroupTicketCount { GroupName = item.GroupName,  TicketCount = x });



            }

            var json = list.ToGoogleDataTable()
                    .NewColumn(new Column(ColumnType.String, "Ticket Created In This Group In This Month"), x => x.GroupName)
                    .NewColumn(new Column(ColumnType.Number, "Ticket Count"), x => x.TicketCount)
                    .Build()
                    .GetJson();

            return Content(json);
        }


        public ActionResult TodayTicketStatusChartData()
        {
            var comid = HttpContext.Request.Cookies["ComID"];


            var open = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Open" && x.CreatedTime.Date.Day == DateTime.Today.Date.Day && x.ComID==comid).Count();
            var closed = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Closed" && x.ClosedTime.Date.Day == DateTime.Today.Date.Day && x.ComID == comid).Count();
            var reopen = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Re-Opened" && x.CreatedTime.Date.Day == DateTime.Today.Date.Day && x.ComID == comid).Count();

            var pending = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Pending" && x.CreatedTime.Date.Day == DateTime.Today.Date.Day && x.ComID == comid).Count();

            var ticket = new[]
                {
                    new {Ticket = "Open Tickets", Count = open},
                    new {Ticket = "Closed Tickets", Count = closed},
                    new {Ticket = "Re-Opened Tickets", Count = reopen},
                    new {Ticket = "Pending Tickets", Count = pending}

                };

            var json = ticket.ToGoogleDataTable()
                    .NewColumn(new Column(ColumnType.String, "Today Ticket Status"), x => x.Ticket)
                    .NewColumn(new Column(ColumnType.Number, "Count"), x => x.Count)
                    .Build()
                    .GetJson();

            return Content(json);

        }


        public ActionResult MonthTicketStatusChartData()
        {
            var comid = HttpContext.Request.Cookies["ComID"];


            var open = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Open" && x.CreatedTime.Date.Month == DateTime.Today.Date.Month && x.ComID == comid).Count();
            var closed = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Closed" && x.CreatedTime.Date.Month == DateTime.Today.Date.Month && x.ComID == comid).Count();
            var reopen = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Re-Opened" && x.CreatedTime.Date.Month == DateTime.Today.Date.Month && x.ComID == comid).Count();
            var pending = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Pending" && x.CreatedTime.Date.Month == DateTime.Today.Date.Month && x.ComID == comid).Count();

            var ticket = new[]
                {
                    new {Ticket = "Open Tickets", Count = open},
                    new {Ticket = "Closed Tickets", Count = closed},
                    new {Ticket = "Re-Opened Tickets", Count = reopen},
                    new {Ticket = "Pending Tickets", Count = pending}

                };

            var json = ticket.ToGoogleDataTable()
                    .NewColumn(new Column(ColumnType.String, "Month Ticket Status"), x => x.Ticket)
                    .NewColumn(new Column(ColumnType.Number, "Count"), x => x.Count)
                    .Build()
                    .GetJson();

            return Content(json);

        }

        public ActionResult YearlyTicketStatusChartData()
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var open = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Open" && x.CreatedTime.Date.Year == DateTime.Today.Date.Year && x.ComID == comid).Count();
            var closed = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Closed" && x.CreatedTime.Date.Year == DateTime.Today.Date.Year && x.ComID == comid).Count();
            var reopen = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Re-Opened" && x.CreatedTime.Date.Year == DateTime.Today.Date.Year && x.ComID == comid).Count();
            var pending = context.Tickets.Include(x => x.Status).Where(x => x.Status.StatusName == "Pending" && x.CreatedTime.Date.Year == DateTime.Today.Date.Year && x.ComID == comid).Count();

            var ticket = new[]
                {
                    new {Ticket = "Open Tickets", Count = open},
                    new {Ticket = "Closed Tickets", Count = closed},
                    new {Ticket = "Re-Opened Tickets", Count = reopen},
                    new {Ticket = "Pending Tickets", Count = pending}

                };

            var json = ticket.ToGoogleDataTable()
                    .NewColumn(new Column(ColumnType.String, "Month Ticket Status"), x => x.Ticket)
                    .NewColumn(new Column(ColumnType.Number, "Count"), x => x.Count)
                    .Build()
                    .GetJson();

            return Content(json);

        }

        public IActionResult TodaySupportCountByAgent(string supportAgent)
        {

            var comid = HttpContext.Request.Cookies["ComID"];


            var todaySupport = context.Tickets.Where(x=> x.SupportGivenBy== supportAgent && x.ClosedTime.Date.Day == DateTime.Today.Date.Day && x.ComID == comid).Count();

            var support = new[]
                {
                    new {Support = DateTime.Today.ToShortDateString(), Day= DateTime.Now.DayOfWeek, Count = todaySupport},
                    
                };

            var json = support.ToGoogleDataTable()
                   .NewColumn(new Column(ColumnType.String, "Support Date"), x => x.Support +  " - " +x.Day)
                   .NewColumn(new Column(ColumnType.Number, $"Support Given Today By {supportAgent}"), x => x.Count)
                   .Build()
                   .GetJson();
            return Content(json);
        }

        public IActionResult MonthlySupportCountByAgent(string supportAgent)
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var todaySupport = context.Tickets.Where(x => x.SupportGivenBy == supportAgent && x.CreatedTime.Date.Month == DateTime.Today.Date.Month && x.ComID == comid).Count();

            var support = new[]
                {
                    new {Support = DateTime.Now.ToString("MMMM"), Year = DateTime.Now.Year, Count = todaySupport},

                };

            var json = support.ToGoogleDataTable()
                   .NewColumn(new Column(ColumnType.String, "Support Date"), x => x.Support + " - " +x.Year )
                   .NewColumn(new Column(ColumnType.Number, $"Support Given In This Month By {supportAgent}"), x => x.Count)
                   .Build()
                   .GetJson();
            return Content(json);
        }

        public IActionResult YearlySupportCountByAgent(string supportAgent)
        {
            var comid = HttpContext.Request.Cookies["ComID"];

            var todaySupport = context.Tickets.Where(x => x.SupportGivenBy == supportAgent && x.CreatedTime.Date.Year == DateTime.Today.Date.Year && x.ComID == comid).Count();

            var support = new[]
                {
                    new {Support = DateTime.Now.Year, Count = todaySupport},

                };

            var json = support.ToGoogleDataTable()
                   .NewColumn(new Column(ColumnType.String, "Support Date"), x => x.Support)
                   .NewColumn(new Column(ColumnType.Number, $"Support Given In This Year By {supportAgent}"), x => x.Count)
                   .Build()
                   .GetJson();
            return Content(json);
        }




        public IActionResult SendEmail(string email, string subject, string body)
        {

            var message = new MailMessage();
            message.From = new MailAddress(config.GetSection("CredentialMail").Value, "Genuine Technology & Research Ltd.");


            string[] mailSplit = email.Split(", ");
            foreach (var mail in mailSplit)
            {
                message.To.Add(new MailAddress(mail));

            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var client = new SmtpClient())
            {
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(config.GetSection("CredentialMail").Value, config.GetSection("CredentialPassword").Value);
                client.Send(message);

            }

            return View();
        }

    }
}

