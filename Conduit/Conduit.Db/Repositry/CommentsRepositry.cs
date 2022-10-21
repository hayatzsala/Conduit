using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db.Repositry
{
    public class CommentsRepositry:ICommentsRepositry
    {

        private readonly ConduitContext _context;


        public CommentsRepositry(ConduitContext context, IArticlesRepositry articlesRepositry)
        {
            _context = context;
        }

        public async Task<bool> CreateComment(Comment comment,Guid UserId)
        {

            await _context.Comments.AddAsync(

                  new Comment
                  {
                     ArticleId = comment.ArticleId,
                     CommentId = comment.CommentId,
                     content=comment.content,
                     UserId=UserId,
                  }

                  );

            return await Save();
        }

        public async Task<bool> DeleteComment(Guid CommentId, Guid ArticleId)
        {
            var Article = await GetComment(CommentId,ArticleId);
            if (Article != null)
            {
                _context.Remove(Article);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Comment>> GetAllComments(Guid articleId)
        {
       
            return await _context.Comments.Select(s => s).Where(k=>k.ArticleId.Equals(articleId)).ToListAsync();
        }

        public async Task<Comment> GetComment(Guid CommetntId,Guid ArticleId)
        {
            return await _context.Comments.Where(s => s.CommentId.Equals(CommetntId) &&s.ArticleId.Equals(ArticleId)
            ).SingleOrDefaultAsync();
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
