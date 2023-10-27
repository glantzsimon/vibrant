namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameContactToClient : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Contact", newName: "Client");
            RenameTable(name: "dbo.ContactProduct", newName: "ClientProduct");
            RenameColumn(table: "dbo.Consultation", name: "ContactId", newName: "ClientId");
            RenameColumn(table: "dbo.ClientProduct", name: "ContactId", newName: "ClientId");
            RenameColumn(table: "dbo.Product", name: "ContactId", newName: "ClientId");
            RenameColumn(table: "dbo.Order", name: "ContactId", newName: "ClientId");
            RenameColumn(table: "dbo.ProductPack", name: "ContactId", newName: "ClientId");
            RenameColumn(table: "dbo.Protocol", name: "ContactId", newName: "ClientId");
            RenameIndex(table: "dbo.ClientProduct", name: "IX_ContactId", newName: "IX_ClientId");
            RenameIndex(table: "dbo.Product", name: "IX_ContactId", newName: "IX_ClientId");
            RenameIndex(table: "dbo.Consultation", name: "IX_ContactId", newName: "IX_ClientId");
            RenameIndex(table: "dbo.Order", name: "IX_ContactId", newName: "IX_ClientId");
            RenameIndex(table: "dbo.ProductPack", name: "IX_ContactId", newName: "IX_ClientId");
            RenameIndex(table: "dbo.Protocol", name: "IX_ContactId", newName: "IX_ClientId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Protocol", name: "IX_ClientId", newName: "IX_ContactId");
            RenameIndex(table: "dbo.ProductPack", name: "IX_ClientId", newName: "IX_ContactId");
            RenameIndex(table: "dbo.Order", name: "IX_ClientId", newName: "IX_ContactId");
            RenameIndex(table: "dbo.Consultation", name: "IX_ClientId", newName: "IX_ContactId");
            RenameIndex(table: "dbo.Product", name: "IX_ClientId", newName: "IX_ContactId");
            RenameIndex(table: "dbo.ClientProduct", name: "IX_ClientId", newName: "IX_ContactId");
            RenameColumn(table: "dbo.Protocol", name: "ClientId", newName: "ContactId");
            RenameColumn(table: "dbo.ProductPack", name: "ClientId", newName: "ContactId");
            RenameColumn(table: "dbo.Order", name: "ClientId", newName: "ContactId");
            RenameColumn(table: "dbo.Product", name: "ClientId", newName: "ContactId");
            RenameColumn(table: "dbo.ClientProduct", name: "ClientId", newName: "ContactId");
            RenameColumn(table: "dbo.Consultation", name: "ClientId", newName: "ContactId");
            RenameTable(name: "dbo.ClientProduct", newName: "ContactProduct");
            RenameTable(name: "dbo.Client", newName: "Contact");
        }
    }
}
