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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        public DataContext _context;
        public DbSet<TEntity> dbset;
        string errorMessage;
        public GenericRepository(DataContext context)
        {
            _context = context;
            this.dbset = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAsQueryable(string includeProperties = "")
        {
            IQueryable<TEntity> query = dbset;
            query = query.Where(x => !x.IsDeleted);
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
        public async Task<int> DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            TEntity entity = dbset.Find(id);
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<TEntity> GetAll(string includeProperties = "")
        {
            if(!string.IsNullOrEmpty(includeProperties))
                return dbset.Where(x => !x.IsDeleted).Include(includeProperties).ToList();
            else
                return dbset.Where(x => !x.IsDeleted).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties = "")
        {
            if (!string.IsNullOrEmpty(includeProperties))
                return await dbset.Where(x => !x.IsDeleted).Include(includeProperties).ToListAsync();
            else
                return await dbset.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<TEntity> GetByIdASync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<int> InsertAsync(TEntity entity)
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

        public int Insert(TEntity entity)
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

        public async Task<int> InsertAllAsync(List<TEntity> entities)
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
            TEntity entity = dbset.Find(id);
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity);
            }
            dbset.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> RemoveAsync(TEntity entity)
        {
            dbset.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public int Update(TEntity entity)
        {
            entity.LastModified = DateTime.Now;
            _context.Update(entity);
            return _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            entity.LastModified = DateTime.Now;
            _context.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
