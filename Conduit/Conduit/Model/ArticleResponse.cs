
using Conduit.Db;

namespace Conduit.Model
{
    public class ArticleResponse
    {
        public List<Article> ArticlesList { get; set; }=new List<Article>();
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }

        public ArticleResponse()
        {
            ArticlesList = new List<Article>();
        }

    }
}
