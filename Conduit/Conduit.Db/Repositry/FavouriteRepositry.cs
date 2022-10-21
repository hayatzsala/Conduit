using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db.Repositry
{
    public class FavouriteRepositry:IFavouriteRepositry
    {
        private readonly ConduitContext _context;

        public FavouriteRepositry(ConduitContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFavourite(Guid userId ,Guid articleId)
        {
             await _context.Favourites.AddAsync(

               new Favourite
               {
                  ArticleId = articleId,
                  UserId = userId,
             
               });
            return await Save();

        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

    }
}
