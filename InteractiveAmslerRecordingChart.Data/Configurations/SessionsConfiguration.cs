using InteractiveAmslerRecordingChart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractiveAmslerRecordingChart.Data.Configurations
{
    class SessionsConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.DateTime).IsRequired();
        }
    }
}
