using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace fakeLook_starter.Repositories
{
    public class UserTaggedPostRepository : IUserTaggedPostRepository
    {
        readonly private DataContext _context;
        readonly private IDtoConverter _converter;

        public UserTaggedPostRepository(DataContext context, IDtoConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task<UserTaggedPost> Add(UserTaggedPost item)
        {
            var res = _context.UserTaggedPosts.Add(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<UserTaggedPost> Delete(int id)
        {
            var userTaggedPost = GetById(id);
            var res = _context.UserTaggedPosts.Remove(userTaggedPost);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<UserTaggedPost> Edit(UserTaggedPost item)
        {
            var res = _context.UserTaggedPosts.Update(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public ICollection<UserTaggedPost> GetAll()
        {
            return _context.UserTaggedPosts
                .Select(DtoLogic)
                .ToList();
        }

        public UserTaggedPost GetById(int id)
        {
            return _context.UserTaggedPosts
               .Select(DtoLogic)
               .SingleOrDefault(u => u.Id == id);
        }

        public ICollection<UserTaggedPost> GetByPredicate(Func<UserTaggedPost, bool> predicate)
        {
            return _context.UserTaggedPosts
            .Select(DtoLogic)
            .Where(predicate).ToList();
        }
        private UserTaggedPost DtoLogic(UserTaggedPost userTaggedPost)
        {
            var dtouserTaggedPost = _converter.DtoUserTaggedPost(userTaggedPost);
            dtouserTaggedPost.User = _converter.DtoUser(userTaggedPost.User);
            dtouserTaggedPost.Post = _converter.DtoPost(userTaggedPost.Post);
            return dtouserTaggedPost;
        }
    }
}
