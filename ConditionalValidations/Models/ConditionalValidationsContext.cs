using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ConditionalValidations.Models
{
    public class ConditionalValidationsContext : DbContext
    {
        public ConditionalValidationsContext() : base("ConditionalValidations")
        {
            //Database.Initialize(false);            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<ConditionalValidation> ConditionalValidation { get; set; }
       
    }
}