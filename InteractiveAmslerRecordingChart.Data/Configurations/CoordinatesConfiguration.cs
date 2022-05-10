using InteractiveAmslerRecordingChart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace InteractiveAmslerRecordingChart.Data.Configurations
{
    class CoordinatesConfiguration : IEntityTypeConfiguration<Coordinate>
    {
        public void Configure(EntityTypeBuilder<Coordinate> builder)
        {
            builder.Property(e => e.SessionId).IsRequired();
            builder.Property(e => e.VisualFieldStatus).HasConversion(v => v.ToString(), v => (VisualFieldStatus)Enum.Parse(typeof(VisualFieldStatus), v));
            builder.Property(e => e.X).IsRequired();
            builder.Property(e => e.Y).IsRequired();
        }
    }
}
