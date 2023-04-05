using HW_3.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HW_3.Repositories {
    public class EfRepository<T> : IRepository<T> where T : BaseEntity {
        private readonly DbContext _dbContext;

        public EfRepository(DbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync() {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(Guid id) {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(T entity) {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity) {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id) {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null) {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
