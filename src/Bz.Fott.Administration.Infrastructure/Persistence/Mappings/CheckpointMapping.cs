using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Bz.Fott.Administration.Domain.ManagingCompetition;

namespace Bz.Fott.Administration.Infrastructure.Persistence.Mappings;

internal class CheckpointMapping : IEntityTypeConfiguration<Checkpoint>
{
    public void Configure(EntityTypeBuilder<Checkpoint> builder)
    {
        builder.ToTable(name: "checkpoints");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasConversion(entityId => entityId.Value, dbId => new CheckpointId(dbId));

        builder.Property(e => e.CompetitionId)
            .HasColumnName("competitionId")
            .HasConversion(entityId => entityId.Value, dbId => new CompetitionId(dbId));

        builder.OwnsOne(e => e.TrackPoint,
            navigationBuilder => 
            {
                navigationBuilder
                    .Property(trackPoint => trackPoint.Amount)
                    .HasColumnName("trackPointAmount");
                navigationBuilder
                    .Property(trackPoint => trackPoint.Unit)
                    .HasColumnName("trackPointUnit")
                    .HasColumnType("tinyint");
            });
    }
}
