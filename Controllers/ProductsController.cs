using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackendComputer.Models.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using BackendComputer.Helpers;
using System.IO;

namespace BackendComputer.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ComputerdbContext _context;
        private readonly IWebHostEnvironment _environment;

            public ProductsController(ComputerdbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        //GET: Products
        public async Task<IActionResult> Index()
        {
            var computerdbContext = _context.Products.Include(p => p.Stock).Include(p => p.IdTypeNavigation);
            return View(await computerdbContext.ToListAsync());
        }


        // GET: Products/Create
        public IActionResult Create()
        {
            //ViewData["IdType"] = new SelectList(_context.Stock, "Id", "Id");
            ViewData["PdStock"] = new SelectList(_context.Type, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Products data, IFormFile UpFile)
        {
            var result = await _context.Products.FindAsync(data.Id);
            if (result != null)
            {
                TempData["ChkError"] = "Error";
                return View();
            }

            #region ImageManageMent

            var path = _environment.WebRootPath + Constants.Directory;
            if (UpFile?.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    //ตัดเอาเฉพาะชื่อไฟล์
                    var fileName = data.Id + Constants.ProductImage;
                    if (UpFile.FileName != null)
                    {
                        fileName += UpFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                    }

                    using (FileStream filestream =
                        System.IO.File.Create(path + fileName))
                    {
                        UpFile.CopyTo(filestream);
                        filestream.Flush();

                        data.Image = Constants.Directory + fileName;
                    }
                }
                catch (Exception ex)
                {
                    return View();
                }
            }

            #endregion

            await _context.Products.AddAsync(data);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            //ViewData["IdStock"] = new SelectList(_context.Stock, "Id", "Id", products.IdStock);
            ViewData["IdType"] = new SelectList(_context.Type, "Id", "TypeName", products.IdType);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(Products data, IFormFile UpFile)
        {
            #region ImageManageMent

            var path = _environment.WebRootPath + Constants.Directory;
            if (UpFile?.Length > 0)
            {
                try
                {
                    //ลบรูปภาพเดิม
                    var oldpath = _environment.WebRootPath + data.Image;
                    if (System.IO.File.Exists(oldpath)) System.IO.File.Delete(oldpath);
                    //

                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    //ตัดเอาเฉพาะชื่อไฟล์
                    var fileName = data.Id + Constants.ProductImage;
                    if (UpFile.FileName != null)
                    {
                        fileName += UpFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                    }

                    using (FileStream filestream =
                        System.IO.File.Create(path + fileName))
                    {
                        UpFile.CopyTo(filestream);
                        filestream.Flush();

                        data.Image = Constants.Directory + fileName;
                    }
                }
                catch (Exception ex)
                {
                    return View();
                }
            }

            #endregion

            _context.Products.Update(data);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                //.Include(p => p.IdStockNavigation)
                .Include(p => p.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

       
    }
}
