using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder builder, DbEntityConfiguration<TEntity> config) where TEntity : class
        {
            builder.Entity<TEntity>(config.Configure);
        }
    }

    public abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
