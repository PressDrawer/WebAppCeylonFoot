using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;
using System.IO.Pipelines;
using System.Collections.Generic;
using Bogus.DataSets;
using Microsoft.AspNetCore.Hosting;

namespace WebApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly WebAppDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        [System.Obsolete]
        public PostsController(WebAppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var post =  _context.Posts
                .Include(p => p.Category)
                .Include(p => p.District)
                .AsNoTracking();
            return View(await post.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        [HttpGet]
        [Authorize]
        public  IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictName"); 
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("CategoryId,DistrictId,Title,Description,Images,PostFeatures,Contact")]  Post post)
        {
            
            if (ModelState.IsValid)
            {
                /* using (var ms = new MemoryStream())
                 {
                     await Images.CopyToAsync(ms);
                     var imagesByteArray = ms.ToArray();
                     Post.Image = imagesByteArray;
                     // var filePath = Path.GetTempFileName();
                     //  Images.CopyTo(ms);
                     //  post.Images = ms.CopyToAsync();
                 } */
               

                long size = Post.Images.Sum(f => f.Length);

                var filePaths = new List<string>();
                foreach (var formFile in Post.Images)
                {
                    if (formFile.Length > 0)
                    {
                        // full path to file in temp location
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                        //var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                        filePaths.Add(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                _context.Add(post);          
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,DistrictId,Title,Description,Images,PostFeatures,Contact")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
