using Microsoft.EntityFrameworkCore;

namespace Shared.TestableCode.Database;

public class ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : DbContext(options)
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>()
            .HasKey(e => new { e.EmployeeId, e.FiscalYear, e.Category }); // Composite primary key

        modelBuilder.Entity<Expense>()
            .Property(e => e.Category)
            .HasMaxLength(25);

        modelBuilder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasColumnType("numeric(18, 2)");
    }
}
