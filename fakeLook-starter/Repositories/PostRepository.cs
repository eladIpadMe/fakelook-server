﻿using fakeLook_dal.Data;
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
        readonly private IDtoConverter _converter;
        public PostRepository(DataContext context, IDtoConverter converter)
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
            ICollection < Post > posts = _context.Posts
                .Include(p => p.Likes)
                .Include(p => p.Tags)
                .Include(p => p.UserTaggedPost)
                .ThenInclude(u => u.User)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.UserTaggedComment)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Tags)
                .Select(DtoLogic).ToList();

            return posts;
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
            .Include(p => p.Comments)
            .ThenInclude(c => c.UserTaggedComment)
            .Select(DtoLogic)
            .SingleOrDefault(p => p.Id == id);

            return post;
        }

        public ICollection<Post> GetByPredicate(Func<Post, bool> predicate)
        {
            return _context.Posts
                .Include(p => p.Likes)
                .ThenInclude(l => l.User)
                .Include(p => p.Tags)
                .Include(p => p.UserTaggedPost)
                .ThenInclude(u => u.User)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Tags)
                .Include(p => p.Comments)
                .ThenInclude(c => c.UserTaggedComment)
                .Select(DtoLogic).Where(predicate).ToList();
        }

        private Post DtoLogic(Post post)
        {
            var dtoPost = _converter.DtoPost(post);
            dtoPost.User = _converter.DtoUser(post.User);
            dtoPost.Comments = post.Comments.Count > 0 ? post.Comments.Select(c =>
            {
                var dtoComment = _converter.DtoComment(c);
                dtoComment.User = _converter.DtoUser(c.User);
                //_converter.DtoUser(c.User);
                dtoComment.Tags = c.Tags.Count > 0 ? c.Tags.Select(t =>
                {
                    _converter.DtoTag(t);
                    return t;
                }).ToArray() : new List<Tag>();
              
                dtoComment.UserTaggedComment = c.UserTaggedComment.Count > 0 ? c.UserTaggedComment.Select(ut =>
                    {
                        return _converter.DtoUserTaggedComment(ut);

                    }).ToArray() : new List<UserTaggedComment>();
                
                return dtoComment;
            }).ToArray() : new List<Comment>(); 

            dtoPost.Likes = post.Likes.Count > 0 ? post.Likes.Select(l =>
            {
                var dtoLike = _converter.DtoLike(l);
                dtoLike.User = _converter.DtoUser(l.User);
                return dtoLike;
            }).ToArray(): new List<Like>(); 

            dtoPost.Tags = post.Tags.Count > 0 ? post.Tags.Select(t =>
            {
                var dtoTag = _converter.DtoTag(t);
                return dtoTag;
            }).ToArray(): new List<Tag>();

            dtoPost.UserTaggedPost = post.UserTaggedPost.Count > 0 ? post.UserTaggedPost.Select(utp =>
            {
                var dtoUserTaggedPost = _converter.DtoUserTaggedPost(utp);
                return dtoUserTaggedPost;
            }).ToArray() : new List<UserTaggedPost>();
            return dtoPost;
        }


    }
}
