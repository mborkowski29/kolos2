using kolos2.Models;
using Microsoft.EntityFrameworkCore;

public class DBContext : DbContext
{

public DBContext(DbContextOptions<DBContext> options) : base(options)
{
}

public DbSet<Backpacks> Backpacks { get; set; }
public DbSet<Character_titles> Character_titles { get; set; }
public DbSet<Characters> Characters { get; set; }
public DbSet<Items> Items { get; set; }
public DbSet<Titles> Titles { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
        
    modelBuilder.Entity<Backpacks>()
        .HasKey(b => new { b.CharacterId, b.ItemId });

    modelBuilder.Entity<Backpacks>()
        .HasOne(b => b.Characters)
        .WithMany(c => c.Backpacks)
        .HasForeignKey(b => b.CharacterId);

    modelBuilder.Entity<Backpacks>()
        .HasOne(b => b.Items)
        .WithMany(i => i.Backpacks)
        .HasForeignKey(b => b.ItemId);
    
    modelBuilder.Entity<Characters>().HasData(
        new Characters { Id = 1, FirstName = "John", LastName = "Yakuza", CurrentWeight = 43, MaxWeight = 500000 },
        new Characters { Id = 2, FirstName = "Mikołaj", LastName = "Borkowski", CurrentWeight = 53, MaxWeight = 15000 }

    );
        
    modelBuilder.Entity<Items>().HasData(
        new Items { Id = 1, Name = "Item1", Weight = 10 },
        new Items { Id = 2, Name = "Item2", Weight = 11 },
        new Items { Id = 3, Name = "Item3", Weight = 12 }
    );

    modelBuilder.Entity<Titles>().HasData(
        new Titles { Id = 1, Name = "Title1" },
        new Titles { Id = 2, Name = "Title2" },
        new Titles { Id = 3, Name = "Title3" }
    );

    modelBuilder.Entity<Backpacks>().HasData(
        new Backpacks { Amount = 2, ItemId = 1, CharacterId = 1 },
        new Backpacks { Amount = 1, ItemId = 2, CharacterId = 1 },
        new Backpacks { Amount = 1, ItemId = 3, CharacterId = 1 },
        new Backpacks { Amount = 1, ItemId = 3, CharacterId = 2 },
        new Backpacks { Amount = 1, ItemId = 2, CharacterId = 2 },
        new Backpacks { Amount = 2, ItemId = 1, CharacterId = 2 }
    );

    modelBuilder.Entity<Character_titles>().HasData(
        new Character_titles { CharacterId = 1, TitleId = 1, AcquiredAt = new DateTime(2024, 6, 10) },
        new Character_titles { CharacterId = 1, TitleId = 2, AcquiredAt = new DateTime(2024, 6, 9) },
        new Character_titles { CharacterId = 1, TitleId = 3, AcquiredAt = new DateTime(2024, 6, 8) },
        new Character_titles { CharacterId = 2, TitleId = 3, AcquiredAt = new DateTime(2024, 6, 8) }
    );
}
}