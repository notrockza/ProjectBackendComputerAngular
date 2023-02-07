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
    public class ApiDetailProductsController : ControllerBase
    {
        private readonly ComputerdbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ApiDetailProductsController(ComputerdbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }
        [HttpPost]
        public async Task<ActionResult> PostDetailProductt([FromForm] DetailsProducts data, [FromForm] IFormFileCollection UpFile)
        {
            foreach (var file in UpFile)
            {
                #region ImageManageMent
                //               ได้WWW.rootออกมา           เก็บไว้ในuploads 
                var path = _environment.WebRootPath + Constants01.Directory;
                // if (UpFile != null && UpFile.Length > 0) เขียนอีกเเบบ
                
                try
                {
                    //uploads มีหรือป่าว ถ้าไม่มีให้สร้าง
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    //ตัดเอาเฉพาะชื่อไฟล์
                    var fileName = data.IdProductsDetails + "-" + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
                    if (file.FileName != null)
                    {
                        fileName += file.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                    }
                    //เอาไว้อ่านข้อมูลรูปภาพ
                    using (FileStream filestream =
                        System.IO.File.Create(path + fileName))
                    {
                        file.CopyTo(filestream);
                        filestream.Flush();
                        // ให้ data.Image เท่ากับข้อมูลในไฟล์ wwwroot/uploadsDetailProducts
                        data.Image = Constants01.Directory + fileName;
                        data.Id = data.IdProductsDetails + "-" + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss-fffff");
                    }
                }
                catch (Exception ex)
                {
                    return CreatedAtAction(nameof(PostDetailProductt), ex.ToString());
                }
                #endregion
                await _context.DetailsProducts.AddAsync(data);
                await _context.SaveChangesAsync();

            }
           
            return CreatedAtAction(nameof(PostDetailProductt), new { msg = "OK", data });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DetailsProducts>>> GetByIDProducts(int id)
        {
            var data = await _context.DetailsProducts.Where(e => e.IdProductsDetails == id).ToListAsync();

            return data;
        }


        // POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var ProductsDetail = await _context.DetailsProducts.FindAsync(id);
        //    _context.DetailsProducts.Remove(ProductsDetail);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductsExists(int id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}


        [HttpDelete("{id}")]
        public async Task<ActionResult<DetailsProducts>> DeleteProducts(string id)
        {
            var result = await _context.DetailsProducts.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            try
            {
                //ลบรูปภาพ
                var path = _environment.WebRootPath + result.Image;

                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

                _context.DetailsProducts.Remove(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(DeleteProducts), e.ToString());
            }

            return result;
        }
    }
}
