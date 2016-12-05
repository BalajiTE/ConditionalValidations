namespace ConditionalValidations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConditionalValidationContext2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "FieldOne", c => c.String());
            AddColumn("dbo.Book", "FieldTwo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "FieldTwo");
            DropColumn("dbo.Book", "FieldOne");
        }
    }
}
