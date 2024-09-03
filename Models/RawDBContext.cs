using Microsoft.EntityFrameworkCore;

namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class RawDBContext : DbContext
  {
    public RawDBContext() { }
    public RawDBContext(DbContextOptions<RawDBContext> options) : base(options) { }

    public virtual DbSet<Bookmark> Bookmarks { get; set; }
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<GroceryItem> GroceryItems { get; set; }
    public virtual DbSet<GroceryList> GroceryLists { get; set; }
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<ManualMeal> ManualMeals { get; set; }
    public virtual DbSet<Meal> Meals { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Planner> Planners { get; set; }
    public virtual DbSet<Store> Stores { get; set; }
    public virtual DbSet<Tracker> Trackers { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer("Data Source=raw2plate-fyp-db.czkgrx27genj.us-east-1.rds.amazonaws.com;Initial Catalog=raw2plate;User ID=raw2plate_admin;Password=raw2plate_admin;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

      modelBuilder.Entity<Bookmark>(entity =>
      {
        entity.ToTable("Bookmark");

        entity.HasKey(e => e.BookmarkId);

        entity.Property(e => e.BookmarkId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.MealType)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.RecipeId)
          .IsRequired();

        entity.Property(e => e.DateAdded)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.UserId);
      });

      modelBuilder.Entity<Cart>(entity =>
      {
        entity.ToTable("Cart");

        entity.HasKey(e => e.CartId);

        entity.Property(e => e.CartId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.Quantity)
          .IsRequired();

        entity.Property(e => e.UserId);

        entity.Property(e => e.ItemId);
      });

      modelBuilder.Entity<GroceryItem>(entity =>
      {
        entity.ToTable("GroceryItem");

        entity.HasKey(e => e.GroceryItemId);

        entity.Property(e => e.GroceryItemId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.Name)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.IsCompleted)
          .IsRequired();

        entity.Property(e => e.GroceryListId);
      });

      modelBuilder.Entity<GroceryList>(entity =>
      {
        entity.ToTable("GroceryList");

        entity.HasKey(e => e.GroceryListId);

        entity.Property(e => e.GroceryListId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.UserId);
      });

      modelBuilder.Entity<Item>(entity =>
      {
        entity.ToTable("Item");

        entity.HasKey(e => e.ItemId);

        entity.Property(e => e.ItemId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.Name)
          .IsRequired()
          .HasMaxLength(255);
        
        entity.Property(e => e.Category)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Image)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Price)
          .IsRequired()
          .HasColumnType("decimal(10,2)");
        
        entity.Property(e => e.Description)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.StoreId);
      });

      modelBuilder.Entity<ManualMeal>(entity =>
      {
        entity.ToTable("ManualMeal");

        entity.HasKey(e => e.ManualMealId);

        entity.Property(e => e.ManualMealId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.Name)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Calories)
          .IsRequired();
        
        entity.Property(e => e.TrackerId);
      });

      modelBuilder.Entity<Meal>(entity =>
      {
        entity.ToTable("Meal");

        entity.HasKey(e => e.MealId);

        entity.Property(e => e.MealId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.MealType)
          .IsRequired()
          .HasMaxLength(255);
        
        entity.Property(e => e.RecipeId)
          .IsRequired();
        
        entity.Property(e => e.PlannerId);

        entity.Property(e => e.TrackerId);
      });

      modelBuilder.Entity<Order>(entity =>
      {
        entity.ToTable("Order");

        entity.HasKey(e => e.OrderId);

        entity.Property(e => e.OrderId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.Receiver)
          .IsRequired()
          .HasMaxLength(255);
        
        entity.Property(e => e.Contact)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Address)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.TotalPrice)
          .IsRequired()
          .HasColumnType("decimal(10,2)");

        entity.Property(e => e.PaidWith)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Status)
          .IsRequired()
          .HasMaxLength(255);
        
        entity.Property(e => e.Date)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.OrderTime)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.DeliveredTime)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Driver)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.UserId);
      });

      modelBuilder.Entity<OrderItem>(entity =>
      {
        entity.ToTable("OrderItem");

        entity.HasKey(e => e.OrderItemId);

        entity.Property(e => e.OrderItemId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.ItemId);

        entity.Property(e => e.OrderId);
      });

      modelBuilder.Entity<Planner>(entity =>
      {
        entity.ToTable("Planner");

        entity.HasKey(e => e.PlannerId);

        entity.Property(e => e.PlannerId)
          .ValueGeneratedOnAdd()
          .IsRequired();
        
        entity.Property(e => e.Date)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.UserId);
      });

      modelBuilder.Entity<Store>(entity =>
      {
        entity.ToTable("Store");

        entity.HasKey(e => e.StoreId);

        entity.Property(e => e.StoreId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.Name)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Image)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Distance)
          .IsRequired();
      });

      modelBuilder.Entity<Tracker>(entity =>
      {
        entity.ToTable("Tracker");

        entity.HasKey(e => e.TrackerId);

        entity.Property(e => e.TrackerId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.Date)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.UserId);
      });

      modelBuilder.Entity<User>(entity =>
      {
        entity.ToTable("User");

        entity.HasKey(e => e.UserId);

        entity.Property(e => e.UserId)
          .ValueGeneratedOnAdd()
          .IsRequired();

        entity.Property(e => e.Username)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Password)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Image)
          .HasMaxLength(255);

        entity.Property(e => e.Email)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.Contact)
          .HasMaxLength(255);

        entity.Property(e => e.DateOfBirth)
          .HasMaxLength(255);

        entity.Property(e => e.Height);

        entity.Property(e => e.Weight);

        entity.Property(e => e.Age);

        entity.Property(e => e.RegisteredDate)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(e => e.IsDarkMode)
          .IsRequired();
        
        entity.Property(e => e.IsAppleAuth)
          .IsRequired();

        entity.Property(e => e.IsGoogleAuth)
          .IsRequired();
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
