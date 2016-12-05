namespace ConditionalValidations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConditionalValidationContext3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConditionalValidation",
                c => new
                    {
                        ConditionalValidationId = c.Int(nullable: false, identity: true),
                        FieldOne = c.String(nullable: false),
                        FieldTwo = c.String(),
                        FieldThree = c.String(),
                        FieldFour = c.String(),
                        FieldFive = c.String(),
                        FieldSix = c.String(),
                        FieldSeven = c.DateTime(),
                        FieldEight = c.Int(),
                    })
                .PrimaryKey(t => t.ConditionalValidationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ConditionalValidation");
        }
    }
}
