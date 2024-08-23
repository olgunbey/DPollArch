using DPoll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPoll.Persistence.Data.Configurations;
internal class SlideConfiguration:EntityConfiguration<Slide>
{
    public override void Configure(EntityTypeBuilder<Slide> builder)
    {
        base.Configure(builder);
        builder.Property(p => p.Content).HasColumnType("jsonb").IsRequired();

    }
}
