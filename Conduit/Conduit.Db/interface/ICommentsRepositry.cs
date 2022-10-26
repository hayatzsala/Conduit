using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db
{
    public interface ICommentsRepositry
{
        public Task<bool> CreateComment(Comment comment,Guid UserId);
        public Task<bool> DeleteComment(Guid CommentId, Guid ArticleId);
        public Task<IEnumerable<Comment>> GetAllComments(Guid ArticleId);
        public Task<Comment> GetComment(Guid CommetntId, Guid ArticleId);
}
}
