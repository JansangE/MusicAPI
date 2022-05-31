using Microsoft.EntityFrameworkCore;
using EP.DOMAIN;
using Type = EP.DOMAIN.Type;

namespace EP.DAL
{
    public class MusicContext : DbContext
    {
        private static string Local_DB =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MusicOkie;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Local_DB);
        }

        //DB Set
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Composer> Composers { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<PieceArtist> PiecesArtists { get; set; }
        public DbSet<Type> Types { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region PRIMARY KEYS

            modelBuilder.Entity<Composer>().HasKey(x => x.ID);
            modelBuilder.Entity<Type>().HasKey(x => x.ID);
            modelBuilder.Entity<Piece>().HasKey(x => x.ID);
            modelBuilder.Entity<Artist>().HasKey(x => x.ID);

           
            #endregion

            #region UNIQUE KEYS

            #endregion

            #region REQUIRED

            #region Composer

            modelBuilder.Entity<Composer>()
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Composer>()
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Composer>()
                .Property(x => x.NickName)
                .HasComputedColumnSql("LEFT(FirstName,1) + '.' + LastName", stored: true);

            #endregion

            #region Type

            modelBuilder.Entity<Type>()
                .Property(x => x.NameType)
                .IsRequired()
                .HasMaxLength(150);
            #endregion

            #region Piece

            modelBuilder.Entity<Piece>()
                .Property(x => x.NamePiece)
                .IsRequired()
                .HasMaxLength(150);
            #endregion

            #endregion

            #region ONE-TO-MAMY

            modelBuilder.Entity<Piece>()
                .HasOne(a => a.Composer)
                .WithMany(b => b.Pieces);

            modelBuilder.Entity<Piece>()
                .HasOne(a => a.Type)
                .WithMany(b => b.Pieces);

            #endregion

            #region MANY-TO-MANY

            modelBuilder.Entity<PieceArtist>()
                .HasKey(x => new { x.PieceID, x.ArtistID });
            modelBuilder.Entity<PieceArtist>()
                .HasOne(a => a.Artist)
                .WithMany(b => b.Pieces);

            modelBuilder.Entity<PieceArtist>()
                .HasOne(a => a.Piece)
                .WithMany(b => b.Artists);

            #endregion

        }
    }
}