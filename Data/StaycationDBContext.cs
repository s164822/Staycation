using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Staycation.Models;

namespace Staycation.Data
{
    public partial class StaycationDBContext : DbContext
    {
        public StaycationDBContext()
        {
        }

        public StaycationDBContext(DbContextOptions<StaycationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adresse> Adresse { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<BookingStatus> BookingStatus { get; set; }
        public virtual DbSet<Fuldbooking> Fuldbooking { get; set; }
        public virtual DbSet<Kunde> Kunde { get; set; }
        public virtual DbSet<VærelseType> VærelseType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=StaycationDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Adresse1)
                    .IsRequired()
                    .HasColumnName("Adresse")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.By)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Etage)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nummer)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PostNummer)
                    .HasColumnName("Post_nummer")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingNummer)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.KundeId)
                    .HasName("FK_Booking_Kunde_idx");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_Booking_BookingStatus_idx");

                entity.HasIndex(e => e.VærelseTypeId)
                    .HasName("FK_Booking_VærelseType_idx");

                entity.Property(e => e.BookingNummer)
                    .HasColumnName("Booking_nummer")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AntalBørn)
                    .HasColumnName("Antal_børn")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AntalVoksne)
                    .HasColumnName("Antal_voksne")
                    .HasColumnType("int(11)");

                entity.Property(e => e.KundeId)
                    .HasColumnName("Kunde_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("Status_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TjekIndDato)
                    .HasColumnName("Tjek_ind_dato")
                    .HasColumnType("date");

                entity.Property(e => e.TjekUdDato)
                    .HasColumnName("Tjek_ud_dato")
                    .HasColumnType("date");

                entity.Property(e => e.TotalPris)
                    .HasColumnName("Total_pris")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.VærelseTypeId)
                    .HasColumnName("Værelse_type_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Kunde)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.KundeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_Kunde");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_BookingStatus");

                entity.HasOne(d => d.VærelseType)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.VærelseTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_VærelseType");
            });

            modelBuilder.Entity<BookingStatus>(entity =>
            {
                entity.ToTable("Booking_status");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fuldbooking>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("fuldbooking");

                entity.Property(e => e.AdresseId)
                    .HasColumnName("Adresse_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AntalBørn)
                    .HasColumnName("Antal_børn")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AntalVoksne)
                    .HasColumnName("Antal_voksne")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BookingNummer)
                    .HasColumnName("Booking_nummer")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Efternavn)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fornavn)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Fødselsdagsdato).HasColumnType("date");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.KundeId)
                    .HasColumnName("Kunde_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Pasnummer)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId)
                    .HasColumnName("Status_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TelefonNummer)
                    .HasColumnName("Telefon_nummer")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TjekIndDato)
                    .HasColumnName("Tjek_ind_dato")
                    .HasColumnType("date");

                entity.Property(e => e.TjekUdDato)
                    .HasColumnName("Tjek_ud_dato")
                    .HasColumnType("date");

                entity.Property(e => e.TotalPris)
                    .HasColumnName("Total_pris")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.VærelseTypeId)
                    .HasColumnName("Værelse_type_ID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Kunde>(entity =>
            {
                entity.HasIndex(e => e.AdresseId)
                    .HasName("FK_Kunde_Adresse_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AdresseId)
                    .HasColumnName("Adresse_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Efternavn)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fornavn)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Fødselsdagsdato).HasColumnType("date");

                entity.Property(e => e.Pasnummer)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonNummer)
                    .HasColumnName("Telefon_nummer")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Adresse)
                    .WithMany(p => p.Kunde)
                    .HasForeignKey(d => d.AdresseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kunde_Adresse");
            });

            modelBuilder.Entity<VærelseType>(entity =>
            {
                entity.ToTable("Værelse_type");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
