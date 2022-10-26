using AutoMapper;
using Conduit.Dto;
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
        public IMapper _mapper;
        public ArticleRepositry(ConduitContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Article ArticleMapping(ArticleD article)
        {
            return _mapper.Map<Article>(article);
        }
        public async Task<bool> CreateArticle(ArticleD article,Guid userId)
        {
            await _context.Articles.AddAsync(
                new Article
                {
                    ArticleId = new Guid(),
                    Description=article.Description,
                    Image=article.Image,
                    Title=article.Title,
                    UserId=userId
                }

                ); ;

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
        public  async Task<List<Article>> GetAllArticlePaginated(int PageNumber,double pageResult)
        {
            return  await  _context.Articles.Skip((PageNumber-1)*(int)pageResult).Take((int)pageResult).ToListAsync();
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
