﻿using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class GroupRepository :  IGroupRepository
    {
         readonly private DataContext _context;
        public GroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Group> Add(Group item)
        {
            var res = _context.Groups.Add(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Group> Delete(Group item)
        {
            var res = _context.Groups.Remove(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Group> Edit(Group item)
        {
            var res = _context.Groups.Update(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public ICollection<Group> GetAll()
        {
            return _context.Groups.ToList();
        }

        public Group GetById(int id)
        {
            return _context.Groups.SingleOrDefault(g => g.Id == id);
        }

        public ICollection<Group> GetByPredicate(Func<Group, bool> predicate)
        {
            return _context.Groups.Where(predicate).ToList();
        }
    }
    
}
