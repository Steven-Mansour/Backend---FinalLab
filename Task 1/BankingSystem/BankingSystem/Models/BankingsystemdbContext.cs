using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models;

public partial class BankingsystemdbContext : DbContext
{
    public BankingsystemdbContext()
    {
    }

    public BankingsystemdbContext(DbContextOptions<BankingsystemdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bankingsystemdb;Username=steven;Password=0000");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accounts_pk");

            entity.ToTable("accounts");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactions_pk");

            entity.ToTable("transactions");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Accountid).HasColumnName("accountid");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
            entity.Property(e => e.Transactiontype).HasColumnName("transactiontype");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Transaction)
                .HasForeignKey<Transaction>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transactions_accounts_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
