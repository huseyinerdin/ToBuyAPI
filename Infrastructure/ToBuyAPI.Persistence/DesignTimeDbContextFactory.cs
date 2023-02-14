﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Persistence.Contexts;

namespace ToBuyAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ToBuyAPIDbContext>
    {
        public ToBuyAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ToBuyAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
            return new ToBuyAPIDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
