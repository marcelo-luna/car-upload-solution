using FineData.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace FineData.Controllers
{
    public class FineInsertController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILoadService _loadService;

        public FineInsertController(IWebHostEnvironment environment, ILoadService loadService)
        {
            _environment = environment;
            _loadService = loadService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImportPeople(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            var filePath = Path.Combine(_environment.ContentRootPath, "Uploads", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            _loadService.ImportListOfPerson(filePath);

            return RedirectToAction("Index");
        }

        public IActionResult ImportCars(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            var filePath = Path.Combine(_environment.ContentRootPath, "Uploads", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            _loadService.ImportListOfCars(filePath);

            return RedirectToAction("Index");
        }
    }
}
