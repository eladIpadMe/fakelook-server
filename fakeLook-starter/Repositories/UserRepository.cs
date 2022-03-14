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

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public Task<User> Add(User item)
            {
                throw new NotImplementedException();
            }

            public Task<User> Edit(User item)
            {
                throw new NotImplementedException();
            }

            public ICollection<User> GetAll()
            {
            return _context.Users.ToList();
            }

            public User GetById(int id)
            {
                return _context.Users.SingleOrDefault(u => u.Id == id);
            }

            public ICollection<User> GetByPredicate(Func<User, bool> predicate)
            {
                throw new NotImplementedException();
            }
        }
    }

