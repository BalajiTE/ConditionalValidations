namespace ConditionalValidations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConditionalValidationContext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "NumberOfBooks", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "NumberOfBooks", c => c.Int(nullable: false));
        }
    }
}
