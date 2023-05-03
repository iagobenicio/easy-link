using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using easy_link.Entities;
using Microsoft.EntityFrameworkCore;

namespace easy_link.Context
{
    public class EasylinkContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
       public EasylinkContext(DbContextOptions<EasylinkContext> options) : base (options) {}
       public DbSet<User>? user { get; set; }
       public DbSet<Page>? page { get; set; }
       public DbSet<Link>? link { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {   
            builder.Entity<User>().Property(p => p.Email).IsRequired();
            builder.Entity<User>().Property(p => p.UserName).IsRequired();
            builder.Entity<User>().Property(p => p.PasswordHash).IsRequired();
            base.OnModelCreating(builder);
        }  
    }
}