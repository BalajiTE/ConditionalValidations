namespace ConditionalValidations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContionalValidationContext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "FieldThree", c => c.String());
            AddColumn("dbo.Book", "FieldFour", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "FieldFour");
            DropColumn("dbo.Book", "FieldThree");
        }
    }
}
