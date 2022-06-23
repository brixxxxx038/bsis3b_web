using System;
using System.Collections.Generic;
using System.Linq;
using bsis3b_web.Data;
using bsis3b_web.Models;
using bsis3b_web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bsis3b_web.Controllers
{
    public class TypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public TypeViewModel TypeVM { get; set; }

        public TypeController(ApplicationDbContext db)
        {
            _db = db;    
            TypeVM = new TypeViewModel()
            {
                Items = _db.Items.ToList(),
                Type = new Models.Type()
            } ;
                    
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Type = _db.Types.Include(m => m.Item);
            return View(Type);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View(TypeVM);
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost()
        {
            if(ModelState.IsValid)
            {
                _db.Types.Add(TypeVM.Type);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(TypeVM);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            TypeVM.Type = _db.Types.Include(m=>m.Item).SingleOrDefault(m=> m.Id == id);
            if(TypeVM.Type == null)
            {
                return NotFound();
            }
            return View(TypeVM);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost()
        {
            if(ModelState.IsValid)
            {
                _db.Types.Update(TypeVM.Type);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(TypeVM);
        }

        public IActionResult Delete(int id)
        {
            TypeVM.Type = _db.Types.Include(m=>m.Item).SingleOrDefault(m=> m.Id == id);
            if(TypeVM.Type == null)
            {
                return NotFound();
            }
            return View(TypeVM);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var Type = _db.Types.Find(id);
            if(Type == null)
            {
                return NotFound();
            }
                _db.Types.Remove(Type);
                _db.SaveChanges();
                return RedirectToAction("Index"); 
        }


    }
}