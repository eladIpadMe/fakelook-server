﻿using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class UserRepository : IUserRepository
    {
            readonly private DataContext _context;
            readonly private DtoConverter _converter;

        public UserRepository(DataContext context, DtoConverter converter)
        {
            _context = context;
            _converter = converter;
        }
        public async Task<User> Add(User item)
        {
            var user = _context.Users.Where(x => x.UserName == item.UserName).FirstOrDefault();
            
            if(user == null) {
                //item.Password = item.Password.GetHashCode().ToString();
                var res = _context.Users.Add(item);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> Delete(User item)
        {
            var res = _context.Users.Remove(item);
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

            return _context.Users.ToList();
            }

            public User GetById(int id)
            {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return _converter.DtoUser(user);
            }

            public ICollection<User> GetByPredicate(Func<User, bool> predicate)
            {
                return _context.Users.Where(predicate).ToList();
            }

            public User findItem(User item)
            {
            //item.Password = item.Password.GetHashCode().ToString();
            return _context.Users.Where(user => user.UserName == item.UserName && user.Password == item.Password).SingleOrDefault();
            }
        }
    }

