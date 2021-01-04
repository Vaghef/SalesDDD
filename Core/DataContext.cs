using Common.Extensions;
using Core.FluentAPI;
using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            AddConfiguration(builder);
        }

        void AddConfiguration(ModelBuilder builder)
        {
            builder.AddConfiguration(new UnitMap());
            builder.AddConfiguration(new BrandMap());
            builder.AddConfiguration(new ProductMap());
            builder.AddConfiguration(new OrderMap());
        }
    }
}
