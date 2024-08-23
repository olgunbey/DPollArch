using DPoll.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DPoll.Persistence.Data.Configurations;
internal abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(m => m.Id).ValueGeneratedOnAdd().IsRequired();
    }
}