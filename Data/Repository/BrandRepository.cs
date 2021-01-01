using Core;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BrandRepository : IBrandRepository
    {
        public DataContext _context;
        public DbSet<Brand> dbset;
        string errorMessage;
        public BrandRepository(DataContext context)
        {
            _context = context;
            this.dbset = context.Set<Brand>();
        }
        public async Task<int> DeleteAsync(Brand entity)
        {            
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            Brand entity = dbset.Find(id);
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<Brand> GetAll()
        {
            return dbset.Where(x => !x.IsDeleted).ToList();
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await dbset.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Brand> GetByIdASync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<int> InsertAsync(Brand entity)
        {
            try
            {
                entity.AddedDate = DateTime.Now;
                dbset.Add(entity);
                return await _context.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public int Insert(Brand entity)
        {
            try
            {
                entity.AddedDate = DateTime.Now;
                dbset.Add(entity);
                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> InsertAllAsync(List<Brand> entities)
        {
            try
            {
                dbset.AddRange(entities);
                return await _context.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }            
        }

        public async Task<int> RemoveAsync(int id)
        {
            Brand entity = dbset.Find(id);
            if(_context.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity);
            }
            dbset.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> RemoveAsync(Brand entity)
        {
            dbset.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public int Update(Brand entity)
        {
            entity.LastModified = DateTime.Now;
            _context.Update(entity);
            return _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(Brand entity)
        {
            entity.LastModified = DateTime.Now;
            _context.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
