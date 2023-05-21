namespace PRIORI_SERVICES_API.Repository.Interface;

public interface IRepositoryGenerico<TEntity> where TEntity : class
{
    IQueryable<TEntity> FindAll();

    Task<TEntity?> GetById(int id);

    Task Create(TEntity entity);

    Task Update(TEntity entity);

    Task Delete(int id);
}
