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

    public class TagRepository : ITagRepository
    {

        readonly private DataContext _context;
        readonly private IDtoConverter _converter;
        public TagRepository(DataContext context, IDtoConverter converter)
        {
            _context = context;
            _converter = converter;
        }
        public async Task<Tag> Add(Tag item)
        {
            var tag = _context.Tags.SingleOrDefault(t => t.Content == item.Content);
            if(tag == null){
                var res = _context.Tags.Add(item);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            else
            {
                return tag;
            }
        }

        public async Task<Tag> Delete(int id)
        {
            var tag = GetById(id);
            var res = _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Tag> Edit(Tag item)
        {
            var res = _context.Tags.Update(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public ICollection<Tag> GetAll()
        {
            return _context.Tags
                .Select(DtoLogic)
                .ToList();
        }

        public Tag GetById(int id)
        {
            return _context.Tags
                 .Select(DtoLogic)
                 .SingleOrDefault(u => u.Id == id);
        }

        public ICollection<Tag> GetByPredicate(Func<Tag, bool> predicate)
        {
            return _context.Tags
               .Select(DtoLogic)
               .Where(predicate).ToList();
        }
        private Tag DtoLogic(Tag tag)
        {
            var dtoTag = _converter.DtoTag(tag);
            return dtoTag;
        }
    }
}
