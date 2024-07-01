using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class MailContext:IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KAAN-MONSTER\\SQLEXPRESS;initial catalog=DbMail;integrated Security=true");
        }


        public DbSet<Messages> Messages { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
