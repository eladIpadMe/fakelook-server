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
    public class CommentRepository : ICommentRepository
    {
        readonly private DataContext _context;
        readonly private IDtoConverter _converter;
        public CommentRepository(DataContext context, IDtoConverter converter)
        {
            _context = context;
            _converter = converter;
        }
        public async Task<Comment> Add(Comment item)
        {
                var res = _context.Comments.Add(item);
                await _context.SaveChangesAsync();
                return res.Entity;
        }

        public async Task<Comment> Delete(int id)
        {
            var comment = GetById(id);
            var res = _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Comment> Edit(Comment item)
        {
            var res = _context.Comments.Update(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public ICollection<Comment> GetAll()
        {
            return _context.Comments
                .Include(c => c.Tags)
                .Include(c => c.UserTaggedComment)
                .Select(DtoLogic)
                .ToList();
        }

        public Comment GetById(int id)
        {
            return _context.Comments
                .Include(c => c.Tags)
                .Include(c => c.UserTaggedComment)
                .Select(DtoLogic)
                .SingleOrDefault(u => u.Id == id);
        }

        public ICollection<Comment> GetByPredicate(Func<Comment, bool> predicate)
        {
            return _context.Comments
                .Include(c => c.Tags)
                .Include(c => c.UserTaggedComment)
                .Select(DtoLogic)
                .Where(predicate)
                .ToList();
        }

        private Comment DtoLogic(Comment comment)
        {
            var dtoComment = _converter.DtoComment(comment);
            dtoComment.User = _converter.DtoUser(comment.User);
            dtoComment.UserTaggedComment = comment.UserTaggedComment.Count > 0 ?
                comment.UserTaggedComment.Select(utc =>
                {
                    var userTagged = _converter.DtoUserTaggedComment(utc);
                    return userTagged;
                }).ToList() : new List<UserTaggedComment>();

            dtoComment.Tags = comment.Tags.Count > 0 ?
                comment.Tags.Select(ct =>
                {
                    var commentTag = _converter.DtoTag(ct);
                    return commentTag;
                }).ToList() : new List<Tag>();
            return dtoComment;
        }
    }
}
