using AppCore.DataAccess.Settings;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Concretes.EntityFramework;

public class BookStoreContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    // public DbSet<Genre> Genres { get; set; }
    // public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<BookOrder> BookOrders { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionSetting.ConnectionStrings);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserDetail)
            .WithOne(ud => ud.User)
            .HasForeignKey<User>(u => u.UserDetailId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserDetail>()
            .HasOne(ud => ud.Country)
            .WithMany(co => co.UserDetails)
            .HasForeignKey(ud => ud.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserDetail>()
            .HasOne(ud => ud.City)
            .WithMany(c => c.UserDetails)
            .HasForeignKey(ud => ud.CityId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<City>()
            .HasOne(c => c.Country)
            .WithMany(co => co.Cities)
            .HasForeignKey(c => c.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        // modelBuilder.Entity<BookGenre>()
        //     .HasOne(bg => bg.Book)
        //     .WithMany(b => b.BookGenres)
        //     .HasForeignKey(bg => bg.BookId)
        //     .OnDelete(DeleteBehavior.NoAction);
        //
        // modelBuilder.Entity<BookGenre>()
        //     .HasOne(bg => bg.Genre)
        //     .WithMany(g => g.BookGenres)
        //     .HasForeignKey(bg => bg.GenreId)
        //     .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<BookOrder>()
            .HasOne(bo => bo.Book)
            .WithMany(b => b.BookOrders)
            .HasForeignKey(bo => bo.BookId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<BookOrder>()
            .HasOne(bo => bo.Order)
            .WithMany(o => o.BookOrders)
            .HasForeignKey(bo => bo.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Name);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        modelBuilder.Entity<UserDetail>()
            .HasIndex(ud => ud.EMail)
            .IsUnique();
    }
}