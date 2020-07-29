using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SharesAPI.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        //-------------------- SHARES
        public async Task<IActionResult> Index()
        {
            return View(await db.Shares.Include(p => p.Groups).ToListAsync());
        }
        public IActionResult CreateShares()
        {
            ViewBag.Groups = new SelectList(db.Groups, "GroupId", "GroupName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateShares(Shares Shares)
        {
            db.Shares.Add(Shares);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //-------------------- GROUPS
        public async Task<IActionResult> IndexGroups()
        {
            return View(await db.Groups.ToListAsync());
        }
        public IActionResult CreateGroups()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroups(Groups Groups)
        {
            db.Groups.Add(Groups);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexGroups");
        }
    }

}
