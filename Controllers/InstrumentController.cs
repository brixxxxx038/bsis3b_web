using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bsis3b_web.Data;
using bsis3b_web.Models;
using bsis3b_web.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bsis3b_web.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public InstrumentViewModel InstrumentVM { get; set; }



        public InstrumentController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;  
            _hostingEnvironment = hostingEnvironment;
            InstrumentVM = new InstrumentViewModel()
            {
                Items = _db.Items.ToList(),
                Types = _db.Types.ToList(),
                Instrument = new Models.Instrument()
            } ;
                    
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Instrument = _db.Instruments.Include(m => m.Item) .Include(m => m.Type);
            return View(Instrument);
        }

        [HttpGet]
        public IActionResult Create()
        {
        
            return View(InstrumentVM);
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost()
        {
            if(ModelState.IsValid)
            {
                _db.Instruments.Add(InstrumentVM.Instrument);
                _db.SaveChanges();

                var instrumentId = InstrumentVM.Instrument.Id;

                string wwwrootPath = _hostingEnvironment.WebRootPath;

                var files = HttpContext.Request.Form.Files;

                var SaveInstrument = _db.Instruments.Find(instrumentId);

                if(files.Count != 0)
                {
                    var ImagePath = @"images\instrument\";
                    var Extension = Path.GetExtension(files[0].FileName);
                    var RelativeImagePath = ImagePath + instrumentId + Extension ;
                    var AbsImagePath = Path.Combine(wwwrootPath, RelativeImagePath);

                    using (var fileStream = new FileStream(AbsImagePath,FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    SaveInstrument.ImagePath = RelativeImagePath;
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            InstrumentVM.Items = _db.Items.ToList();
            return View(InstrumentVM);
        }

        // [HttpGet]
        // public IActionResult Edit(int id)
        // {
        //     TypeVM.Type = _db.Types.Include(m=>m.Item).SingleOrDefault(m=> m.Id == id);
        //     if(TypeVM.Type == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(TypeVM);
        // }

        // [HttpPost]
        // [ActionName("Edit")]
        // public IActionResult EditPost()
        // {
        //     if(ModelState.IsValid)
        //     {
        //         _db.Types.Update(TypeVM.Type);
        //         _db.SaveChanges();
        //         return RedirectToAction("Index");
        //     }
        //     return View(TypeVM);
        // }   


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var instrument = _db.Instruments.Find(id);
            if(instrument == null)
            {
                return NotFound();
            }
                _db.Instruments.Remove(instrument);
                _db.SaveChanges();
                return RedirectToAction("Index"); 
        }


    }
}