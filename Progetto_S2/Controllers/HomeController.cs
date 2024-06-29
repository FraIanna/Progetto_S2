using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Progetto_S2.Models;
using System.Diagnostics;

namespace Progetto_S2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }        

        public IActionResult Index()
        {
            var articles = ArticleRepository.GetAll();
            return View(articles);
        }

        public IActionResult Details(int id)
        {
            var article = ArticleRepository.GetById(id);
            if (article == null)
            {
                return View(new ErrorViewModel());
            }
            return View(article);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Article article)
        {
                article.Id = ArticleRepository.GetNextId();
                ArticleRepository.Articles.Add(article);

                string uploads = Path.Combine(_env.WebRootPath, "Images");
                var coverFileName = $"{article.Id}_cover.jpg";
                var coverfilepath = Path.Combine(uploads, coverFileName);

                if (article.CoverImageUrl != null && article.CoverImageUrl.Length > 0)
                {
                    using Stream filestream = new FileStream(coverfilepath, FileMode.Create);
                    article.CoverImageUrl.CopyTo(filestream);
                    article.CoverImagePath = $"/Images/{coverFileName}";
                }
                var additionalImages = new[] { article.AdditionalImage1, article.AdditionalImage2 };
                for (int i = 0; i < additionalImages.Length; i++)
                {
                    if (additionalImages[i] != null && additionalImages[i].Length > 0)
                    {
                        var additionalFileName = $"{article.Id}_additional_{i + 1}.jpg";
                        var additionalFilePath = Path.Combine(uploads, additionalFileName);
                        using Stream additionalFileStream = new FileStream(additionalFilePath, FileMode.Create);
                        additionalImages[i].CopyTo(additionalFileStream);

                        if (i == 0)
                        {
                            article.AdditionalImagePath1 = $"/Images/{additionalFileName}";
                        }
                        else if (i == 1)
                        {
                            article.AdditionalImagePath2 = $"/Images/{additionalFileName}";
                        }
                    }
                }
            return RedirectToAction(nameof(Index));
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
