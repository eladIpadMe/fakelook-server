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
        readonly private IDtoConverter _converter;
        readonly private ITagRepository _tagRepository;
        public PostRepository(DataContext context, IDtoConverter converter
            ,ITagRepository tagRepository)
        {
            _context = context;
            _converter = converter;
            _tagRepository = tagRepository;
        }

        public async Task<Post> Add(Post item)
        {
            //for(int i = 0; i< item.Tags.Count; i++)
            //{
            //   var currentTag = _context.Tags.FirstOrDefault(t => t.Content == item.Tags[i].Content)
            //}
            var postHashTags = new List<Tag>();
            if (item.Tags != null)
            {
                foreach (var tag in item.Tags)
                {
                    var hashtag = _tagRepository.Add(tag);
                    postHashTags.Add(await hashtag);
                    //var currentTag = _context.Tags.FirstOrDefault(t => t.Content == tag.Content);
                    //if(currentTag != null)
                    //{
                    //    tag.Id = currentTag.Id;
                    //    tag.
                    //} 
                }
            }
            item.Tags = postHashTags;
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
            try
            {
                var post =_context.Posts.FirstOrDefault(p=> p.Id == item.Id);
                if (post == null) return null;
                _context.Entry<Post>(post).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
                return post;

            //var res = _context.Posts.Update(item);
            //await _context.SaveChangesAsync();
            //return res.Entity;
            }
            catch (Exception ex)
            {
                return new Post { };
            }

        }

        public ICollection<Post> GetAll()
        {
            return _context.Posts
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
                .Select(DtoLogic)
                .ToList();
        }
           
        public Post GetById(int id)
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
                    .Include(p => p.Comments)
                    .ThenInclude(c => c.UserTaggedComment)
                    .Select(DtoLogic)
                    .SingleOrDefault(p => p.Id == id);
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
                .Where(predicate)
                .Select(DtoLogic)
                .ToList();
        }
        public string GetUsernameById(int userId)
        {
           // var post = _context.Posts.SingleOrDefault(p => p.UserId == userId);
            return _context.Users.SingleOrDefault(u => u.Id == userId).UserName;
            //var likes = post.Likes;
            //return post.User.UserName;
                
        }

        public void ManageLike(int userId, int postId)
        {
            var post = GetById(postId);
            var like = post.Likes.SingleOrDefault(l => l.UserId == userId);
            if(like == null)
            {
                post.Likes.Append(new Like() {
                    IsActive = true,
                    UserId = post.UserId,
                    PostId = post.Id}
                );
            }
            else
            {
                _context.Posts.SingleOrDefault(p => p.Id == postId).
                    Likes.SingleOrDefault(l => l.UserId == userId)
                    .IsActive = !like.IsActive;
            }
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
                    var dtoTag = _converter.DtoTag(t);
                    return dtoTag;
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
                dtoLike.UserId = l.UserId;
                dtoLike.PostId = l.PostId;
                return dtoLike;
            }).ToArray() : new List<Like>();

            dtoPost.Tags = post.Tags.Count > 0 ? post.Tags.Select(t =>
            {
                var dtoTag = _converter.DtoTag(t);
                return dtoTag;
            }).ToArray() : new List<Tag>();

            dtoPost.UserTaggedPost = post.UserTaggedPost.Count > 0 ? post.UserTaggedPost.Select(utp =>
            {
                var dtoUserTaggedPost = _converter.DtoUserTaggedPost(utp);
                return dtoUserTaggedPost;
            }).ToArray() : new List<UserTaggedPost>();
            return dtoPost;
        }


    }
}
