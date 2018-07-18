namespace MyInventory.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class InvDBContext : DbContext
    {
        public InvDBContext()
            : base("name=InvDBContext")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Item_Category> Item_Category { get; set; }
        public virtual DbSet<Item_Master> Item_Master { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order_Details> Order_Details { get; set; }
        public virtual DbSet<Order_Master> Order_Master { get; set; }
        public virtual DbSet<PO_Detail> PO_Detail { get; set; }
        public virtual DbSet<PO_Header> PO_Header { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<UOM> UOMs { get; set; }
        public virtual DbSet<vwStock> vwStocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Order_Details)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.ApprovedBy);

            modelBuilder.Entity<Item_Category>()
                .HasMany(e => e.Item_Master)
                .WithOptional(e => e.Item_Category)
                .HasForeignKey(e => e.Cat_ID);

            modelBuilder.Entity<Item_Master>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.Item_Master)
                .HasForeignKey(e => e.Product_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order_Master>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Order_Master)
                .HasForeignKey(e => e.OrderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PO_Header>()
                .Property(e => e.PO_Number)
                .IsFixedLength();

            modelBuilder.Entity<PO_Header>()
                .Property(e => e.Supplier)
                .IsUnicode(false);

            modelBuilder.Entity<PO_Header>()
                .HasMany(e => e.PO_Detail)
                .WithRequired(e => e.PO_Header)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.Qty)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Stock>()
                .Property(e => e.WIP_Qty)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Stock)
                .HasForeignKey(e => e.StockId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Service_ID)
                .IsFixedLength();

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Resource_ID)
                .IsFixedLength();

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Product_ID)
                .IsFixedLength();

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Qty)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Approver)
                .IsFixedLength();

            modelBuilder.Entity<Transaction>()
                .Property(e => e.E_Approver)
                .IsFixedLength();

            modelBuilder.Entity<UOM>()
                .Property(e => e.Unit)
                .IsFixedLength();

            modelBuilder.Entity<UOM>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<UOM>()
                .HasMany(e => e.Item_Master)
                .WithRequired(e => e.UOM)
                .HasForeignKey(e => e.Unit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<vwStock>()
                .Property(e => e.Qty)
                .HasPrecision(18, 3);

            modelBuilder.Entity<vwStock>()
                .Property(e => e.WIP_Qty)
                .HasPrecision(18, 3);
        }
    }
}
