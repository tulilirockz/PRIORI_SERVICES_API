using Microsoft.EntityFrameworkCore;
using PRIORI_SERVICES_API.Repository.Interface;

namespace PRIORI_SERVICES_API.Repository;

public class RepositoryGenerico<TEntity> : IRepositoryGenerico<TEntity> where TEntity : class
{
    private readonly PrioriDbContext _contexto;

    public RepositoryGenerico(PrioriDbContext prioriDbContext)
    {
        _contexto = prioriDbContext;
    }

    public async Task Create(TEntity entity)
    {
        await _contexto.AddAsync(entity);
        await _contexto.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await GetById(id);
        _contexto.Set<TEntity>().Remove(entity!);
        await _contexto.SaveChangesAsync();
    }

    public IQueryable<TEntity> FindAll()
    {
        return _contexto.Set<TEntity>();
    }

    public async Task<TEntity?> GetById(int id)
    {
        return await _contexto.Set<TEntity>().FindAsync(id);
    }

    public async Task Update(TEntity entity)
    {
        var registro = _contexto.Set<TEntity>().Update(entity);
        registro.State = EntityState.Modified;
        await _contexto.SaveChangesAsync();
    }
}
