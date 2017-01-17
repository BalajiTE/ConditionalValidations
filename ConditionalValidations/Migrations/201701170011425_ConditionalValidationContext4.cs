namespace ConditionalValidations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConditionalValidationContext4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConditionalValidation", "FieldNine", c => c.String());
            AddColumn("dbo.ConditionalValidation", "FieldTen", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConditionalValidation", "FieldTen");
            DropColumn("dbo.ConditionalValidation", "FieldNine");
        }
    }
}
