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
        public async Task<IActionResult> Index(string group)
        {
            IQueryable<Shares> Shares = db.Shares.Include(p => p.Groups);
            if (!String.IsNullOrEmpty(group))
            {
                Shares = Shares.Where(p => p.Groups.GroupName == group);
                if (Shares != null)
                    return View(Shares);
            }
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

        public async Task<IActionResult> EditShares(int? id)
        {
            ViewBag.Groups = new SelectList(db.Groups, "GroupId", "GroupName");
            if (id != null)
            {
                Shares Shares = await db.Shares.FirstOrDefaultAsync(p => p.ShareId == id);
                if (Shares != null)
                    return View(Shares);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditShares(Shares Shares)
        {
            db.Shares.Update(Shares);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("DeleteShares")]
        public async Task<IActionResult> ConfirmDeleteShares(int? id)
        {
            if (id != null)
            {
                Shares Shares = await db.Shares.Include(p => p.Groups).FirstOrDefaultAsync(p => p.ShareId == id);
                if (Shares != null)
                    return View(Shares);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteShares(int? id)
        {
            if (id != null)
            {
                Shares Shares = await db.Shares.FirstOrDefaultAsync(p => p.ShareId == id);
                if (Shares != null)
                {
                    db.Shares.Remove(Shares);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        //-------------------- GROUPS
        public async Task<IActionResult> IndexGroups(string? name)
        {
            IQueryable<Groups> Groups = db.Groups;
            if (!String.IsNullOrEmpty(name))
            {
                Groups = Groups.Where(p => p.GroupName == name);
                if (Groups != null)
                return View(Groups);
            }
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

        public async Task<IActionResult> EditGroups(int? id)
        {
            if (id != null)
            {
                Groups Groups = await db.Groups.FirstOrDefaultAsync(p => p.GroupId == id);
                if (Groups != null)
                return View(Groups);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditGroups(Groups Groups)
        {
            db.Groups.Update(Groups);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexGroups");
        }

        [HttpGet]
        [ActionName("DeleteGroups")]
        public async Task<IActionResult> ConfirmDeleteGroups(int? id)
        {
            if (id != null)
            {
                Groups Groups = await db.Groups.FirstOrDefaultAsync(p => p.GroupId == id);
                if (Groups != null)
                    return View(Groups);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroups(int? id)
        {
            if (id != null)
            {
                Groups Groups = await db.Groups.FirstOrDefaultAsync(p => p.GroupId == id);
                if (Groups != null)
                {
                    db.Groups.Remove(Groups);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexGroups");
                }
            }
            return NotFound();
        }
    }

}
