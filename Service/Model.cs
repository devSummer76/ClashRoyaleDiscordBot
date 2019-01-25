using Microsoft.EntityFrameworkCore;
using System;
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

        public int TotalPoints { get; set; }

        public DateTime LastUpdate { get; set; }

        public List<History> Histories { get; set; }
    }

    public class Clan {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClanTag { get; set; }
        public string Title { get; set; }

        public List<Player> Players { get; set; }
    }

    public class History {
        [Key]
        public int Id { get; set; }

        public int PlayerTag { get; set; }
        public Player Player { get; set; }

        public int ClanTag { get; set; }

    }

    public class PointsConfiguration {
        [Key]
        public int Id { get; set; }

        public int ClanTag { get; set; }
        public Clan Clan { get; set; }

        public int Wargame_Participation { get; set; }
        public int Wargame_Absense { get; set; }

        // public int WarDeckSharing {get; set; }
        public int Wargame_Win { get; set; }
        public int Wargame_Loss { get; set; }
        public int Wargame_CardsGathered { get; set;}

        public int Weekly_Donations { get; set; }
        public int Weekly_Requests { get; set; }

 
        public int ClanLeaving { get; set; }


        // Not from the game
        public int Weekly_PointReset { get; set; }


    }



}

