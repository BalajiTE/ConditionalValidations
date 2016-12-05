using System.Data.Entity;

namespace ConditionalValidations.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ConditionalValidationsContext>
    {
        protected override void Seed(ConditionalValidationsContext context)
        {            
            context.SaveChanges();
        }
    }
}