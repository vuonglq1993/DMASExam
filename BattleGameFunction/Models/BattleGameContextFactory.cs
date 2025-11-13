using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class BattleGameContextFactory : IDesignTimeDbContextFactory<BattleGameContext>
{
    public BattleGameContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BattleGameContext>();

        // Lấy connection string từ environment variable hoặc hardcode tạm thời
        var conn = Environment.GetEnvironmentVariable("SqlConnectionString") 
                   ?? "Server=.;Database=BattleGameDB;Trusted_Connection=True;";
        
        optionsBuilder.UseSqlServer(conn);

        return new BattleGameContext(optionsBuilder.Options);
    }
}