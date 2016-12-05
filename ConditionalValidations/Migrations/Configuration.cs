namespace ConditionalValidations.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ConditionalValidations.Models.ConditionalValidationsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ConditionalValidations.Models.ConditionalValidationsContext context)
        {
        }
    }
}
