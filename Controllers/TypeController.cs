using System;
using System.Collections.Generic;
using System.Linq;
using bsis3b_web.Data;
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
    }
}