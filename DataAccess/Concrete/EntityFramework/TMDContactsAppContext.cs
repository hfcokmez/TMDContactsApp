using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class TMDContactsAppContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=TESTWEBDB\TESTWEBDB02;Database=TMDContacts;User Id=db_testadmin;Password=sabahsoft;Trusted_Connection=False;MultipleActiveResultSets=true;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupContact> Groups_Contacts { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
    }
}
