using System.ComponentModel.DataAnnotations;

namespace Progetto_S2.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Display(Name = "Nome dell'articolo")]
        public string Name { get; set; }

        [Display(Name = "Prezzo dell'articolo")]
        public decimal Price { get; set; }

        [Display(Name = "Descrizione dell'articolo")]
        public string Description { get; set; }

        public string CoverImagePath { get; set; }

        public string AdditionalImagePath1 { get; set; }

        public string AdditionalImagePath2 { get; set; }

        [Display(Name = "Immagine di copertina dell'articolo")]
        public IFormFile CoverImageUrl { get; set; }

        [Display(Name = "Immagini addizionali dell'articolo")]
        public IFormFile AdditionalImage1 { get; set; }

        [Display(Name = "Immagini addizionali dell'articolo")]
        public IFormFile AdditionalImage2 { get; set; }


    }
}
