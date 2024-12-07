using Electronics_store_db_wpf.Data.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.EFcommonRepository
{
    public interface IEFcommonRepository<T> where T : BaseEntity
    {
        public Task<bool> CheckRecordByIdAsync(Guid id);
        public Task<T> GetByIdAsync(Guid id);
        public Task<bool> AddAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<List<T>> GetAllAsync();
        public Task<bool> DeleteByIdAsync(Guid id);
        public Task<bool> DeleteByIdsAsync(List<Guid> ids);
    }
}
