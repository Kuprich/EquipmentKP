﻿using Equipment.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Context
{
    public class EquipmentContext : DbContext
    {   
        
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<EquipmentType>     EquipmentTypes      { get; set; }
        public DbSet<Location>          Locations           { get; set; }
        public DbSet<Owner>             Owners              { get; set; }
        public DbSet<EquipmentsKit>     EquipmentsKits      { get; set; }
        public DbSet<MainEquipment>     MainEquipments      { get; set; }
        public DbSet<RequestMovement>   RequestMovements    { get; set; }
        public DbSet<RequestState>      RequestStates       { get; set; }
        public DbSet<Request>           Requests            { get; set; }
        public DbSet<Document>          Documents           { get; set; }


        public EquipmentContext(DbContextOptions<EquipmentContext> options) : base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentsKit>()
                .HasMany(e => e.MainEquipments)
                .WithOne(k => k.EquipmentsKit)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MainEquipment>()
               .HasMany(r => r.Requests)
               .WithOne(e => e.MainEquipment)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Request>()
              .HasMany(m => m.RequestMovements)
              .WithOne(r => r.Request)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Request>()
              .HasMany(d => d.Documents)
              .WithOne(r => r.Request)
              .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
