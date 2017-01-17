namespace ConditionalValidations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConditionalValidationContext5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConditionalValidation", "FieldElevan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConditionalValidation", "FieldElevan");
        }
    }
}
