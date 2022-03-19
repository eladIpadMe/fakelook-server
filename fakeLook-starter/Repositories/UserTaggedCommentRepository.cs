using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class UserTaggedCommentRepository : IUserTaggedCommentRepository
    {
        readonly private DataContext _context;
        readonly private IDtoConverter _converter;

        public UserTaggedCommentRepository(DataContext context, IDtoConverter converter)
        {
            _context = context;
            _converter = converter;
        }
        public async Task<UserTaggedComment> Add(UserTaggedComment item)
        {
            var res = _context.UserTaggedComments.Add(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<UserTaggedComment> Delete(int id)
        {
            var userTaggedComment = GetById(id);
            var res = _context.UserTaggedComments.Remove(userTaggedComment);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<UserTaggedComment> Edit(UserTaggedComment item)
        {
            var res = _context.UserTaggedComments.Update(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public ICollection<UserTaggedComment> GetAll()
        {
            return _context.UserTaggedComments
                .Select(DtoLogic)
                .ToList();
        }

        public UserTaggedComment GetById(int id)
        {
            return _context.UserTaggedComments
                   .Select(DtoLogic)
                   .SingleOrDefault(u => u.Id == id);
        }

        public ICollection<UserTaggedComment> GetByPredicate(Func<UserTaggedComment, bool> predicate)
        {
            return _context.UserTaggedComments
                .Select(DtoLogic)
                .Where(predicate).ToList();
        }
        private UserTaggedComment DtoLogic(UserTaggedComment userTaggedComment)
        {
            var dtouserTaggedComment = _converter.DtoUserTaggedComment(userTaggedComment);
            dtouserTaggedComment.User = _converter.DtoUser(userTaggedComment.User);
            dtouserTaggedComment.Comment = _converter.DtoComment(userTaggedComment.Comment);
            return dtouserTaggedComment;
        }
    }
}
