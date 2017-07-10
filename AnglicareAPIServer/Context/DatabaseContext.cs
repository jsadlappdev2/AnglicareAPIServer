using AnglicareAPIServer.Models;
using System.Data.Entity;

namespace AnglicareAPIServer.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {

        }
        
        public DbSet<RegisterUser> RegisterUser { get; set; }
        public DbSet<RegisterCompany> RegisterCompany { get; set; }
        public DbSet<TokensManager> TokensManager { get; set; }
        public DbSet<ClientKey> ClientKeys { get; set; }
        public DbSet<MusicStore> MusicStore { get; set; }
    }
}