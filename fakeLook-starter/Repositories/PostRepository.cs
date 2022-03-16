using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class PostRepository : IPostRepository
    {
        readonly private DataContext _context;
        readonly private DtoConverter _converter;
        public PostRepository(DataContext context, DtoConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task<Post> Add(Post item)
        {
            var res = _context.Posts.Add(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Post> Delete(int id)
        {
            var post = GetById(id);
            var res = _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Post> Edit(Post item)
        {
            var res = _context.Posts.Update(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public ICollection<Post> GetAll()
        {
            return _context.Posts
                .Include(p => p.Likes)
                .Include(p => p.Tags)
                .Include(p => p.UserTaggedPost)
                .ThenInclude(u=>u.User)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Tags)
                .Select(DtoLogic).ToList();
        }

        public Post GetById(int id)
        {
            var post = _context.Posts
                .Include(p => p.Likes)
                .Include(p => p.Tags)
                .Include(p => p.UserTaggedPost)
                .ThenInclude(u => u.User)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Tags)
                .Select(DtoLogic)
                .SingleOrDefault(p => p.Id == id);

            return post;
        }

        public ICollection<Post> GetByPredicate(Func<Post, bool> predicate)
        {
            return _context.Posts
                .Include(p => p.Likes)
                .Include(p => p.Tags)
                .Include(p => p.UserTaggedPost)
                .ThenInclude(u => u.User)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Tags)
                .Select(DtoLogic).Where(predicate).ToList();
        }

        private Post DtoLogic(Post post)
        {
            var dtoPost = _converter.DtoPost(post);
            dtoPost.User = _converter.DtoUser(post.User);
            dtoPost.Comments = post.Comments?.Select(c =>
            {
                var dtoComment = _converter.DtoComment(c);
                dtoComment.User = _converter.DtoUser(c.User);
                dtoComment.Tags = c.Tags?.Select(t => 
                {
                    _converter.DtoTag(t);
                    return t;
                }).ToArray();
                dtoComment.UserTaggedComment = c.UserTaggedComment.Select(ut =>
                {
                    _converter.DtoUserTaggedComment(ut);
                    return ut;

            }).ToArray();

                return dtoComment;
            }).ToArray();
            dtoPost.Likes = post.Likes.Select(l =>
            {
                var dtoLike = _converter.DtoLike(l);
                return dtoLike;
            }).ToArray();
            dtoPost.Tags = post.Tags?.Select(t =>
            {
                var dtoTag = _converter.DtoTag(t);
                return dtoTag;
            }).ToArray();
            dtoPost.UserTaggedPost = post.UserTaggedPost.Select(utp =>
            {
                var dtoUserTaggedPost = _converter.DtoUserTaggedPost(utp);
                return dtoUserTaggedPost;
            }).ToArray();
            return dtoPost;
        }


    }
}
