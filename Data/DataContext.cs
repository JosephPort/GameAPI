using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameAPI.Entities.Tables;

namespace GameAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Player> Player { get; set; }
        public DbSet<Leaderboard> Leaderboard { get; set; }
        public DbSet<GameSave> GameSave { get; set; }
    }
}