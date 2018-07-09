namespace AngularReduxDemo.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AngularReduxDemoEntities : DbContext
    {
        public AngularReduxDemoEntities()
            : base("name=AngularReduxDemoEntities")
        {
        }

        public virtual DbSet<DrinkItem> DrinkItems { get; set; }
        public virtual DbSet<MealItem> MealItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrinkItem>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MealItem>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Section>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Server>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }

        //public virtual DbSet<DrinkItem> DrinkItems { get; set; }
        //public virtual DbSet<MealItem> MealItems { get; set; }
        //public virtual DbSet<Order> Orders { get; set; }
        //public virtual DbSet<Section> Sections { get; set; }
        //public virtual DbSet<Server> Servers { get; set; }
        //public virtual DbSet<Table> Tables { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DrinkItem>()
        //        .Property(e => e.Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<MealItem>()
        //        .Property(e => e.Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Section>()
        //        .Property(e => e.Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Server>()
        //        .Property(e => e.Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Table>()
        //        .HasMany(e => e.DrinkItems)
        //        .WithOptional(e => e.Table)
        //        .HasForeignKey(e => e.OrderId);

        //    modelBuilder.Entity<Table>()
        //        .HasMany(e => e.MealItems)
        //        .WithOptional(e => e.Table)
        //        .HasForeignKey(e => e.OrderId);
        //}
    }
}
