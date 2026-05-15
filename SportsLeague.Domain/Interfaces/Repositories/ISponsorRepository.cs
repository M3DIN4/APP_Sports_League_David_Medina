<<<<<<< HEAD
using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface ISponsorRepository : IGenericRepository<Sponsor>
{
    Task<Sponsor?> GetByNameAsync(string name);
    Task<bool> ExistsByNameAsync(string name);
}
=======
using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ISponsorRepository : IGenericRepository<Sponsor>
    {
        Task<bool> ExistsByNameAsync(string name);
    }
}
>>>>>>> e3d6aca (Fase 4 completada)
