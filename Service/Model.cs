using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClashRoyaleBot {
    public class ClsshRoyaleContext : DbContext {
        public DbSet<Player> Players { get; set; }
        public DbSet<Clan> Clans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=clashRoyale.db");
        }
    }

    public class Player {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlayerTag { get; set; }
        public string Name { get; set; }

        public int ClanTag { get; set; }
        public Clan Clan { get; set; }
    }

    public class Clan {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClanTag { get; set; }
        public string Title { get; set; }

        public List<Player> Players { get; set; }
    }
}
