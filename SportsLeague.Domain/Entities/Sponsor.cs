<<<<<<< HEAD
using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities;

public class Sponsor : AuditBase
{
    public string Name { get; set; } = string.Empty;

    public string ContactEmail { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? WebsiteUrl { get; set; }

    public SponsorCategory Category { get; set; } = SponsorCategory.Bronze;

    // Navigation
    public ICollection<TournamentSponsor> TournamentSponsors { get; set; } = new List<TournamentSponsor>();
}
=======
using System.ComponentModel.DataAnnotations;
using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities
{
    public class Sponsor : AuditBase
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ContactEmail { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public string? WebsiteUrl { get; set; }
        public SponsorCategory Category { get; set; } = SponsorCategory.Bronze;

        // Navigation
        public ICollection<TournamentSponsor> TournamentSponsors { get; set; } = new List<TournamentSponsor>();
    }
}
>>>>>>> e3d6aca (Fase 4 completada)
