using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db.Repositry
{
    public class ArticleRepositry : IArticlesRepositry
    {

        private readonly ConduitContext _context;
        public ArticleRepositry(ConduitContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateArticle(Article article,Guid userId)
        {

           await _context.Articles.AddAsync(

                 new Article
                 {
                     ArticleId = article.ArticleId,
                    Description = article.Description,
                    Image=article.Image,
                    Title = article.Title,
                    UserId= userId
                 }

                 );

            return await Save();
        }

        public async  Task<bool> DeleteArticle(Guid ArticleId)
        {
            var Article = await GetArticleById( ArticleId);
            if (Article != null)
            {
                _context.Remove(Article);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Article>> GetAllArticle()
        {
            return await _context.Articles.Select(s => s).ToListAsync();
        }

        public async Task<Article> GetArticleById(Guid ArticleId)
        {
            return await _context.Articles.Where(s=> s.ArticleId.Equals(ArticleId)).SingleOrDefaultAsync();
        }

        public async Task<bool> UpdateArticle(Article article)
        {

            var artcle = await GetArticleById(article.ArticleId);

            artcle.Title = article.Title;
            artcle.Description = article.Description;
            artcle.Image = article.Image;
            var save = await Save();
            if (save)
            {
                return true;

            }
            return false;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

      
    }
}
