using fakeLook_models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Interfaces
{
    public interface IRepository<T>
    {
        public Task<T> Add(T item);
        public ICollection<T> GetAll();
        public Task<T> Edit(T item);

        public Task<T> Delete(int id);
        public T GetById(int id);
        public ICollection<T> GetByPredicate(Func<T, bool> predicate);
        
    }
    public interface ICommentRepository : IRepository<Comment>
    {

    }
    public interface IGroupRepository : IRepository<Group>
    {

    }
    public interface ILikeRepository : IRepository<Like>
    {

    }
    public interface IPostRepository : IRepository<Post>
    {

    }
    public interface ITagRepository : IRepository<Tag>
    {

    }
    public interface IUserRepository : IRepository<User>
    {
        public User findItem(User item);
    }
    public interface IUserTaggedCommentRepository : IRepository<UserTaggedComment>
    {

    }
    public interface IUserTaggedPostRepository : IRepository<UserTaggedPost>
    {

    }


}
