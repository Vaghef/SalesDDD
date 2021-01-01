using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        IEnumerable<Brand> GetAll();
        Task<Brand> GetByIdASync(int id);
        Task<int> InsertAsync(Brand entity);
        Task<int> DeleteAsync(Brand entity);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(Brand entity);


        int Update(Brand entity);
        int Insert(Brand entity);

        Task<int> InsertAllAsync(List<Brand> entities);
        Task<int> RemoveAsync(int id);
        Task<int> RemoveAsync(Brand brand);

    }
}
