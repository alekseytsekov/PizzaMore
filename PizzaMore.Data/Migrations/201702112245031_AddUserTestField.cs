namespace PizzaMore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTestField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RequestParams", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RequestParams");
        }
    }
}
