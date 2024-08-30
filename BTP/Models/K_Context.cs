using Microsoft.EntityFrameworkCore;
namespace BTP.Models;

public class K_Context : DbContext
{
     public K_Context(DbContextOptions<K_Context> options) : base(options)
        {
        }

        public DbSet<Profil> Profil { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<TypeBordereau> TypeBordereau { get; set; }
        public DbSet<SerieTravaux> SerieTravaux { get; set; }
        public DbSet<Bdq> Bdq { get; set; }
        public DbSet<TypeMateriel> TypeMateriel {  get; set; }
        public DbSet<Unite> Unite {  get; set; }
        public DbSet<Materiel> Materiel {  get; set; }
        public DbSet<DetailBdq> DetailBdq { get; set; }
        public DbSet<DetailMateriaux> DetailMateriaux { get; set; }

    public void ResetDatabase(K_Context context)
        {
            string sql = "DO $$ DECLARE table_record RECORD; ";
            sql += " BEGIN FOR table_record IN ";
            sql += " SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE' LOOP";
            sql += " EXECUTE format('TRUNCATE TABLE %I RESTART IDENTITY CASCADE', table_record.table_name); ";
            sql += " END LOOP;END $$; ";
            context.Database.ExecuteSqlRaw(sql);
            context.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        modelBuilder.Entity<DetailMateriaux>()
            .HasNoKey();
        modelBuilder.Entity<DetailMateriaux>()
            .HasKey(dm => new { dm.IdMateriaux, dm.IdDetailBdq });

        base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Profil>()
                    .Property(p => p.IdProfil)
                    .HasDefaultValueSql($"NEXT VALUE FOR profil_seq");

            base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Utilisateur>()
                    .Property(p => p.IdUtilisateur)
                    .HasDefaultValueSql($"NEXT VALUE FOR user_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TypeBordereau>()
                .Property(p => p.IdTypeBordereau)
                .HasDefaultValueSql($"NEXT VALUE FOR tb_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SerieTravaux>()
                .Property(p => p.IdSerieTravaux)
                .HasDefaultValueSql($"NEXT VALUE FOR st_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bdq>()
                .Property(p => p.IdBdq)
                .HasDefaultValueSql($"NEXT VALUE FOR bdq_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TypeMateriel>()
                .Property(p => p.IdTypeMateriel)
                .HasDefaultValueSql($"NEXT VALUE FOR tm_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Unite>()
                .Property(p => p.IdUnite)
                .HasDefaultValueSql($"NEXT VALUE FOR unite_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Materiel>()
                .Property(p => p.IdMateriel)
                .HasDefaultValueSql($"NEXT VALUE FOR matx_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DetailBdq>()
                .Property(p => p.IdDetailBdq)
                .HasDefaultValueSql($"NEXT VALUE FOR dbdq_seq");


    }
}