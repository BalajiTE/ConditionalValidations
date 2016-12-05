namespace ConditionalValidations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConditionlValidationContext : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Book");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        PublishDate = c.DateTime(),
                        NumberOfBooks = c.Int(),
                        FieldOne = c.String(),
                        FieldTwo = c.String(),
                        FieldThree = c.String(),
                        FieldFour = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Genre = c.String(),
                    })
                .PrimaryKey(t => t.BookId);
            
        }
    }
}
