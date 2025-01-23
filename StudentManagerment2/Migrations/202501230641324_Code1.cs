namespace StudentManagerment2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Code1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Classes", "ClassName", c => c.String(nullable: false));
            AlterColumn("dbo.Classes", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Classes", "Description", c => c.String());
            AlterColumn("dbo.Classes", "ClassName", c => c.String());
        }
    }
}
