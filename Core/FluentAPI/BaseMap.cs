using Common.Extensions;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.FluentAPI
{
    public class UnitMap : DbEntityConfiguration<Unit>
    {
        public override void Configure(EntityTypeBuilder<Unit> entity)
        {
            entity.ToTable("Units");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
        }
    }

    public class BrandMap : DbEntityConfiguration<Brand>
    {
        public override void Configure(EntityTypeBuilder<Brand> entity)
        {
            entity.ToTable("Brands");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
        }
    }

}
