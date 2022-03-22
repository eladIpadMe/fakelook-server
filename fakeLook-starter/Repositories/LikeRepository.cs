using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        readonly private DataContext _context;
        readonly private IDtoConverter _converter;
        public LikeRepository(DataContext context, IDtoConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task<Like> Add(Like item)
        {
            var like = _context.Likes.SingleOrDefault(l => l.UserId == item.UserId && l.PostId == item.PostId);
            if(like == null)
            {
                var res = _context.Likes.Add(item);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            else
            {
                return like;
            }
        }

        public async Task<Like> Delete(int id)
        {
            var like = GetById(id);
            var res = _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Like> Edit(Like item)
        {
            var res = _context.Likes.Update(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public  ICollection<Like> GetAll()
        {
            return _context.Likes
               .Select(l => DtoLogic(l))
               .ToList();
        }

        public Like GetById(int id)
        {
            return _context.Likes
               .Select(DtoLogic)
               .SingleOrDefault(l => l.Id == id);
        }

        public ICollection<Like> GetByPredicate(Func<Like, bool> predicate)
        {
            return _context.Likes
             .Select(DtoLogic)
             .Where(predicate)
             .ToList();
        }

        private Like DtoLogic(Like like)
        {
            var dtoLike = _converter.DtoLike(like);
            dtoLike.User = _converter.DtoUser(like.User);
            dtoLike.Post = _converter.DtoPost(like.Post);
           
            return dtoLike;
        }
    }
}
