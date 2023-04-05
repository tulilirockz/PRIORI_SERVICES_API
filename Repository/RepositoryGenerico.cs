using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Repository.Interface;

namespace PRIORI_SERVICES_API.Repository
{
    public class RepositoryGenerico<TEntity> : IRepositoryGenerico<TEntity> where TEntity : class
    {
        private readonly PrioriDbContext _contexto;

        public RepositoryGenerico(PrioriDbContext prioriDbContext)
        {
            _contexto = prioriDbContext;
        }

        public async Task Create(TEntity entity)
        {
            try
            {
                await _contexto.AddAsync(entity);
                await _contexto.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var entity = await GetById(id);
                _contexto.Set<TEntity>().Remove(entity);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<TEntity> FindAll()
        {
            try
            {
                return _contexto.Set<TEntity>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<TEntity> GetById(int id)
        {
            try
            {
                var entity = await _contexto.Set<TEntity>().FindAsync(id);
                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(TEntity entity)
        {
            try
            {
                var registro = _contexto.Set<TEntity>().Update(entity);
                registro.State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
