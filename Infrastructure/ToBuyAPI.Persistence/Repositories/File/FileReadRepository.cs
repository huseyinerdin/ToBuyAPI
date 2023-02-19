﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Contexts;

namespace ToBuyAPI.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<ToBuyApı.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ToBuyAPIDbContext context) : base(context)
        {
        }
    }
}