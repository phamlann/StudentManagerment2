namespace StudentManagerment2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class code4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(maxLength: 15));
        }
    }
}
