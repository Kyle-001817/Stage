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
        public DbSet<V_bdq> V_bdq { get; set; }
        public DbSet<V_bde> V_bde { get; set; }
        public DbSet<Detail_bde> Detail_bde { get; set; }
        public DbSet<Proprietaire> Proprietaire { get; set; }
        public DbSet<Rendement> Rendement { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<V_SalairePersonnel> V_SalairePersonnel { get; set; }
        public DbSet<MaterielTravail> MaterielTravail { get; set; }
        public DbSet<Approbation> Approbations { get; set; }
        public DbSet<Plan> Plan { get; set; }

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

        modelBuilder.Entity<V_bdq>()
            .HasNoKey();

        modelBuilder.Entity<V_bde>()
            .HasNoKey();

        modelBuilder.Entity<Rendement>()
            .HasNoKey();

        modelBuilder.Entity<V_SalairePersonnel>()
            .HasNoKey();

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
            modelBuilder.Entity<Bdq>()
                .Property(p => p.Etat)
                .HasDefaultValue(1);

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

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Detail_bde>()
                .Property(p => p.IdDetailBde)
                .HasDefaultValueSql($"NEXT VALUE FOR dbde_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Proprietaire>()
                .Property(p => p.IdProprietaire)
                .HasDefaultValueSql($"NEXT VALUE FOR propri_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Personnel>()
                .Property(p => p.IdPersonnel)
                .HasDefaultValueSql($"NEXT VALUE FOR perso_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Service>()
                .Property(p => p.IdService)
                .HasDefaultValueSql($"NEXT VALUE FOR service_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MaterielTravail>()
                .Property(p => p.IdMaterielTravail)
                .HasDefaultValueSql($"NEXT VALUE FOR mattrav_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Approbation>()
                .Property(p => p.Id_approbation)
                .HasDefaultValueSql($"NEXT VALUE FOR approb_seq");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Plan>()
                .Property(p => p.IdPlan)
                .HasDefaultValueSql($"NEXT VALUE FOR plan_seq");


    }
}