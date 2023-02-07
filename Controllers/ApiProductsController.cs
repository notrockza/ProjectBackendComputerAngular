using BackendComputer.Helpers;
using BackendComputer.Models.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendComputer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiProductsController : Controller
    {
        private readonly ComputerdbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ApiProductsController(ComputerdbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/ApiProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            //return await _context.Products.ToListAsync();
            return await _context.Products.Include(e => e.IdTypeNavigation).ToListAsync();
        }


        // GET: api/ApiProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/ApiProducts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        // Old

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProducts(int id, Products products)
        //{
        //    if (id != products.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(products).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        /////////////////////////////////////////////////////////////// new
        [HttpPost]
        public async Task<ActionResult> PostProducts([FromForm] Products data, [FromForm] IFormFile UpFile)
        {
            var result = await _context.Products.FindAsync(data.Id);
            if (result != null)
            {
                //return Conflict();
                return CreatedAtAction(nameof(PostProducts), new { msg = "รหัสสินค้าซ้ำ" });
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
                    return CreatedAtAction(nameof(PostProducts), ex.ToString());
                }
            }

            #endregion

            await _context.Products.AddAsync(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostProducts), new { msg = "OK", data });
        }
        ///////////////////////////////////////////////////////////////////////////////



        [HttpPut]
        public async Task<IActionResult> PutProducts([FromForm] Products data, [FromForm] IFormFile UpFile)
        {
            var result = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(data.Id));
            if (result == null)
            {
                return NotFound();
            }

            #region ImageManageMent

            var path = _environment.WebRootPath + Constants.Directory;
            if (UpFile?.Length > 0)
            {
                try
                {
                    //ลบรูปภาพเดิม
                    var oldpath = _environment.WebRootPath + result.Image;
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
                    return CreatedAtAction(nameof(PutProducts), new { msg = ex.ToString() });
                }
            }

            #endregion

            _context.Products.Update(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PutProducts), new { msg = "OK", data });
        }




        ///////////////////////////////////////////////////////////////////////////// Old
        // POST: api/ApiProducts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.


        //[HttpPost]
        //public async Task<ActionResult<Products>> PostProducts(Products products)
        //{
        //    _context.Products.Add(products);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ProductsExists(products.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetProducts", new { id = products.Id }, products);
        //}
        ////////////////////////////////////////////////////////////////////////////////

        // DELETE: api/ApiProducts/5

        // DELETE: ApiProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var result = await _context.Products.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            try
            {
                //ลบรูปภาพ
                var path = _environment.WebRootPath + result.Image;

                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

                _context.Products.Remove(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(DeleteProducts), e.ToString());
            }

            return result;
        }

        ////////////////////////////////// Old na
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Products>> DeleteProducts(int id)
        //{
        //    var products = await _context.Products.FindAsync(id);
        //    if (products == null)      
        //    {
        //        return NotFound();
        //    }

        //    _context.Products.Remove(products);
        //    await _context.SaveChangesAsync();

        //    return products;
        //}

        //private bool ProductsExists(int id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}


     //Get /ApiProducts/SearchProducts/aa
    [Route("SearchProducts/{keyword}")]
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Products>>> SearchProducts(string keyword)
        {
            var result = await _context.Products.Where(p => p.ProductName.Contains(keyword)).ToListAsync();
            return result;
    }
    }
}
