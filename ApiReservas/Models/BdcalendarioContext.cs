using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiReservas.Models;

public partial class BdcalendarioContext : DbContext
{
    public BdcalendarioContext()
    {
    }

    public BdcalendarioContext(DbContextOptions<BdcalendarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.End)
                .HasColumnType("datetime")
                .HasColumnName("end");
            entity.Property(e => e.Fecha)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("fecha");
            entity.Property(e => e.Fin)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("fin");
            entity.Property(e => e.Inicio)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Start)
                .HasColumnType("datetime")
                .HasColumnName("start");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
