<<<<<<< HEAD
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Services;

public class SponsorService : ISponsorService
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly ILogger<SponsorService> _logger;

    public SponsorService(
        ISponsorRepository sponsorRepository,
        ITournamentSponsorRepository tournamentSponsorRepository,
        ITournamentRepository tournamentRepository,
        ILogger<SponsorService> logger)
    {
        _sponsorRepository = sponsorRepository;
        _tournamentSponsorRepository = tournamentSponsorRepository;
        _tournamentRepository = tournamentRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Sponsor>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all sponsors");
        return await _sponsorRepository.GetAllAsync();
    }

    public async Task<Sponsor?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving sponsor with ID: {SponsorId}", id);
        var sponsor = await _sponsorRepository.GetByIdAsync(id);
        if (sponsor == null)
            _logger.LogWarning("Sponsor with ID {SponsorId} not found", id);
        return sponsor;
    }

    public async Task<Sponsor> CreateAsync(Sponsor sponsor)
    {
        // Validations: unique name and valid email
        if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
        {
            _logger.LogWarning("Sponsor with name '{SponsorName}' already exists", sponsor.Name);
            throw new InvalidOperationException($"Ya existe un patrocinador con el nombre '{sponsor.Name}'");
        }

        if (!IsValidEmail(sponsor.ContactEmail))
        {
            throw new InvalidOperationException("Email de contacto inválido");
        }

        _logger.LogInformation("Creating sponsor: {SponsorName}", sponsor.Name);
        return await _sponsorRepository.CreateAsync(sponsor);
    }

    public async Task UpdateAsync(int id, Sponsor sponsor)
    {
        var existing = await _sponsorRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {id}");

        if (!string.Equals(existing.Name, sponsor.Name, StringComparison.OrdinalIgnoreCase))
        {
            if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
                throw new InvalidOperationException($"Ya existe un patrocinador con el nombre '{sponsor.Name}'");
        }

        if (!IsValidEmail(sponsor.ContactEmail))
            throw new InvalidOperationException("Email de contacto inválido");

        existing.Name = sponsor.Name;
        existing.ContactEmail = sponsor.ContactEmail;
        existing.Phone = sponsor.Phone;
        existing.WebsiteUrl = sponsor.WebsiteUrl;
        existing.Category = sponsor.Category;

        _logger.LogInformation("Updating sponsor with ID: {SponsorId}", id);
        await _sponsorRepository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        var exists = await _sponsorRepository.ExistsAsync(id);
        if (!exists)
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {id}");

        _logger.LogInformation("Deleting sponsor with ID: {SponsorId}", id);
        await _sponsorRepository.DeleteAsync(id);
    }

    public async Task LinkToTournamentAsync(int sponsorId, int tournamentId, decimal contractAmount)
    {
        // Validations
        var sponsorExists = await _sponsorRepository.ExistsAsync(sponsorId);
        if (!sponsorExists)
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {sponsorId}");

        var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
        if (tournament == null)
            throw new KeyNotFoundException($"No se encontró el torneo con ID {tournamentId}");

        if (contractAmount <= 0)
            throw new InvalidOperationException("El monto del contrato debe ser mayor que 0");

        var existing = await _tournamentSponsorRepository.GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
        if (existing != null)
            throw new InvalidOperationException("Este patrocinador ya está vinculado a este torneo");

        var ts = new TournamentSponsor
        {
            SponsorId = sponsorId,
            TournamentId = tournamentId,
            ContractAmount = contractAmount,
            JoinedAt = DateTime.UtcNow
        };

        _logger.LogInformation("Linking sponsor {SponsorId} to tournament {TournamentId}", sponsorId, tournamentId);
        await _tournamentSponsorRepository.CreateAsync(ts);
    }

    public async Task UnlinkFromTournamentAsync(int sponsorId, int tournamentId)
    {
        var existing = await _tournamentSponsorRepository.GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
        if (existing == null)
            throw new KeyNotFoundException("La relación patrocinador-torneo no existe");

        _logger.LogInformation("Unlinking sponsor {SponsorId} from tournament {TournamentId}", sponsorId, tournamentId);
        await _tournamentSponsorRepository.DeleteAsync(existing.Id);
    }

    public async Task<IEnumerable<Tournament>> GetTournamentsBySponsorAsync(int sponsorId)
    {
        var sponsor = await _sponsorRepository.GetByIdAsync(sponsorId);
        if (sponsor == null)
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {sponsorId}");

        var ts = await _tournamentSponsorRepository.GetAllAsync();
        var related = ts.Where(x => x.SponsorId == sponsorId).Select(x => x.Tournament);
        return related;
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
=======
using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace SportsLeague.Domain.Services;

public class SponsorService : ISponsorService
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly ILogger<SponsorService> _logger;

    public SponsorService(ISponsorRepository sponsorRepository,
        ITournamentSponsorRepository tournamentSponsorRepository,
        ITournamentRepository tournamentRepository,
        ILogger<SponsorService> logger)
    {
        _sponsorRepository = sponsorRepository;
        _tournamentSponsorRepository = tournamentSponsorRepository;
        _tournamentRepository = tournamentRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Sponsor>> GetAllAsync()
    {
        return await _sponsorRepository.GetAllAsync();
    }

    public async Task<Sponsor?> GetByIdAsync(int id)
    {
        return await _sponsorRepository.GetByIdAsync(id);
    }

    public async Task<Sponsor> CreateAsync(Sponsor sponsor)
    {
        // Validaciones
        if (string.IsNullOrWhiteSpace(sponsor.Name))
            throw new InvalidOperationException("Name is required");

        if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
            throw new InvalidOperationException($"Sponsor with name '{sponsor.Name}' already exists");

        if (!IsValidEmail(sponsor.ContactEmail))
            throw new InvalidOperationException("Contact email is invalid");

        return await _sponsorRepository.CreateAsync(sponsor);
    }

    public async Task UpdateAsync(int id, Sponsor sponsor)
    {
        var existing = await _sponsorRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Sponsor with id {id} not found");

        if (!string.Equals(existing.Name, sponsor.Name, StringComparison.OrdinalIgnoreCase))
        {
            if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
                throw new InvalidOperationException($"Sponsor with name '{sponsor.Name}' already exists");
        }

        if (!IsValidEmail(sponsor.ContactEmail))
            throw new InvalidOperationException("Contact email is invalid");

        existing.Name = sponsor.Name;
        existing.ContactEmail = sponsor.ContactEmail;
        existing.Phone = sponsor.Phone;
        existing.WebsiteUrl = sponsor.WebsiteUrl;
        existing.Category = sponsor.Category;

        await _sponsorRepository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        var exists = await _sponsorRepository.ExistsAsync(id);
        if (!exists)
            throw new KeyNotFoundException($"Sponsor with id {id} not found");

        await _sponsorRepository.DeleteAsync(id);
    }

    public async Task<TournamentSponsor> LinkSponsorToTournamentAsync(int sponsorId, TournamentSponsor relation)
    {
        // Validaciones
        var sponsorExists = await _sponsorRepository.ExistsAsync(sponsorId);
        if (!sponsorExists)
            throw new KeyNotFoundException($"Sponsor with id {sponsorId} not found");

        var tournament = await _tournamentRepository.GetByIdAsync(relation.TournamentId);
        if (tournament == null)
            throw new KeyNotFoundException($"Tournament with id {relation.TournamentId} not found");

        if (relation.ContractAmount <= 0)
            throw new InvalidOperationException("ContractAmount must be greater than zero");

        if (await _tournamentSponsorRepository.ExistsAsync(relation.TournamentId, sponsorId))
            throw new InvalidOperationException($"Sponsor {sponsorId} is already linked to tournament {relation.TournamentId}");

        relation.SponsorId = sponsorId;
        relation.JoinedAt = relation.JoinedAt == default ? DateTime.UtcNow : relation.JoinedAt;

        return await _tournamentSponsorRepository.CreateAsync(relation);
    }

    public async Task<IEnumerable<Tournament>> GetTournamentsBySponsorAsync(int sponsorId)
    {
        var sponsorExists = await _sponsorRepository.ExistsAsync(sponsorId);
        if (!sponsorExists)
            throw new KeyNotFoundException($"Sponsor with id {sponsorId} not found");

        var relations = await _tournamentSponsorRepository.GetBySponsorIdAsync(sponsorId);
        return relations.Select(r => r.Tournament!).ToList();
    }

    public async Task UnlinkSponsorFromTournamentAsync(int sponsorId, int tournamentId)
    {
        var sponsorExists = await _sponsorRepository.ExistsAsync(sponsorId);
        if (!sponsorExists)
            throw new KeyNotFoundException($"Sponsor with id {sponsorId} not found");

        var existsRelation = await _tournamentSponsorRepository.ExistsAsync(tournamentId, sponsorId);
        if (!existsRelation)
            throw new KeyNotFoundException($"Relation between sponsor {sponsorId} and tournament {tournamentId} not found");

        // find relation id
        var relations = await _tournamentSponsorRepository.GetBySponsorIdAsync(sponsorId);
        var rel = relations.FirstOrDefault(r => r.TournamentId == tournamentId);
        if (rel == null)
            throw new KeyNotFoundException($"Relation between sponsor {sponsorId} and tournament {tournamentId} not found");

        await _tournamentSponsorRepository.DeleteAsync(rel.Id);
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        try
        {
            // use simple regex
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
        catch
        {
            return false;
        }
    }
}
>>>>>>> e3d6aca (Fase 4 completada)
