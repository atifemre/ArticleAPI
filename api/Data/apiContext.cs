using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class apiContext : DbContext
    {
        public DbSet<Articles> Articles { get; set; }
        public DbSet<Reviews> Reviews { get; set; }

        public apiContext()
        {
        }

        public apiContext (DbContextOptions<apiContext> options)
            : base(options)
        {
        }

    }
}
