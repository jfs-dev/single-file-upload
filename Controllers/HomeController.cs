using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SingleFileUpload.Models;

namespace SingleFileUpload.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Upload()
    {
        return View(new SingleFileUploadViewModel());
    }

    [HttpPost]
    public IActionResult Upload(SingleFileUploadViewModel model)
    {
        model.IsSuccess = false;

        if (ModelState.IsValid)
        {
            model.IsResponse = true;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedFiles");

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            if (model.File != null)
            {
                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string fileName = model.FileName + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }

                model.IsSuccess = true;
                model.Message = "Upload de arquivo realizado com sucesso.";
            }
            else
            {
                model.Message = "Favor escolher um arquivo!";
            }
        }
        else
        {
            model.Message = "Ops! Algo deu errado.";
        }

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
