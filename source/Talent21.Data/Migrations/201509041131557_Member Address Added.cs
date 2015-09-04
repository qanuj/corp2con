namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberAddressAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Address", c => c.String());
            AddColumn("dbo.Members", "PinCode", c => c.String());
            AddColumn("dbo.Contacts", "Address", c => c.String());
            AddColumn("dbo.Contacts", "PinCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "PinCode");
            DropColumn("dbo.Contacts", "Address");
            DropColumn("dbo.Members", "PinCode");
            DropColumn("dbo.Members", "Address");
        }
    }
}
