using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db
{
    public interface IArticlesRepositry
{
        public  Task<bool> CreateArticle(Article article, Guid userId);
        public Task<bool> UpdateArticle(Article Article);
        public  Task<Article> GetArticleById(Guid ArticleId);
        public Task<IEnumerable<Article>> GetAllArticle();
        public Task<bool> DeleteArticle(Guid ArtcleId);


}
}
