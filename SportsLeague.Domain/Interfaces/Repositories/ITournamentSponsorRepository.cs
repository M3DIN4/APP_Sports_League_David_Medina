<<<<<<< HEAD
using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface ITournamentSponsorRepository : IGenericRepository<TournamentSponsor>
{
    Task<IEnumerable<TournamentSponsor>> GetByTournamentIdAsync(int tournamentId);
    Task<TournamentSponsor?> GetByTournamentAndSponsorAsync(int tournamentId, int sponsorId);
}
=======
using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITournamentSponsorRepository
    {
        Task<TournamentSponsor> CreateAsync(TournamentSponsor entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int tournamentId, int sponsorId);
        Task<IEnumerable<TournamentSponsor>> GetBySponsorIdAsync(int sponsorId);
    }
}
>>>>>>> e3d6aca (Fase 4 completada)
