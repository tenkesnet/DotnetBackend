using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tanulok.Models;

public partial class IskolaContext : DbContext
{
    public IskolaContext()
    {
    }

    public IskolaContext(DbContextOptions<IskolaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alkalmazott> Alkalmazotts { get; set; }

    public virtual DbSet<AutoCsop> AutoCsops { get; set; }

    public virtual DbSet<Autok> Autoks { get; set; }

    public virtual DbSet<Rendele> Rendeles { get; set; }

    public virtual DbSet<Reszleg> Reszlegs { get; set; }

    public virtual DbSet<Tipusok> Tipusoks { get; set; }

    public virtual DbSet<Ugyfelek> Ugyfeleks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=iskola;Username=postgres;Password=PostgreSQL");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alkalmazott>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("alkalmazott_pkey");

            entity.ToTable("alkalmazott");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlkKod).HasColumnName("alk_kod");
            entity.Property(e => e.AlkNev)
                .HasMaxLength(20)
                .HasColumnName("alk_nev");
            entity.Property(e => e.Belepes).HasColumnName("belepes");
            entity.Property(e => e.Beosztas)
                .HasMaxLength(16)
                .HasColumnName("beosztas");
            entity.Property(e => e.Fizetes).HasColumnName("fizetes");
            entity.Property(e => e.Premium).HasColumnName("premium");
            entity.Property(e => e.ReszlegId)
                .ValueGeneratedOnAdd()
                .HasColumnName("reszleg_id");

            entity.HasOne(d => d.Reszleg).WithMany(p => p.Alkalmazotts)
                .HasForeignKey(d => d.ReszlegId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("alkalmazott_reszleg_id_fkey");
        });

        modelBuilder.Entity<AutoCsop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auto_csop_pkey");

            entity.ToTable("auto_csop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AutoCsopNev)
                .HasMaxLength(6)
                .HasColumnName("auto_csop_nev");
            entity.Property(e => e.KmDij).HasColumnName("km_dij");
            entity.Property(e => e.NapiDij).HasColumnName("napi_dij");
        });

        modelBuilder.Entity<Autok>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("autok_pkey");

            entity.ToTable("autok");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlkalmazottId)
                .ValueGeneratedOnAdd()
                .HasColumnName("alkalmazott_id");
            entity.Property(e => e.Allapot)
                .HasMaxLength(1)
                .HasColumnName("allapot");
            entity.Property(e => e.Ar).HasColumnName("ar");
            entity.Property(e => e.AutoCsopId)
                .ValueGeneratedOnAdd()
                .HasColumnName("auto_csop_id");
            entity.Property(e => e.FutottKm).HasColumnName("futott_km");
            entity.Property(e => e.Rendszam)
                .HasMaxLength(7)
                .HasColumnName("rendszam");
            entity.Property(e => e.ReszlegId)
                .ValueGeneratedOnAdd()
                .HasColumnName("reszleg_id");
            entity.Property(e => e.TipusokId)
                .ValueGeneratedOnAdd()
                .HasColumnName("tipusok_id");
            entity.Property(e => e.UtSzerviz).HasColumnName("ut_szerviz");
            entity.Property(e => e.VasarlasDatuma).HasColumnName("vasarlas_datuma");

            entity.HasOne(d => d.Alkalmazott).WithMany(p => p.Autoks)
                .HasForeignKey(d => d.AlkalmazottId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("autok_alkalmazott_id_fkey");

            entity.HasOne(d => d.AutoCsop).WithMany(p => p.Autoks)
                .HasForeignKey(d => d.AutoCsopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("autok_auto_csop_id_fkey");

            entity.HasOne(d => d.Reszleg).WithMany(p => p.Autoks)
                .HasForeignKey(d => d.ReszlegId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("autok_reszleg_id_fkey");

            entity.HasOne(d => d.Tipusok).WithMany(p => p.Autoks)
                .HasForeignKey(d => d.TipusokId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("autok_tipusok_id_fkey");
        });

        modelBuilder.Entity<Rendele>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rendeles_pkey");

            entity.ToTable("rendeles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fizetes)
                .HasMaxLength(1)
                .HasColumnName("fizetes");
            entity.Property(e => e.KmKezdet).HasColumnName("km_kezdet");
            entity.Property(e => e.KmVeg).HasColumnName("km_veg");
            entity.Property(e => e.KolcsonDij).HasColumnName("kolcson_dij");
            entity.Property(e => e.KolcsonKezdete).HasColumnName("kolcson_kezdete");
            entity.Property(e => e.Napok).HasColumnName("napok");
            entity.Property(e => e.RendelesDatum).HasColumnName("rendeles_datum");
            entity.Property(e => e.RendelesSzam)
                .HasMaxLength(5)
                .HasColumnName("rendeles_szam");
            entity.Property(e => e.RendeloSzemely)
                .HasMaxLength(12)
                .HasColumnName("rendelo_szemely");
            entity.Property(e => e.RendszamId)
                .ValueGeneratedOnAdd()
                .HasColumnName("rendszam_id");
            entity.Property(e => e.TipusokId)
                .ValueGeneratedOnAdd()
                .HasColumnName("tipusok_id");
            entity.Property(e => e.UgyfelekId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ugyfelek_id");

            entity.HasOne(d => d.Rendszam).WithMany(p => p.Rendeles)
                .HasForeignKey(d => d.RendszamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rendeles_rendszam_id_fkey");

            entity.HasOne(d => d.Tipusok).WithMany(p => p.Rendeles)
                .HasForeignKey(d => d.TipusokId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rendeles_tipusok_id_fkey");

            entity.HasOne(d => d.Ugyfelek).WithMany(p => p.Rendeles)
                .HasForeignKey(d => d.UgyfelekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rendeles_ugyfelek_id_fkey");
        });

        modelBuilder.Entity<Reszleg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reszleg_pkey");

            entity.ToTable("reszleg");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ReszlegCim)
                .HasMaxLength(15)
                .HasColumnName("reszleg_cim");
            entity.Property(e => e.ReszlegKod).HasColumnName("reszleg_kod");
            entity.Property(e => e.ReszlegNev)
                .HasMaxLength(20)
                .HasColumnName("reszleg_nev");
        });

        modelBuilder.Entity<Tipusok>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipusok_pkey");

            entity.ToTable("tipusok");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AutoCsopId)
                .ValueGeneratedOnAdd()
                .HasColumnName("auto_csop_id");
            entity.Property(e => e.Leiras)
                .HasMaxLength(30)
                .HasColumnName("leiras");
            entity.Property(e => e.SzervizKm).HasColumnName("szerviz_km");
            entity.Property(e => e.TipusNev)
                .HasMaxLength(15)
                .HasColumnName("tipus_nev");

            entity.HasOne(d => d.AutoCsop).WithMany(p => p.Tipusoks)
                .HasForeignKey(d => d.AutoCsopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipusok_auto_csop_id_fkey");
        });

        modelBuilder.Entity<Ugyfelek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ugyfelek_pkey");

            entity.ToTable("ugyfelek");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cim)
                .HasMaxLength(15)
                .HasColumnName("cim");
            entity.Property(e => e.FizetesiMod)
                .HasMaxLength(1)
                .HasColumnName("fizetesi_mod");
            entity.Property(e => e.IranyitoSzam)
                .HasMaxLength(6)
                .HasColumnName("iranyito_szam");
            entity.Property(e => e.Megbizott)
                .HasMaxLength(15)
                .HasColumnName("megbizott");
            entity.Property(e => e.Orszag)
                .HasMaxLength(10)
                .HasColumnName("orszag");
            entity.Property(e => e.UgyfelNev)
                .HasMaxLength(20)
                .HasColumnName("ugyfel_nev");
            entity.Property(e => e.UgyfelSzam)
                .HasMaxLength(3)
                .HasColumnName("ugyfel_szam");
            entity.Property(e => e.Varos)
                .HasMaxLength(10)
                .HasColumnName("varos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
