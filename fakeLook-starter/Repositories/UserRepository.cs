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
    public class UserRepository : IUserRepository
    {
            readonly private DataContext _context;
            //readonly private IDtoConverter _converter;

        public UserRepository(DataContext context)//, IDtoConverter converter)
        {
            _context = context;
            //_converter = converter;
        }
        public async Task<User> Add(User item)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == item.UserName);
            
            if(user == null) {
                var res = _context.Users.Add(item);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            else
            {
                return user;
            }
        }

        public async Task<User> Delete(int id)
        {
            var user = GetById(id);
            var res = _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<User> Edit(User item)
        {
            var res = _context.Users.Update(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public ICollection<User> GetAll()
        {
            //return _context.Users
            //    .Select(DtoLogic)
            //    .ToList();
            return _context.Users.ToList();
        }

        public User GetById(int id)
            {
            //return _context.Users.Select(DtoLogic).SingleOrDefault(u => u.Id == id);
            //return _context.Users
            //    .Select(DtoLogic)
            //    .SingleOrDefault(u => u.Id == id);
            return _context.Users.SingleOrDefault(user => user.Id == id);
        }

            public ICollection<User> GetByPredicate(Func<User, bool> predicate)
            {
                //return _context.Users
                //.Select(DtoLogic)
                //.Where(predicate).ToList();
            return _context.Users.Where(predicate).ToList();
            }

            public User findItem(User item)
            {
            //item.Password = item.Password.GetHashCode().ToString();
            return _context.Users.Where(user => user.UserName == item.UserName && user.Password == item.Password).SingleOrDefault();
            }

        public int GetUserIdByUserName(string UserName)
        {
            var res = _context.Users.SingleOrDefault(u => u.UserName.Equals(UserName));
            //Where(user =>
            //user.UserName.Equals(UserName))

            if (res != null) return res.Id;
            return 0;
        }

        //    private User DtoLogic(User user)
        //    {
        //        var dtoUser = _converter.DtoUser(user);
        //        dtoUser.Comments = user.Comments.Count > 0 ? user.Comments.Select(c =>
        //        {
        //            var dtoComment = _converter.DtoComment(c);
        //            //dtoComment.User = _converter.DtoUser(c.User);
        //            dtoComment.Post = _converter.DtoPost(c.Post);
        //            dtoComment.Tags = c.Tags.Count > 0 ? c.Tags.Select(t =>
        //             {
        //                 var dtoTag = _converter.DtoTag(t);
        //                 return dtoTag;
        //             }).ToArray() : new List<Tag>();

        //            dtoComment.UserTaggedComment = c.UserTaggedComment.Count > 0 ? c.UserTaggedComment.Select(ut =>
        //            {
        //                return _converter.DtoUserTaggedComment(ut);
        //            }).ToArray() : new List<UserTaggedComment>();

        //            return dtoComment;
        //        }).ToArray() : new List<Comment>();
        //        dtoUser.Posts = user.Posts.Count > 0 ? user.Posts.Select(p =>
        //        {
        //            var dtoPost = _converter.DtoPost(p);
        //            dtoPost.Likes = p.Likes.Count > 0 ? p.Likes.Select(l =>
        //            {
        //                var dtoLike = _converter.DtoLike(l);
        //                dtoLike.User = _converter.DtoUser(l.User);
        //                return dtoLike;
        //            }).ToList() : new List<Like>();
        //            dtoPost.Comments = p.Comments.Count > 0 ? p.Comments.Select(c =>
        //            {
        //                var dtoComment = _converter.DtoComment(c);
        //                dtoComment.User = _converter.DtoUser(c.User);

        //            })
        //        })

        //        return dtoUser;
        //    }
    }


}

