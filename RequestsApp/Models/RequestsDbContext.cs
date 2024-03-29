using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RequestsApp.Models
{
    public partial class RequestsDbContext : DbContext
    {
        public RequestsDbContext()
        {
        }

        public RequestsDbContext(DbContextOptions<RequestsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClosedRequestsTable> ClosedRequestsTables { get; set; } = null!;
        public virtual DbSet<DocumentsTable> DocumentsTables { get; set; } = null!;
        public virtual DbSet<DocumentsView> DocumentsViews { get; set; } = null!;
        public virtual DbSet<EmployeesTable> EmployeesTables { get; set; } = null!;
        public virtual DbSet<FacilityTable> FacilityTables { get; set; } = null!;
        public virtual DbSet<RequestsTable> RequestsTables { get; set; } = null!;
        public virtual DbSet<RequestsView> RequestsViews { get; set; } = null!;
        public virtual DbSet<ScheduleView> ScheduleViews { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-UTKFT1Q\\SQLEXPRESS;Initial Catalog=RequestsDb;Integrated Security=True; Trusted_Connection=True; TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClosedRequestsTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ClosedRequests_table");

                entity.Property(e => e.CloseDate)
                    .HasColumnType("date")
                    .HasColumnName("Close_date");

                entity.Property(e => e.Employee).HasMaxLength(50);

                entity.Property(e => e.Facility).HasMaxLength(100);

                entity.Property(e => e.OpenDate)
                    .HasColumnType("date")
                    .HasColumnName("Open_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Phone).HasMaxLength(13);
            });

            modelBuilder.Entity<DocumentsTable>(entity =>
            {
                entity.HasKey(e => e.DocumentId)
                    .HasName("PK__Document__513B384DBF180A1F");

                entity.ToTable("Documents_table");

                entity.Property(e => e.DocumentId).HasColumnName("Document_id");

                entity.Property(e => e.FacilityId).HasColumnName("Facility_id");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.DocumentsTables)
                    .HasForeignKey(d => d.FacilityId)
                    .HasConstraintName("FK_Doc_Facility");
            });

            modelBuilder.Entity<DocumentsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Documents_view");

                entity.Property(e => e.DocumentId).HasColumnName("Document_id");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<EmployeesTable>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__Employee__781228D918A1CC73");

                entity.ToTable("Employees_table");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("First_name");

                entity.Property(e => e.Password).HasMaxLength(30);

                entity.Property(e => e.Phone).HasMaxLength(13);

                entity.Property(e => e.Position).HasMaxLength(100);

                entity.Property(e => e.SecondName)
                    .HasMaxLength(30)
                    .HasColumnName("Second_name");

                entity.Property(e => e.ThirdName)
                    .HasMaxLength(30)
                    .HasColumnName("Third_name");
            });

            modelBuilder.Entity<FacilityTable>(entity =>
            {
                entity.HasKey(e => e.FacilityId)
                    .HasName("PK__Facility__CEA9201D89B4FBE5");

                entity.ToTable("Facility_table");

                entity.HasIndex(e => e.DirName, "UQ__Facility__2750AC18A2811F5A")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__Facility__737584F68B01FD05")
                    .IsUnique();

                entity.HasIndex(e => e.AgentName, "UQ__Facility__C1354F67CD4E07BF")
                    .IsUnique();

                entity.Property(e => e.FacilityId).HasColumnName("Facility_id");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.AgentName)
                    .HasMaxLength(100)
                    .HasColumnName("Agent_name");

                entity.Property(e => e.DirName)
                    .HasMaxLength(100)
                    .HasColumnName("Dir_name");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(13);
            });

            modelBuilder.Entity<RequestsTable>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__Requests__E9C0AF0B4C0FCA75");

                entity.ToTable("Requests_table");

                entity.Property(e => e.RequestId).HasColumnName("Request_id");

                entity.Property(e => e.CloseDate)
                    .HasColumnType("date")
                    .HasColumnName("Close_date");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.FacilityId).HasColumnName("Facility_id");

                entity.Property(e => e.OpenDate)
                    .HasColumnType("date")
                    .HasColumnName("Open_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Phone).HasMaxLength(13);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RequestsTables)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Emplyee");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.RequestsTables)
                    .HasForeignKey(d => d.FacilityId)
                    .HasConstraintName("FK_Req_Facility");
            });

            modelBuilder.Entity<RequestsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Requests_view");

                entity.Property(e => e.CloseDate)
                    .HasColumnType("date")
                    .HasColumnName("Close_date");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.OpenDate)
                    .HasColumnType("date")
                    .HasColumnName("Open_date");

                entity.Property(e => e.Phone).HasMaxLength(13);

                entity.Property(e => e.RequestId).HasColumnName("Request_id");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(30)
                    .HasColumnName("Second_name");
            });

            modelBuilder.Entity<ScheduleView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Schedule_view");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.OpenDate)
                    .HasColumnType("date")
                    .HasColumnName("Open_date");

                entity.Property(e => e.Phone).HasMaxLength(13);

                entity.Property(e => e.RequestId).HasColumnName("Request_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
