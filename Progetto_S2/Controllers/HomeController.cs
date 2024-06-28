using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Progetto_S2.Models;
using System.Diagnostics;

namespace Progetto_S2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _imagesDirectory;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
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
            if (ModelState.IsValid)
            {
                article.Id = ArticleRepository.GetNextId();
                ArticleRepository.Articles.Add(article);
            }
            return View("Index", ArticleRepository.GetAll());
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

        // Purtroppo non sono riuscito a configurare il funzionamento dell'implementazione delle immagini :/
    }
}
