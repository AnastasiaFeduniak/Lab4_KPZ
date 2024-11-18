using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab4_1.Models;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookAmountLocation> BookAmountLocations { get; set; }

    public virtual DbSet<BookAmountOrder> BookAmountOrders { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientOrderAnalytics1> ClientOrderAnalytics1s { get; set; }

    public virtual DbSet<ClientOrderAnalytics2> ClientOrderAnalytics2s { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderTotalAmount> OrderTotalAmounts { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<WorkingHour> WorkingHours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-41EOJRI\\SQLEXPRESS;initial catalog=BookStore;trusted_connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__86516BCF601C1085");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__490D1AE18A4718BB");

            entity.ToTable(tb => tb.HasTrigger("trg_BeforeInsertBooks"));

            entity.HasIndex(e => e.AuthorId, "idx_Book_author_id");

            entity.HasIndex(e => e.BookId, "idx_book_id").IsUnique();

            entity.HasIndex(e => e.BookId, "idx_books_book_id").IsUnique();

            entity.HasIndex(e => e.BookId, "idx_books_bookid");

            entity.HasIndex(e => e.BookId, "idx_books_books");

            entity.HasIndex(e => e.Price, "idx_books_booksprice");

            entity.HasIndex(e => e.Title, "idx_books_bookstitle");

            entity.HasIndex(e => e.Title, "idx_books_bookstitle1");

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DiscountPercentage)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discount_percentage");
            entity.Property(e => e.FinalPrice)
                .HasComputedColumnSql("([price]-([price]*[discount_percentage])/(100))", true)
                .HasColumnType("decimal(21, 8)")
                .HasColumnName("final_price");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PublicationYear).HasColumnName("publication_year");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__author_id__5165187F");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__category___52593CB8");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__publisher__534D60F1");
        });

        modelBuilder.Entity<BookAmountLocation>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__BookAmou__BFCFB4DDA33AA848");

            entity.ToTable("BookAmountLocation");

            entity.HasIndex(e => new { e.BookId, e.Quantity }, "idx_BookAmountOrder_double_book_quantity");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Book).WithMany(p => p.BookAmountLocations)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__BookAmoun__book___6477ECF3");

            entity.HasOne(d => d.Location).WithMany(p => p.BookAmountLocations)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__BookAmoun__locat__6383C8BA");
        });

        modelBuilder.Entity<BookAmountOrder>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__BookAmou__BFCFB4DD1BEAE106");

            entity.ToTable("BookAmountOrder", tb =>
                {
                    tb.HasTrigger("trg_add_to_location_on_not_finished_delete");
                    tb.HasTrigger("trg_update_sold_by_price");
                    tb.HasTrigger("update_book_amount_location_on_insert");
                });

            entity.HasIndex(e => e.BookId, "idx_bookAmountOrder_books");

            entity.HasIndex(e => e.Quantity, "idx_bookAmountOrder_quant");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.SoldByPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sold_by_price");

            entity.HasOne(d => d.Book).WithMany(p => p.BookAmountOrders)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__BookAmoun__book___6A30C649");

            entity.HasOne(d => d.Order).WithMany(p => p.BookAmountOrders)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__BookAmoun__order__693CA210");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__D54EE9B4DCDBFA7D");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__BF21A4244E8994AA");

            entity.HasIndex(e => e.Email, "IDX_Clients_Phone_Email");

            entity.HasIndex(e => e.ClientId, "idx_Clients_client_id");

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.FullName)
                .HasMaxLength(71)
                .IsUnicode(false)
                .HasComputedColumnSql("(([first_name]+' ')+[last_name])", false)
                .HasColumnName("full_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<ClientOrderAnalytics1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ClientOrderAnalytics1");

            entity.Property(e => e.AvgOrderValue)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("avg_order_value");
            entity.Property(e => e.ClientFullName)
                .HasMaxLength(201)
                .IsUnicode(false)
                .HasColumnName("client_full_name");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ClientRank).HasColumnName("client_rank");
            entity.Property(e => e.LocationAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location_address");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.TotalOrders).HasColumnName("total_orders");
            entity.Property(e => e.TotalRevenue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("total_revenue");
        });

        modelBuilder.Entity<ClientOrderAnalytics2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ClientOrderAnalytics2");

            entity.Property(e => e.AvgOrderValue)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("avg_order_value");
            entity.Property(e => e.ClientFullName)
                .HasMaxLength(201)
                .IsUnicode(false)
                .HasColumnName("client_full_name");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ClientRank).HasColumnName("client_rank");
            entity.Property(e => e.TotalOrders).HasColumnName("total_orders");
            entity.Property(e => e.TotalRevenue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("total_revenue");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__771831EA5AB0117E");

            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LocationType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("location_type");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__46596229A98720CB");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("SetOrderDate");
                    tb.HasTrigger("trg_UpdateStatusOnReceivingDate");
                });

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("delivery_address");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.ReceiptNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("receipt_number");
            entity.Property(e => e.ReceivingDate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("receiving_date");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("paid")
                .HasColumnName("status");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Orders__client_i__5BE2A6F2");

            entity.HasOne(d => d.Location).WithMany(p => p.Orders)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__Orders__location__5CD6CB2B");
        });

        modelBuilder.Entity<OrderTotalAmount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OrderTotalAmount");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("total_amount");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__3263F29DD9B12DEF");

            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<WorkingHour>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__WorkingH__771831EA1DD9C800");

            entity.Property(e => e.LocationId)
                .ValueGeneratedNever()
                .HasColumnName("location_id");
            entity.Property(e => e.SaturdayEnd).HasColumnName("saturday_end");
            entity.Property(e => e.SaturdayStart).HasColumnName("saturday_start");
            entity.Property(e => e.SundayEnd).HasColumnName("sunday_end");
            entity.Property(e => e.SundayStart).HasColumnName("sunday_start");
            entity.Property(e => e.WeekdaysEnd).HasColumnName("weekdays_end");
            entity.Property(e => e.WeekdaysStart).HasColumnName("weekdays_start");

            entity.HasOne(d => d.Location).WithOne(p => p.WorkingHour)
                .HasForeignKey<WorkingHour>(d => d.LocationId)
                .HasConstraintName("FK__WorkingHo__locat__2EA5EC27");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
