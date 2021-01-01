using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAsQueryable(string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties = "");
        IEnumerable<TEntity> GetAll(string includeProperties = "");
        Task<TEntity> GetByIdASync(int id);
        Task<int> InsertAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(TEntity entity);


        int Update(TEntity entity);
        int Insert(TEntity entity);

        Task<int> InsertAllAsync(List<TEntity> entities);
        Task<int> RemoveAsync(int id);
        Task<int> RemoveAsync(TEntity brand);
    }
}
