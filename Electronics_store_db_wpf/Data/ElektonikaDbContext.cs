using Microsoft.EntityFrameworkCore;
using Electronics_store_db_wpf.Data.DatabaseModel;
using System.Reflection;
using System;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Data;
using Electronics_store_db_wpf.Helper;
#nullable disable

namespace Electronics_store_db_wpf.Data
{
    public partial class ElektonikaDbContext : DbContext
    {

        private readonly string _baseDirectory;
        readonly string _connectionString;
        public ElektonikaDbContext()
        {
        }

        public ElektonikaDbContext(DbContextOptions<ElektonikaDbContext> options, string connectionString)
            : base(options)
        {

            
            _connectionString = connectionString;


            bool initializeData =  Database.EnsureCreated();

            if (initializeData)
            {
                string filePath = Path.Combine(FilePathHelper._baseDirectory, "Data\\DatabaseScripts\\data_initialization.sql");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = File.ReadAllText(filePath, Encoding.GetEncoding(1251)); // Указываем кодировку UTF-8 при чтении файла

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.Passwordhash)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("passwordhash");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.Image)
                  .HasMaxLength(255)
                  .IsUnicode(false)
                  .HasColumnName("image")
                  .HasConversion(
                      v => FilePathHelper.GetRelativePathForSaving(v, "Resources\\Img\\Employees"),
                      v => FilePathHelper.GetRelativePathForRetrieval(v));

                entity.Property(e => e.Passwordhash)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("passwordhash");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Position)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("position");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("salary");

                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("surname");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__employee__role_i__44FF419A");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_amount");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__order__client_id__48CFD27E");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_item");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_ite__order__4CA06362");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__order_ite__produ__4D94879B");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image");


                entity.Property(e => e.Image)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("image")
                   .HasConversion(
                        v => FilePathHelper.GetRelativePathForSaving(v, "Resources\\Img\\Product"),
                        v => FilePathHelper.GetRelativePathForRetrieval(v)); 

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__product__categor__3B75D760");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
       
    }
}
