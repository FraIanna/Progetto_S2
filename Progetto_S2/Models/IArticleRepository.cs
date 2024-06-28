namespace Progetto_S2.Models
{
    public static class ArticleRepository
    {
        private static int _nextId = 0;

        private static List<Article> _articles = new List<Article>();
        public static List<Article> Articles { get { return _articles; } }

        public static IEnumerable<Article> GetAll()
        {
            return _articles;
        }
       
        public static Article GetById(int id)
        {
            return _articles.FirstOrDefault(a => a.Id == id);
        }

        public static int GetNextId()
        {
            return _nextId++;
        }
    }
}
