using DPoll.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPoll.Persistence.Data.Configurations;
internal class PresentationConfiguration : EntityConfiguration<Presentation>
{
    
    public override void Configure(EntityTypeBuilder<Presentation> builder)
    {
        base.Configure(builder);
    }

}
