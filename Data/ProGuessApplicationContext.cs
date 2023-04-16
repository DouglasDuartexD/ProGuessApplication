using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProGuessApplication.Models;

namespace ProGuessApplication.Data
{
    public class ProGuessApplicationContext : DbContext
    {
        public ProGuessApplicationContext (DbContextOptions<ProGuessApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<ProGuessApplication.Models.usuario> usuario { get; set; } = default!;

        public DbSet<ProGuessApplication.Models.git> git { get; set; }

        //public DbSet<ProGuessApplication.Models.Git> git { get; set; }
    }
}
